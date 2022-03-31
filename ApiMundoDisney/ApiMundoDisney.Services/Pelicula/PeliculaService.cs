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
    public class PeliculaService : IPeliculaService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public PeliculaService(IMapper mapper, IUnitOfWork unitOfWork, IPeliculaRepository peliculaRepository, IWebHostEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        public ResultadoOperacion CreatePelicula(PeliculaCreateDto peliculaCreateDto, IFormFileCollection archivos)
        {
            var respuesta = new ResultadoOperacion();

            if (peliculaCreateDto != null)
            {
                try
                {
                    var archivo = peliculaCreateDto.Foto;
                    string rutaPrincipal = _hostingEnvironment.WebRootPath;

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

                    respuesta.Mensaje = "Operación exitosa.";

                    respuesta.Resultado = Resultado.Ok;
                }
                catch (Exception ex)
                {
                    respuesta.Mensaje = ex.Message;

                    respuesta.Resultado = Resultado.Error;
                }

                return respuesta;
            }

            respuesta.Mensaje = "Error al intentar crear la película.";

            respuesta.Resultado = Resultado.Error;

            return respuesta;
        }

        public ResultadoOperacion DeletePelicula(int peliculaId)
        {
            var respuesta = new ResultadoOperacion();

            if (peliculaId != 0)
            {
                var pelicula = _unitOfWork.Peliculas.Get(peliculaId);

                if (pelicula != null)
                {

                    try
                    {
                        _unitOfWork.Peliculas.Delete(pelicula);

                        _unitOfWork.Save();

                        respuesta.Mensaje = "Operación exitosa.";

                        respuesta.Resultado = Resultado.Ok;                        
                    }
                    catch (Exception ex)
                    {
                        respuesta.Mensaje = ex.Message;

                        respuesta.Resultado = Resultado.Error;                        
                    }

                    return respuesta;
                }

                respuesta.Mensaje = "No se ha encontrado la película.";

                respuesta.Resultado = Resultado.NoEncontrado;                
            }

            return respuesta;
        }

        public PeliculaDto GetPelicula(int peliculaId)
        {                      
            try
            {               
                if (ExistePelicula(peliculaId))
                {
                    var pelicula = _unitOfWork.Peliculas.GetPeliculaConPersonajes(peliculaId);

                    var peliculaDto = _mapper.Map<PeliculaDto>(pelicula);

                    return peliculaDto;
                }

                var mensaje = "No se ha encontrado la película";
                
                return null;
            }
            catch (Exception)
            {
                var mensaje = "Ha ocurrido un error al intentar obtener la película";

                return null;
            }            
        }

        private bool ExistePelicula(int peliculaId)
        {
            var pelicula = _unitOfWork.Peliculas.Get(peliculaId);

            if (pelicula is null)
            {
                return false;
            }

            return true;
        }

        public List<PeliculaListadoDto> GetPeliculas()
        {
            try
            {
                var listadoPeliculas = _unitOfWork.Peliculas.GetAll();

                if (listadoPeliculas is null)
                {
                    return null;
                }

                var listadoPeliculasDto = new List<PeliculaListadoDto>();

                foreach (var pelicula in listadoPeliculas)
                {
                    listadoPeliculasDto.Add(_mapper.Map<PeliculaListadoDto>(pelicula));
                }

                return listadoPeliculasDto;
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;

                return null;
            }            
        }

        public ResultadoOperacion UpdatePelicula(PeliculaUpdateDto peliculaUpdateDto, int peliculaId)
        {
            var respuesta = new ResultadoOperacion();

            if (peliculaUpdateDto != null)
            {
                try
                {
                    var pelicula = _mapper.Map<Pelicula>(peliculaUpdateDto);

                    _unitOfWork.Peliculas.Update(pelicula);

                    _unitOfWork.Save();

                    respuesta.Mensaje = "Operación exitosa.";

                    respuesta.Resultado = Resultado.Ok;                    
                }
                catch (Exception ex)
                {
                    respuesta.Mensaje = ex.Message;

                    respuesta.Resultado = Resultado.Error;                    
                }

                return respuesta;
            }

            respuesta.Mensaje = "No se ha encontrado la película o se han brindado datos incorrectos.";

            respuesta.Resultado = Resultado.Error;

            return respuesta;
        }
    }
}
