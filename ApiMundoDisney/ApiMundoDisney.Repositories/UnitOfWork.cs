using ApiMundoDisney.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private IPersonajeRepository _personajeRepository;
        private IUsuarioRepository _usuarioRepository;

        public IPersonajeRepository Personajes
        {
            get
            {
                if (_personajeRepository == null)
                {
                    _personajeRepository = new PersonajeRepository(_context);
                }

                return _personajeRepository;
            }
        }

        public IUsuarioRepository Usuarios
        {
            get
            {
                if(_usuarioRepository == null)
                {
                    _usuarioRepository = new UsuarioRepository(_context);
                }
                return _usuarioRepository;
            }
        }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }        
    }
}
