using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Compilador.Lexico
{
    class Automata
    {
        public Automata()
        {
            //Obtencion de la informacion de la base de datos cuando se inicializa en automata
            (MatrizTransiciones, ColumnasMatriz) = new LeerCSV().ObtenerMatriz();
            ErroresLenguaje = new LeerCSV().ObtenerErrores();
        }

        public int EstadoActual { get; set; }

        public int ProximoEstado { get; set; }

        public string[,] MatrizTransiciones { get; set; }

        public List<Error> ErroresLenguaje { get; set; }

        public List<ColumnaMatriz> ColumnasMatriz { get; set; }

        public List<Identificador> TablaIdentificadores { get; set; }
        public List<ConstanteNumerica> TablaConstantesNum { get; set; }


        //Funcion que devuelve el token de la categoria segun el recorrido que vaya haciendo de la matriz, puede devolver un token, token vacio, un salto de linea 
        public string RealizarMovimiento(char caracter)
        {
            Regex saltoLinea = new Regex(@"[\n]");

            EstadoActual = ProximoEstado;

            int columnaCategoria = ColumnasMatriz.Count - 1;

            if (saltoLinea.IsMatch(caracter.ToString()))
            {
                ProximoEstado = 0;
                return "SL";
            }
            else
            {
                // Se determina la columna de la matriz que coincida con el caracter leido
                int columna = ValidarCaracter(caracter);
                // Obtiene el estado proximo
                if (columna == -1)
                {
                    ProximoEstado = 210;
                }
                else
                {
                    ProximoEstado = int.Parse(MatrizTransiciones[EstadoActual, columna].Trim());
                }

                // Regresa el token que se establezca en la columna categoria del proximo estado
                return MatrizTransiciones[ProximoEstado, columnaCategoria];
            }
        }

        //Funcion que valida el caracter leido y devuelve la columna con la que es afin
        public int ValidarCaracter(char caracter)
        {

            if (new Regex(@"[\s]").IsMatch(caracter.ToString()))
            {
                //Buscar el indice de la columna FDC
                int index = ColumnasMatriz.FindIndex(cm => cm.Simbolo == "FDC");

                return ColumnasMatriz.ElementAt(index).NumeroColumna;
            }
            else
            {
                int index = ColumnasMatriz.FindIndex(cm => cm.Simbolo.Equals(caracter.ToString().Trim()));
                return ColumnasMatriz.ElementAt(index).NumeroColumna;
            }
        }

        //Funcion que recibe el token de error y devuelve su error determinado 
        public Error IdentificarError(string error)
        {

            Error errorIdentificado = new Error { Codigo = error };
            if (ErroresLenguaje.Contains(errorIdentificado))
            {
                errorIdentificado.Descripcion = ErroresLenguaje.ElementAt(ErroresLenguaje.IndexOf(errorIdentificado)).Descripcion;
            }
            else
            {
                errorIdentificado.Descripcion = "Error desconocido";
            }
            return errorIdentificado;
        }

        //Funcion que valida que el identificador exista, si existe devuelve su numero, de lo contrario lo agrega 
        public string ValidarIdentificador(string id,string tipo)
        {

            Identificador identificador = new Identificador { Nombre = id };
            if (TablaIdentificadores.Contains(identificador))
            {
                identificador = TablaIdentificadores.ElementAt(TablaIdentificadores.IndexOf(identificador));
            }
            else
            {
                identificador.Id = TablaIdentificadores.Count + 1;
                identificador.Tipo = tipo.Trim();
                TablaIdentificadores.Add(identificador);
            }
            string numIdentificador = $"{identificador.Id.ToString("00")}.{identificador.Tipo}";
            return numIdentificador;
        }

        //Funcion que valida que la constante numerica exista, si existe devuelve su numero, de lo contrario la agrega 
        public string ValidarConstanteNumerica(string cn)
        {

            ConstanteNumerica consNum = new ConstanteNumerica { Valor = cn };
            if (TablaConstantesNum.Contains(consNum))
            {
                consNum = TablaConstantesNum.ElementAt(TablaConstantesNum.IndexOf(consNum));
            }
            else
            {
                consNum.Id = TablaConstantesNum.Count + 1;
                TablaConstantesNum.Add(consNum);
            }
            string numConsNum = consNum.Id.ToString("00");
            return numConsNum;
        }
    }
}
