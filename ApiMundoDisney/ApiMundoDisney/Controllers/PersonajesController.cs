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


        [HttpGet("{personajeId:int}", Name ="GetPersonaje")]
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

        [HttpDelete("{personajeId:int}", Name = "RemovePersonaje")]        
        public IActionResult RemovePersonaje(int personajeId)
        {
            var personaje = _unitOfWork.Personajes.GetById(personajeId);

            _unitOfWork.Personajes.Delete(personaje);

            _unitOfWork.Save();

            return Ok();
        }

        [HttpPatch("{personajeId:int}", Name = "UpdatePersonaje")]
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
