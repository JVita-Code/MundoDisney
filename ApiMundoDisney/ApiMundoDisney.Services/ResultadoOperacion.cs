using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Services
{
    public enum Resultado
    {
        Ok = 1,

        Error = 2,

        NoEncontrado = 3
    }
    public class ResultadoOperacion
    {
        public string Mensaje { get; set; }
        public Resultado Resultado { get; set; }
    }
}
