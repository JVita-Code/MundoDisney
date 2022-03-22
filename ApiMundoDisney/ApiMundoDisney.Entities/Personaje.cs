using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Entities
{
    public class Personaje
    {
        [Key]
        public int PersonajeId { get; set; }
        public string RutaImagen { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int Peso { get; set; }
        public string Historia { get; set; }
        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}
