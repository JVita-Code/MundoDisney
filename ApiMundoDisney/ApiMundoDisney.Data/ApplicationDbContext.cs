using ApiMundoDisney.Entities;
using Microsoft.EntityFrameworkCore;


namespace ApiMundoDisney.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Personaje> Personajes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }           
       
    }
}
