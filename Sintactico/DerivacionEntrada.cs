using System;
using System.Collections.Generic;

namespace Compilador.Sintactico
{
    class DerivacionEntrada
    {
        public DerivacionEntrada(string entrada)
        {
            this.Entrada = entrada;
            this.Derivaciones = new List<String>();
        }
        public string Entrada { get; set; }
        public List<string> Derivaciones { get; set; }
    }
}
