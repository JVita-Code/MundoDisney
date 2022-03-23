using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Repositories
{
    public interface IUnitOfWork
    {
        IPersonajeRepository Personajes { get; }
        IUsuarioRepository Usuarios { get; }

        void Save();
    }
}
