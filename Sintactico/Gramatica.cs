using System;
using System.Text.RegularExpressions;

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
