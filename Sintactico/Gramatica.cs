using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Compilador.Sintactico
{
    class Gramatica : IEquatable<Gramatica>
    {
        public Gramatica(string raiz, Regex pilaValida)
        {
            this.Raiz = raiz;
            this.PilaValida = pilaValida;
        }
        public string Raiz { get; set; }

        public Regex PilaValida { get; set; }

        public bool Equals(Gramatica g)
        {
            return this.PilaValida.IsMatch(g.Raiz);
        }
    }
}
