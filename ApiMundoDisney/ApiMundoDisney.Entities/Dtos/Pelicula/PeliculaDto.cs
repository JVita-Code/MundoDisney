using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApiMundoDisney.Entities.Enums;

namespace ApiMundoDisney.Entities.Dtos
{
    public class PeliculaDto
    {
        public string RutaImagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public CalificacionPelicula Calificacion { get; set; }
        public int? GeneroId { get; set; }
        public virtual ICollection<Personaje> Personajes { get; set; }
    }
}
