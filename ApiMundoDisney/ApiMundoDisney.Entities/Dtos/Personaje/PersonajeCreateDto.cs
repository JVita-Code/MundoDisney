using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ApiMundoDisney.Entities.Dtos
{
    public class PersonajeCreateDto
    {
        public string RutaImagen { get; set; }
        public IFormFile Foto { get; set; }

        [Required]
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int Peso { get; set; }
        public string Historia { get; set; }
        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}
