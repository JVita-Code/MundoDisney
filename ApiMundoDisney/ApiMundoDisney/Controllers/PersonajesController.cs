using ApiMundoDisney.Data;
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
    [Route("api/characters")]    
    [ApiController]
    public class PersonajesController : ControllerBase
    {       
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IPersonajeService _personajeService;

        public PersonajesController(IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, IPersonajeService personajeService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _personajeService = personajeService;
        }

        [HttpGet]
        public IActionResult GetPersonajes()
        {
            var resultado = _personajeService.GetPersonajes();

            if (resultado is null)
            {
                return BadRequest();
            }

            return Ok(resultado);
        }

        [Route("{personajeId}")]
        [HttpGet]
        public IActionResult GetPersonaje(int personajeId)
        {
            var resultado = _personajeService.GetPersonaje(personajeId);

            if (resultado is null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreatePersonaje([FromForm] PersonajeCreateDto personajeDto, IFormFileCollection archivos)
        {
            var resultado = _personajeService.CreatePersonaje(personajeDto, archivos);

            if (resultado.Resultado == Resultado.Error)
            {
                return BadRequest(ModelState);
            }

            return Ok();

            // debería ser Created con 201.

            ////var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            ////var locationUri = baseUrl + "/" + "api/movies" + $"/{pelicula.PeliculaId}";
            //return Created(baseUrl, locationUri); 
        }

        [Route("{personajeId}")]
        [HttpDelete]       
        public IActionResult DeletePersonaje(int personajeId)
        {
            var resultado = _personajeService.DeletePersonaje(personajeId);
            
            if (resultado.Resultado == Resultado.NoEncontrado)
            {
                return NotFound();                
            }
            
            return NoContent();
        }

        [Route("{personajeId}")]
        [HttpPatch]
        public IActionResult UpdatePersonaje([FromBody] PersonajeUpdateDto personajeUpdateDto, int personajeId)
        {
            var resultado = _personajeService.UpdatePersonaje(personajeUpdateDto, personajeId);
            
            if (resultado.Resultado == Resultado.Error)
            {
                return BadRequest();
            }

            //return Ok(resultado);
            return NoContent();
        }
    }
}
