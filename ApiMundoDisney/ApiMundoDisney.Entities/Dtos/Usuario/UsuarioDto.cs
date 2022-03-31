using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Entities.Dtos
{
    public class UsuarioDto
    {
        public string UsuarioA { get; set; }
        public byte[] PasswordHash { get; set; }
        //public string Email { get; set; }
    }
}
