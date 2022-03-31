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
    public interface IPeliculaService
    {
        List<PeliculaListadoDto> GetPeliculas();
        PeliculaDto GetPelicula(int peliculaId);
        
        //PeliculaCreateDto CreatePelicula(PeliculaCreateDto peliculaCreateDto, IFormFileCollection archivos);
        //bool DeletePelicula(int peliculaId);
        //List<Pelicula> UpdatePelicula();

        ResultadoOperacion CreatePelicula(PeliculaCreateDto peliculaCreateDto, IFormFileCollection archivos);
        ResultadoOperacion DeletePelicula(int peliculaId);
        ResultadoOperacion UpdatePelicula(PeliculaUpdateDto peliculaUpdateDto, int peliculaId);
    }
}
