using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Entities
{
    public class Genero
    {
        [Key]
        public int GeneroId { get; set; }
        public string Nombre { get; set; }
        public string RutaImagen { get; set; }
        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}
