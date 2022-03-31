using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApiMundoDisney.Entities.Enums;

namespace ApiMundoDisney.Entities.Dtos
{
    public class PeliculaUpdateDto
    {
        public int PeliculaId { get; set; }
        public string RutaImagen { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public CalificacionPelicula Calificacion { get; set; }
        public virtual ICollection<Personaje> Personajes { get; set; }
    }
}
