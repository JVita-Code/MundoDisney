using ApiMundoDisney.Data;
using ApiMundoDisney.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Repositories
{
    public class PeliculaRepository : Repository<Pelicula>, IPeliculaRepository
    {
        public PeliculaRepository(ApplicationDbContext context) 
            : base(context)
        {

        }        

        public Pelicula GetPeliculaConPersonajes(int id)
        {          
            var peliculaConPersonajes = _context.Peliculas.Include(p => p.Personajes).FirstOrDefault(p => p.PeliculaId == id);

            return peliculaConPersonajes;
        }
    }
}
