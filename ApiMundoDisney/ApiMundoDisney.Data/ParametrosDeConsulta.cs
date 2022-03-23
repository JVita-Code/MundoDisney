using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Data
{
    public class ParametrosDeConsulta<T>
    {
        public ParametrosDeConsulta(int pagina, int top)
        {
            Pagina = pagina;
            Top = top;
            Where = null;
            OrderBy = null;
            OrderByDescending = null;
        }

        public int Pagina { get; set; }
        public int Top { get; set; }
        public Expression<Func<T, bool>> Where { get; set; }
        public Func<T, object> OrderBy { get; set; }
        public Func<T, object> OrderByDescending { get; set; }
    }
}
