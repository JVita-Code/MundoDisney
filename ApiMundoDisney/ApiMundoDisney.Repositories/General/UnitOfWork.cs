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
        private IPeliculaRepository _peliculaRepository;
       
        public IPersonajeRepository Personajes => _personajeRepository = _personajeRepository ?? new PersonajeRepository(_context);
        public IUsuarioRepository Usuarios => _usuarioRepository = _usuarioRepository ?? new UsuarioRepository(_context);
        public IPeliculaRepository Peliculas => _peliculaRepository = _peliculaRepository ?? new PeliculaRepository(_context);


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
