using Compilador.Lexico;

namespace Compilador.Sintactico
{
    class ErrorSintactico
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
                return this.Codigo.Equals(e.Codigo) && this.Linea.Equals(e.Linea);
            }

        }

    }
}
