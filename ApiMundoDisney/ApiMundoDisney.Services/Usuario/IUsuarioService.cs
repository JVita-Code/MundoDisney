using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Services
{
    public interface IUsuarioService
    {
        bool ExisteUsuario(int usuarioId);
    }
}
