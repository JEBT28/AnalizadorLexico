using System;

namespace AnalizadorLexico
{
    class Identificador : IEquatable<Identificador>
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public String Tipo { get; set; }

        public string Valor { get; set; }


        public bool Equals(Identificador i)
        {
            return this.Nombre.Equals(i.Nombre);
        }

        public int CompareTo(Identificador i)
        {
            return this.Id.CompareTo(i.Id);
        }
    }
}
