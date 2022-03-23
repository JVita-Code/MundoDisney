using ApiMundoDisney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        bool ExisteUsuario(string usuario);
        Usuario Registro(Usuario usuario, string password);
        Usuario Login(string usuario, string password);
    }
}
