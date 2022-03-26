using ApiMundoDisney.Data;
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
    [Route("api/characters")]    
    [ApiController]
    public class PersonajesController : ControllerBase
    {       
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PersonajesController(IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult GetPersonajes()
        {
            var listadoPersonajes = _unitOfWork.Personajes.GetAll();

            var listadoPersonajesDto = new List<PersonajeListadoDto>();

            foreach (var personaje in listadoPersonajes)
            {
                listadoPersonajesDto.Add(_mapper.Map<PersonajeListadoDto>(personaje));
            }           

            return Ok(listadoPersonajesDto);
        }

        [Route("{personajeId}")]
        [HttpGet]
        public IActionResult GetPersonaje(int personajeId)
        {
            var personaje = _unitOfWork.Personajes.GetPersonajeConPeliculas(personajeId);

            if (personaje is null)
            {
                return NotFound();
            }

            var personajeDto = _mapper.Map<PersonajeDto>(personaje);            

            return Ok(personajeDto);
        }
       
        [HttpPost]
        public IActionResult CreatePersonaje([FromForm] PersonajeCreateDto personajeDto)
        {
            if (personajeDto == null)
            {
                return BadRequest(ModelState);
            }

            /*subida de archivos*/
            var archivo = personajeDto.Foto;
            string rutaPrincipal = _hostingEnvironment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;

            if (archivo.Length > 0)
            {
                //Nueva imagen
                var nombreImagen = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"imagenes");
                var extension = Path.GetExtension(archivos[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreImagen + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }
                personajeDto.RutaImagen = @"\imagenes\" + nombreImagen + extension;
            }

            var personaje = _mapper.Map<Personaje>(personajeDto);

            _unitOfWork.Personajes.Create(personaje);
            
            _unitOfWork.Save();

            return Ok();            
        }
       
        [Route("{personajeId}")]
        [HttpDelete]       
        public IActionResult DeletePersonaje(int personajeId)
        {
            var personaje = _unitOfWork.Personajes.Get(personajeId);

            _unitOfWork.Personajes.Delete(personaje);

            _unitOfWork.Save();

            return NoContent();
        }

        [Route("{personajeId}")]
        [HttpPatch]
        public IActionResult UpdatePersonaje([FromBody] PersonajeUpdateDto personajeUpdateDto, int personajeId)
        {
            if (personajeUpdateDto.PersonajeId != personajeId || personajeUpdateDto == null)
            {
                return BadRequest();
            }

            var personaje = _mapper.Map<Personaje>(personajeUpdateDto);

            _unitOfWork.Personajes.Update(personaje);

            _unitOfWork.Save();

            return NoContent();
        }
    }
}
