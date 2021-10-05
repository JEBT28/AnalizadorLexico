using System;

namespace Compilador
{
    class ConstanteNumerica : IEquatable<ConstanteNumerica>
    {
        public int Id { get; set; }

        public string Valor { get; set; }

        public int NumeroOperador { get; set; }

        public int Jerarquia { get; set; }


        public bool Equals(ConstanteNumerica cn)
        {
            return this.Valor.Equals(cn.Valor);
        }

        public int CompareTo(ConstanteNumerica cn)
        {
            return this.Id.CompareTo(cn.Id);
        }


    }
}
