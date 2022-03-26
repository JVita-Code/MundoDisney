using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiMundoDisney.Data;
using ApiMundoDisney.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiMundoDisney.Repositories
{
    public class PersonajeRepository : Repository<Personaje>, IPersonajeRepository
    {
        public PersonajeRepository(ApplicationDbContext context) 
            : base(context)
        {

        }

        public Personaje GetPersonajeConPeliculas(int id)
        {
            var peliculaConPersonajes = _context.Personajes.Include(p => p.Peliculas).FirstOrDefault(p => p.PersonajeId == id);

            return peliculaConPersonajes;
        }
    }
}
