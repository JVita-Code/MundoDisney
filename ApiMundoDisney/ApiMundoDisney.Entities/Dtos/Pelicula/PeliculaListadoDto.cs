using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Entities.Dtos
{
    public class PeliculaListadoDto
    {
        public string RutaImagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }        
    }
}
