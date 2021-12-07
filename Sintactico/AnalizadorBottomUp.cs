using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Compilador.Sintactico
{
    class AnalizadorBottomUp
    {
        public AnalizadorBottomUp()
        {
            //Obtenemos las gramaticas del CSV
            this.Gramaticas = new LeerCSV().ObtenerGramaticas();
            this.ListaErrores = new LeerCSV().ObtenerErrores();
            //Inicializamos la lista donde se almacenaran las entradas derivadas
            this.DerivacionesEntradas = new List<DerivacionEntrada>();
            this.ErroresSintacticos = new List<Lexico.Error>();
            this.ErroresSemanticos = new List<Lexico.Error>();
            //Bloque para imprimir las gramaticas con su pila valida en el debugger
            foreach (var item in Gramaticas)
            {
                Debug.WriteLine("Raiz: {0} | PilaValida: {1}", item.Raiz, item.PilaValida.ToString());
            }
        }

        private List<Lexico.Error> ListaErrores { get; set; }
        private int Fila { get; set; }
        public int CorrespondenciaParentesis { get; set; }
        public int CorrespondenciaLlaves { get; set; }
        public List<Lexico.Error> ErroresSintacticos { get; set; }
        public List<Lexico.Error> ErroresSemanticos { get; set; }
        public List<Gramatica> Gramaticas { get; set; }
        public List<DerivacionEntrada> DerivacionesEntradas { get; set; }
        public int CorrespondenciaInicioFin { get; set; }
        public void Recorrido(string[] tokens)
        {
            //Recibimos los tokens de la cadena de entrada
            List<string> pilaRaices = new List<string>();
            DerivacionesEntradas = new List<DerivacionEntrada>();


            CorrespondenciaInicioFin = 0;
            Fila = tokens.Length;
            for (int i = tokens.Length - 1; i > -1; i--)
            {

                if (!String.IsNullOrWhiteSpace(tokens[i]))
                {
                    if (tokens[i].Trim().Contains(' '))
                    {
                        //Realizamos el recorrido, pasando lo tokens obtenidos
                        string resultado = DerivarEntrada(entradaLinea: tokens[i]);

                        if (resultado.Equals("ERROR"))
                        {
                            ErroresSintacticos.Add(new Lexico.Error { Codigo = "ERROR06", Descripcion = "Se esperaba otra terminacion para la instruccion. ", Linea = Fila });
                        }

                        pilaRaices.Add(resultado);
                    }
                    else
                    {

                        DerivacionesEntradas.Add(new DerivacionEntrada(tokens[i]) { Derivaciones = new List<string>() { tokens[i] } });
                        pilaRaices.Add(tokens[i]);
                    }
                }

                Fila--;
            }

            pilaRaices.Reverse();
            string res = DerivarEntrada(String.Join("", pilaRaices));
            if (res.Trim() == "S")
            {
                Debug.WriteLine("Aceptada");
            }
            else
            {
                if (CorrespondenciaInicioFin != 0)
                {
                    ErroresSemanticos.Add(new Lexico.Error { Codigo = "ERROR10", Descripcion = "No existe un balance de Inicio o Fin del programa.", Linea = 0 });
                }
                else
                {
                    ErroresSemanticos.Add(new Lexico.Error { Codigo = "ERROR10", Descripcion = "No se pudo derivar la pila de raices", Linea = 0 });
                }
                throw new Exception();

            }

            //Al finalizar el recorrido de la cadena de entrada verificamos si existe un desbalance
            //con los parentesis y llaves que no fue detectado 
            if (CorrespondenciaLlaves != 0)
            {
                ErroresSintacticos.Add(new Lexico.Error { Codigo = "ERROR10", Descripcion = "Se esperaba un " + (CorrespondenciaLlaves < 0 ? "{" : "}"), Linea = tokens.Length });
            }
            if (CorrespondenciaParentesis != 0)
            {

                ErroresSintacticos.Add(new Lexico.Error { Codigo = "ERROR09", Descripcion = "Se esperaba un " + (CorrespondenciaParentesis < 0 ? "(" : ")"), Linea = Fila });
            }

            if (ErroresSintacticos.Count > 0)
            {
                Debug.WriteLine("Rechazada");
                throw new Exception();
            }

        }

        public string DerivarEntrada(string entradaLinea)
        {

            entradaLinea = new Regex("ID[0-9]{2}").Replace(entradaLinea, "ID");
            entradaLinea = new Regex("CNE[0-9]{2}").Replace(entradaLinea, "CNE");
            entradaLinea = new Regex("CNR[0-9]{2}").Replace(entradaLinea, "CNR");

            string entradaDerivada = entradaLinea;
            while (Regex.Matches(entradaDerivada, "  ").Count != 0)
            {
                entradaDerivada = entradaDerivada.Replace("  ", " ");
            }

            CorrespondenciaParentesis += Regex.Matches(entradaLinea, "CAES02").Count;
            CorrespondenciaParentesis -= Regex.Matches(entradaLinea, "CAES13").Count;
            CorrespondenciaInicioFin += Regex.Matches(entradaLinea, "PR01").Count;
            CorrespondenciaInicioFin -= Regex.Matches(entradaLinea, "PR02").Count;

            Debug.WriteLine("CorresponednciaParentesis: " + CorrespondenciaParentesis);
            if (CorrespondenciaParentesis != 0)
            {
                ErroresSintacticos.Add(new Lexico.Error { Codigo = "ERROR09", Descripcion = "Se esperaba un " + (CorrespondenciaParentesis < 0 ? "(" : ")"), Linea = Fila });
                CorrespondenciaParentesis = 0;
                DerivacionesEntradas.Add(new DerivacionEntrada(entradaLinea) { Derivaciones = new List<string>() { "ERROR" } });
                return "ERROR";
            }

            CorrespondenciaLlaves += new Regex("CAES14").Matches(entradaLinea).Count;
            CorrespondenciaLlaves -= new Regex("CAES15").Matches(entradaLinea).Count;
            Debug.WriteLine("CorresponednciaLlaves: " + CorrespondenciaParentesis);
            if (CorrespondenciaLlaves != 0)
            {
                ErroresSintacticos.Add(new Lexico.Error { Codigo = "ERROR10", Descripcion = "Se esperaba un " + (CorrespondenciaLlaves < 0 ? "{" : "}"), Linea = 0 });
                CorrespondenciaLlaves = 0;
                DerivacionesEntradas.Add(new DerivacionEntrada(entradaLinea) { Derivaciones = new List<string>() { "ERROR" } });
                return "ERROR";
            }
            DerivacionEntrada entrada = new DerivacionEntrada(entradaLinea);
            var pilaAD = "";

            bool control = true;

            while (control)
            {

                if (pilaAD.Equals(entradaDerivada) && !entradaDerivada.Trim().Equals("SENTENCIA"))
                {
                    Debug.WriteLine("Pila antes ERROR: " + entradaDerivada);
                    entradaDerivada = "ERROR";
                    entrada.Derivaciones.Add(entradaDerivada);
                }

                pilaAD = entradaDerivada;
                if (!entradaDerivada.Equals("ERROR"))
                {
                    for (int i = Gramaticas.Count - 1; i > -1; i--)
                    {
                        var g = Gramaticas[i];

                        string pilaResultante = "";
                        while (g.PilaValida.IsMatch(entradaDerivada))
                        {
                            if (g.Raiz.Contains("ERROR"))
                            {
                                var error = ListaErrores.Find(e => e.Codigo == g.Raiz);
                                ErroresSemanticos.Add(new Lexico.Error { Codigo = error.Codigo, Descripcion = error.Descripcion, Linea = Fila });
                            }
                            pilaResultante = g.PilaValida.Replace(entradaDerivada, g.Raiz);
                            if (!entradaDerivada.Equals(pilaResultante))
                            {
                                Debug.WriteLine("Pila antes reduccion: " + entradaDerivada);
                                Debug.WriteLine("Pila reducida: " + pilaResultante);
                                entrada.Derivaciones.Add(pilaResultante);
                                entradaDerivada = pilaResultante;
                            }
                        }
                    }
                }

                if (entradaDerivada.Trim().Equals("SENTENCIA"))
                {
                    control = false;
                }
                else if (entradaDerivada.Equals("ERROR"))
                {
                    control = false;
                }
                else if (!entradaDerivada.Trim().Contains(' '))
                {
                    control = false;
                }
                else
                {
                    control = true;
                }


            }

            DerivacionesEntradas.Add(entrada);
            return entradaDerivada;
        }
    }

}
