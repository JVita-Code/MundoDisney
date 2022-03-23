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
            CreateMap<Usuario, UsuarioDto>().ReverseMap();            
        }
    }
}
