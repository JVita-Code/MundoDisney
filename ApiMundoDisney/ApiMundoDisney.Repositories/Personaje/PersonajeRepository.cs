using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiMundoDisney.Data;
using ApiMundoDisney.Entities;

namespace ApiMundoDisney.Repositories
{
    public class PersonajeRepository : Repository<Personaje>, IPersonajeRepository
    {
        public PersonajeRepository(ApplicationDbContext context) 
            : base(context)
        {

        }

        //public ApplicationDbContext Context
        //{
        //    get { return Context as ApplicationDbContext; }
        //}
    }
}
