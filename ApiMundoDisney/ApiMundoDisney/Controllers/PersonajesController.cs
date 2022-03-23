using ApiMundoDisney.Data;
using ApiMundoDisney.Entities;
using ApiMundoDisney.Entities.Dtos;
using ApiMundoDisney.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiMundoDisney.Controllers
{
    [Route("api/characters")]    
    [ApiController]
    public class PersonajesController : ControllerBase
    {       
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;        

        public PersonajesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;            
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


        //[HttpGet("detalle/{personajeId:int}", Name ="GetPersonaje")]
        [Route("detalle/{personajeId}")]
        [HttpGet]
        public IActionResult GetPersonaje(int personajeId)
        {
            var personaje = _unitOfWork.Personajes.GetById(personajeId);

            if (personaje == null)
            {
                return NotFound();
            }

            var personajeDto = _mapper.Map<PersonajeDto>(personaje);            

            return Ok(personajeDto);
        }

        [Route("create")]
        [HttpPost]
        public IActionResult CreatePersonaje([FromBody] PersonajeDto personajeDto)
        {
            if (personajeDto == null)
            {
                return BadRequest(ModelState);
            }

            var personaje = _mapper.Map<Personaje>(personajeDto);

            _unitOfWork.Personajes.Create(personaje);

            _unitOfWork.Personajes.Save();

            return Ok();            
        }

        //[HttpDelete("{personajeId:int}", Name = "DeletePersonaje")]
        [Route("delete/{personajeId}")]
        [HttpDelete]       
        public IActionResult DeletePersonaje(int personajeId)
        {
            var personaje = _unitOfWork.Personajes.GetById(personajeId);

            _unitOfWork.Personajes.Delete(personaje);

            _unitOfWork.Save();

            return Ok();
        }

        //[HttpPatch("detalle/{personajeId:int}", Name = "UpdatePersonaje")]
        [Route("detalle/{personajeId}")]
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

            return Ok(personajeUpdateDto);
        }
    }
}
