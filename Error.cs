using System;

namespace AnalizadorLexico
{
    class Error : IEquatable<Error>
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Linea { get; set; }
        public int Columna { get; set; }
        public bool Equals(Error e)
        {

            if (Linea == 0 && Columna == 0)
            {
                return this.Codigo.Equals(e.Codigo);
            }
            else
            {
                return this.Codigo.Equals(e.Codigo) && this.Columna.Equals(e.Columna) && this.Linea.Equals(e.Linea);
            }

        }
    }
}
