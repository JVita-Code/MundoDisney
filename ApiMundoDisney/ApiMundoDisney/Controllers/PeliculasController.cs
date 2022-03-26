using ApiMundoDisney.Entities;
using ApiMundoDisney.Entities.Dtos;
using ApiMundoDisney.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApiMundoDisney.Controllers
{   
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/movies")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPeliculaRepository _peliculaRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PeliculasController(IMapper mapper, IUnitOfWork unitOfWork, IPeliculaRepository peliculaRepository, IWebHostEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _peliculaRepository = peliculaRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult GetPeliculas()
        {
            var listadoPeliculas = _unitOfWork.Peliculas.GetAll();
    
            var listadoPeliculasDto = new List<PeliculaListadoDto>();
    
            foreach (var pelicula in listadoPeliculas)
            {
                listadoPeliculasDto.Add(_mapper.Map<PeliculaListadoDto>(pelicula));
            }
    
            return Ok(listadoPeliculasDto);
        }
    
        [Route("{peliculaId}")]
        [HttpGet]
        public IActionResult GetPelicula(int peliculaId)
        {
            var pelicula = _peliculaRepository.GetPeliculaConPersonajes(peliculaId);
    
            if (pelicula is null)
            {
                return NotFound();
            }
    
            var peliculaDto = _mapper.Map<PeliculaDto>(pelicula);
    
            return Ok(peliculaDto);
        }
              
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreatePelicula([FromForm] PeliculaCreateDto peliculaCreateDto)
        {
            if (peliculaCreateDto is null)
            {
                return BadRequest(ModelState);
            }
           
            var archivo = peliculaCreateDto.Foto;
            string rutaPrincipal = _hostingEnvironment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;

            if (archivo.Length > 0)
            {
                var nombreImagen = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"imagenesPeliculas");
                var extension = Path.GetExtension(archivos[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreImagen + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }

                peliculaCreateDto.RutaImagen = @"\imagenesPeliculas\" + nombreImagen + extension;
            }

            var pelicula = _mapper.Map<Pelicula>(peliculaCreateDto);
    
            _unitOfWork.Peliculas.Create(pelicula);
    
            _unitOfWork.Save();
    
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
    
            var locationUri = baseUrl + "/" + "api/movies" + $"/{pelicula.PeliculaId}";
    
            return Created(locationUri, peliculaCreateDto);             
        }
    
        [Route("{peliculaId}")]
        [HttpDelete]
        public IActionResult DeletePelicula(int peliculaId)
        {
            var pelicula = _unitOfWork.Peliculas.Get(peliculaId);
    
            _unitOfWork.Peliculas.Delete(pelicula);
    
            _unitOfWork.Save();
  
            return NoContent();
        }
    
        [Route("{peliculaId}")]
        [HttpPatch]
        public IActionResult UpdatePelicula([FromBody] PeliculaUpdateDto peliculaUpdateDto, int peliculaId)
        {
            if (peliculaUpdateDto.PeliculaId != peliculaId || peliculaUpdateDto is null)
            {
                return BadRequest();
            }
    
            var pelicula = _mapper.Map<Pelicula>(peliculaUpdateDto);
    
            _unitOfWork.Peliculas.Update(pelicula);
    
            _unitOfWork.Save();    
    
            return NoContent();
        }
    }
}

