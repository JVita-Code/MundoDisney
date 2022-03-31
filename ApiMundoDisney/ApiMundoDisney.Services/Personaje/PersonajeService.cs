using ApiMundoDisney.Entities;
using ApiMundoDisney.Entities.Dtos;
using ApiMundoDisney.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Services
{
    public class PersonajeService : IPersonajeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PersonajeService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        public ResultadoOperacion CreatePersonaje(PersonajeCreateDto personajeCreateDto, IFormFileCollection archivos)
        {
            var respuesta = new ResultadoOperacion();
            
            if (personajeCreateDto != null)
            {
                try
                {
                    var archivo = personajeCreateDto.Foto;
                    string rutaPrincipal = _hostingEnvironment.WebRootPath;                    

                    if (archivo.Length > 0)
                    {
                        var nombreImagen = Guid.NewGuid().ToString();
                        var subidas = Path.Combine(rutaPrincipal, @"imagenesPersonajes");
                        var extension = Path.GetExtension(archivos[0].FileName);

                        using (var fileStreams = new FileStream(Path.Combine(subidas, nombreImagen + extension), FileMode.Create))
                        {
                            archivos[0].CopyTo(fileStreams);
                        }

                        personajeCreateDto.RutaImagen = @"\imagenesPersonajes\" + nombreImagen + extension;
                    }

                    var personaje = _mapper.Map<Personaje>(personajeCreateDto);

                    _unitOfWork.Personajes.Create(personaje);

                    _unitOfWork.Save();

                    respuesta.Mensaje = "Operación exitosa";

                    respuesta.Resultado = Resultado.Ok;                    
                }
                catch (Exception ex)
                {
                    respuesta.Mensaje = ex.Message;

                    respuesta.Resultado = Resultado.Error;                    
                }

                return respuesta;
            }

            respuesta.Mensaje = "Error al intentar crear el personaje";

            respuesta.Resultado = Resultado.Error;

            return respuesta;           
        }

        public ResultadoOperacion DeletePersonaje(int personajeId)
        {
            var respuesta = new ResultadoOperacion();

            if (personajeId != 0)
            {
                var personaje = _unitOfWork.Personajes.Get(personajeId);

                if (personaje != null)
                {

                    try
                    {
                        _unitOfWork.Personajes.Delete(personaje);

                        _unitOfWork.Save();

                        respuesta.Mensaje = "Operación exitosa";

                        respuesta.Resultado = Resultado.Ok;

                        return respuesta;
                    }
                    catch (Exception ex)
                    {

                        respuesta.Mensaje = ex.Message;

                        respuesta.Resultado = Resultado.Error;

                        return respuesta;
                    }
                }

                respuesta.Mensaje = "No se ha encontrado el personaje";

                respuesta.Resultado = Resultado.NoEncontrado;

                return respuesta;
            }            

            return respuesta;
        }

        public PersonajeDto GetPersonaje(int personajeId)
        {
            try
            {
                if (ExistePersonaje(personajeId))
                {
                    var personaje = _unitOfWork.Personajes.GetPersonajeConPeliculas(personajeId);

                    var personajeDto = _mapper.Map<PersonajeDto>(personajeId);

                    return personajeDto;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PersonajeListadoDto> GetPersonajes()
        {
            try
            {
                var listadoPersonajes = _unitOfWork.Personajes.GetAll();

                if (listadoPersonajes != null)
                {
                    var listadoPersonajesDto = new List<PersonajeListadoDto>();

                    foreach (var personaje in listadoPersonajes)
                    {
                        listadoPersonajesDto.Add(_mapper.Map<PersonajeListadoDto>(personaje));
                    }

                    return listadoPersonajesDto;
                }

                return null;
                
            }
            catch (Exception)
            {
                var mensaje = "Ha ocurrido un error al intentar obtener el listado de Personajes";

                return null;
            }
        }

        public ResultadoOperacion UpdatePersonaje(PersonajeUpdateDto personajeUpdateDto, int personajeId)
        {
            var respuesta = new ResultadoOperacion();

            if (personajeUpdateDto != null)
            {
                try
                {
                    var personaje = _mapper.Map<Personaje>(personajeUpdateDto);

                    _unitOfWork.Personajes.Update(personaje);

                    _unitOfWork.Save();

                    respuesta.Mensaje = "Operación exitosa";

                    respuesta.Resultado = Resultado.Ok;                    
                }
                catch (Exception ex)
                {
                    respuesta.Mensaje = ex.Message;

                    respuesta.Resultado = Resultado.Error;                    
                }

                return respuesta;
            }

            respuesta.Mensaje = "No se ha encontrado el personaje o se han brindado datos incorrectos";

            respuesta.Resultado = Resultado.Error;

            return respuesta;
        }

        private bool ExistePersonaje(int personajeId)
        {
            var personaje = _unitOfWork.Personajes.Get(personajeId);

            if (personaje is null)
            {
                return false;
            }
            return true;
        }
    }
}
