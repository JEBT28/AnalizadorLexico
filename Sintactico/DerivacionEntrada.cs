using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Sintactico
{
    class DerivacionEntrada
    {
        public DerivacionEntrada(string entrada) { 
                                               this.Entrada = entrada;
            this.Derivaciones = new List<String>();
        }
        public string Entrada { get; set; }
        public List<string> Derivaciones { get; set; }
    }
}
