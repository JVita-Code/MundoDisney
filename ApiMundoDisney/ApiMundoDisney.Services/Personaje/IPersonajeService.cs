using ApiMundoDisney.Entities;
using ApiMundoDisney.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Services
{
    public interface IPersonajeService
    {
        List<PersonajeListadoDto> GetPersonajes();
        PersonajeDto GetPersonaje(int personajeId);        

        ResultadoOperacion CreatePersonaje(PersonajeCreateDto personajeCreateDto, IFormFileCollection archivos);
        ResultadoOperacion DeletePersonaje(int personajeId);
        ResultadoOperacion UpdatePersonaje(PersonajeUpdateDto personajeUpdateDto, int personajeId);
    }
}
