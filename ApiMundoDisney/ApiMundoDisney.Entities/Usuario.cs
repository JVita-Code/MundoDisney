using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Entities
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string UsuarioA { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
