using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    class Error:IEquatable<Error>
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Linea { get; set; }
        public int Columna { get; set; }
        public  bool Equals(Error e) => this.Codigo.Equals(e.Codigo);
    }
}
