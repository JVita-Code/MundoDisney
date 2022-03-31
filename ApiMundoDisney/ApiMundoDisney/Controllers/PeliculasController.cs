using ApiMundoDisney.Entities;
using ApiMundoDisney.Entities.Dtos;
using ApiMundoDisney.Repositories;
using ApiMundoDisney.Services;
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
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IPeliculaService _peliculaService;

        public PeliculasController(IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, IPeliculaService peliculaService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;            
            _hostingEnvironment = hostingEnvironment;
            _peliculaService = peliculaService;
        }

        [HttpGet]
        public IActionResult GetPeliculas()
        {
            var listadoPeliculasDto = _peliculaService.GetPeliculas();

            if (listadoPeliculasDto is null)
            {
                return BadRequest();
            }
     
            return Ok(listadoPeliculasDto);
        }
    
        [Route("{peliculaId}")]
        [HttpGet]
        public IActionResult GetPelicula(int peliculaId)
        {
            var pelicula = _peliculaService.GetPelicula(peliculaId);

            if (pelicula is null)
            {
                return NotFound();
            }

            return Ok(pelicula);
        }
              
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreatePelicula([FromForm] PeliculaCreateDto peliculaCreateDto, IFormFileCollection archivos)
        {
            var resultado = _peliculaService.CreatePelicula(peliculaCreateDto, archivos);

            if (resultado.Resultado == Resultado.Error)
            {
                return BadRequest(ModelState);
            }
            
            return Ok();

            // debería ser Created con 201...

            ////var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            ////var locationUri = baseUrl + "/" + "api/movies" + $"/{pelicula.PeliculaId}";
            //return Created(baseUrl, locationUri);            
        }

        [Route("{peliculaId}")]
        [HttpDelete]
        public IActionResult DeletePelicula(int peliculaId)
        {
            var resultado = _peliculaService.DeletePelicula(peliculaId);

            if (resultado.Resultado == Resultado.NoEncontrado)
            {
                return NotFound();
            }

            return NoContent();
        }
    
        [Route("{peliculaId}")]
        [HttpPatch]
        public IActionResult UpdatePelicula([FromBody] PeliculaUpdateDto peliculaUpdateDto, int peliculaId)
        {
            var resultado = _peliculaService.UpdatePelicula(peliculaUpdateDto, peliculaId);

            if (resultado.Resultado == Resultado.Error)
            {
                return BadRequest();
            }

            //return Ok(resultado);
            return NoContent();
        }
    }
}

