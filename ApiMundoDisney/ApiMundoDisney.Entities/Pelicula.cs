using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApiMundoDisney.Entities.Enums;

namespace ApiMundoDisney.Entities
{
    public class Pelicula
    {
        [Key]
        public int PeliculaId { get; set; }
        public string RutaImagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public CalificacionPelicula Calificacion { get; set; }                  
        public virtual ICollection<Personaje> Personajes { get; set; }
        public int? GeneroId { get; set; }
        public Genero Genero { get; set; }
    }
}
