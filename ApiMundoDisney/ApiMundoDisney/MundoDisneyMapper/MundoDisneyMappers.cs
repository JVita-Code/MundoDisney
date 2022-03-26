using ApiMundoDisney.Entities;
using ApiMundoDisney.Entities.Dtos;
using AutoMapper;

namespace ApiMundoDisney.MundoDisneyMapper
{
    public class MundoDisneyMappers : Profile
    {
        public MundoDisneyMappers()
        {
            CreateMap<Personaje, PersonajeDto>().ReverseMap();
            CreateMap<Personaje, PersonajeListadoDto>().ReverseMap();
            CreateMap<Personaje, PersonajeUpdateDto>().ReverseMap();
            CreateMap<Personaje, PersonajeCreateDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();

            CreateMap<Pelicula, PeliculaDto>().ReverseMap();
            CreateMap<Pelicula, PeliculaListadoDto>().ReverseMap();
            CreateMap<Pelicula, PeliculaUpdateDto>().ReverseMap();
            CreateMap<Pelicula, PeliculaCreateDto>().ReverseMap();
        }
    }
}
