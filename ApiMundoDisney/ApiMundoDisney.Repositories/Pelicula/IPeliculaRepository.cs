using ApiMundoDisney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Repositories
{
    public interface IPeliculaRepository : IRepository<Pelicula>
    {
        public Pelicula GetPeliculaConPersonajes(int id);
    }
}
