using ApiMundoDisney.Data;
using ApiMundoDisney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {       
        public UsuarioRepository(ApplicationDbContext context)
            : base(context)
        {

        }        

        public bool ExisteUsuario(string usuario)
        {
            if (_context.Usuarios.Any(u => u.UsuarioA == usuario))
            {
                return true;
            }
            return false;
        }

        public Usuario Login(string usuario, string password)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.UsuarioA == usuario);

            if (user == null)
            {
                return null;
            }

            if (!VerificaPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private bool VerificaPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var hashComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < hashComputado.Length; i++)
                {
                    if (hashComputado[i] != passwordHash[i]) 
                    {
                        return false;
                    } 
                }
            }

            return true;
        }

        public Usuario Registro(Usuario usuario, string password)
        {
            byte[] passwordHash, passwordSalt;

            CrearPasswordHash(password, out passwordHash, out passwordSalt);

            usuario.PasswordHash = passwordHash;

            usuario.PasswordSalt = passwordSalt;

            _context.Usuarios.Add(usuario);

            Save();

            return usuario;
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
