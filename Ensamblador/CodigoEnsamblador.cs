using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Compilador.Ensamblador
{
    public class CodigoEnsamblador
    {
        public CodigoEnsamblador() { }

        public CodigoEnsamblador(List<Lexico.Identificador> TablaIdentificadores)
        {
            this.TablaIdentificadores = TablaIdentificadores;

            CodigoASM = "code segment\n" +
                        "assume cs:code, ds:code, ss:code\n" +
                        "org 100h\n";

            CargarIdentificadores(TablaIdentificadores);

            Si = false;
            mientras = false;
            Entonces = false;
            parLlaves = 0;
            auxCodigo = "";
        }

        public string CodigoASM { get; set; }

        public string auxCodigo { get; set; }

        public List<Lexico.Identificador> TablaIdentificadores { get; set; }

        public bool Si { get; set; }
        public bool Entonces { get; set; } 

        public string controlInstruccion { get; set; }

        public int siContador { set; get; }

        public bool mientras { get; set; }

        public int parLlaves { get; set; }
        public void AgregarInstruccion(string cadena)
        {
            if (Si && !Entonces)
            {
                if (!auxCodigo.Contains("sino"))
                {
                    if (cadena.Trim().Equals("{"))
                    {
                        Debug.WriteLine("Escribo if");
                        parLlaves++;
                        auxCodigo += $"\n\nsi{siContador}:\n";
                    }
                    else if (cadena.Trim().Equals("}"))
                    {
                        Debug.WriteLine("cierro if");
                        parLlaves--;
                        auxCodigo += $"jmp cont{siContador}\n\n";
                        Entonces = true;
                    }
                    else
                    {
                        Debug.WriteLine("Operacion dentro de if");
                        auxCodigo += ConvertirInstruccion(cadena);
                        Debug.WriteLine(auxCodigo);
                    }
                }                
            }

            else if(Si && Entonces)
            {
                if (cadena.Trim().Equals("{"))
                {
                    Debug.WriteLine("Escribo else");
                    parLlaves++;
                    auxCodigo += $"\n\nsino{siContador}:\n";
                }
                else if (cadena.Trim().Equals("}"))
                {
                    Debug.WriteLine("Cierro else");
                    parLlaves--;
                    auxCodigo += $"jmp cont{siContador}\n\n";
                    if (parLlaves == 0)
                    {
                        int inicioCondicion = controlInstruccion.Split(' ').ToList().IndexOf("(");
                        string[] tokens = controlInstruccion.Split(' ');

                        string destino = tokens[inicioCondicion + 1];

                        string fuente = tokens[inicioCondicion + 3];

                        string op = tokens[inicioCondicion + 2];

                        string cmp = "";

                        if (new Regex("[a-z]{1}[a-zA-Z]*").IsMatch(destino))
                        {
                            if (new Regex("[a-z]{1}[a-zA-Z]*").IsMatch(fuente))
                            {

                                cmp = $"mov al,{destino}\n" +
                                  $"mov bl, {fuente}\n" +
                                  $"cmp al, bl \n";
                               

                            }
                            else
                            {
                                cmp = $"mov al,{destino}\n" +
                                  $"mov bl, {ConversionDec2Hex(int.Parse(fuente.Trim()))}\n" +
                                  $"cmp al, bl \n";

                            }

                        }
                        else
                        {
                            if (new Regex("[a-z]{1}[a-zA-Z]*").IsMatch(fuente))
                            {

                                cmp = $"mov al, {ConversionDec2Hex(int.Parse(destino.Trim()))}\n" +
                                    $"mov bl, {fuente}\n" +
                                    $"cmp al, bl \n";

                            }
                            else
                            {
                                cmp = $"mov al, {ConversionDec2Hex(int.Parse(destino.Trim()))}\n" +
                                  $"mov bl, {ConversionDec2Hex(int.Parse(fuente.Trim()))}\n" +
                                  $"cmp al, bl \n";

                            }
                        }


                        switch (op)
                        {
                            case ">":
                                auxCodigo = cmp + $"JG si{siContador} \n" + (auxCodigo.Contains("sino") ? $"JLE sino{siContador} \n" : "") + auxCodigo;

                                break;
                            case "<":
                                auxCodigo = cmp + $"JL si{siContador} \n" + (auxCodigo.Contains("sino") ? $"JGE sino{siContador} \n" : "") + auxCodigo;
                                break;
                            case "<=":
                                auxCodigo = cmp + $"JLE si{siContador} \n" + (auxCodigo.Contains("sino") ? $"JG sino{siContador} \n" : "") + auxCodigo;
                                break;
                            case ">=":
                                auxCodigo = cmp + $"JGE si{siContador} \n" + (auxCodigo.Contains("sino") ? $"JL sino{siContador} \n" : "") + auxCodigo;
                                break;
                            case "==":
                                auxCodigo = cmp + $"JE si{siContador} \n" + (auxCodigo.Contains("sino") ? $"JNE sino{siContador} \n" : "") + auxCodigo;
                                break;
                            case "<>":
                                auxCodigo = cmp + $"JNE si{siContador} \n" + (auxCodigo.Contains("sino") ? $"JE sino{siContador} \n" : "") + auxCodigo;
                                break;
                        }

                        CodigoASM += auxCodigo + $"cont{siContador}:\n";
                        Si = false;
                        mientras = false;
                        parLlaves = 0;
                        auxCodigo = "";
                    }
                   
                }
                else
                {
                    Debug.WriteLine("Operacion dentro de else");
                    auxCodigo += ConvertirInstruccion(cadena);
                    Debug.WriteLine(auxCodigo);
                }
            }
            else
            {
                CodigoASM += ConvertirInstruccion(cadena);
            }
        }

        private string ConvertirInstruccion(string cadena)
        {

            if (cadena.Contains("Inicio"))
            {
                return "main proc\nmov ax,cs\nmov ds,ax\n";
            }
            else if (cadena.Contains("Fin"))
            {
                return "mov ah,08\nint 21h\nmain endp\ncode ends\nend main";
            }
            else if (cadena.Contains("Clear"))
            {
                return "mov ah,0Fh\nint 10h\nmov ah,0\nint 10h\n";
            }
            else if (cadena.Contains("Escribir"))
            {
                Debug.WriteLine("Imprimir");
                if (new Regex("\\(\\s[a-z]{1}[a-zA-Z0-9]*\\s\\)").Matches(cadena.Trim()).Count > 0)
                {
                    Debug.WriteLine("Imprimir ID");
                    string aux = cadena.Split(' ')[cadena.Split(' ').ToList().IndexOf("(")+1];

                    Lexico.Identificador iden = TablaIdentificadores.Where(i => i.Nombre == aux).FirstOrDefault();

                    Debug.WriteLine(iden.Nombre);

                    if (iden.Tipo == "Cade")
                    {
                        return "lea dx," + iden.Nombre + "\nmov ah,09h\nint 21h\n";
                    }
                    else if (iden.Tipo == "Car" || iden.Tipo == "Ent")
                    {
                        if (iden.Tipo == "Ent")
                        {
                            return "mov al, " + iden.Nombre + "\naam\nadd ax, 3030h\npush ax\nmov dl, ah\nmov ah, 02h\nint 21h\npop dx\nmov ah, 02h\nint 21h\n";
                        }
                        else
                        {
                            return "mov ah, 2\nmov dl, " + iden.Nombre + "\nint 21h\n";
                        }
                    }
                }
                else
                {

                    if (new Regex("\\(\\s[0-9]*\\s\\)").Matches(cadena.Trim()).Count > 0)
                    {
                        string aux = cadena.Split(' ').ToList().ElementAt(cadena.Split(' ').ToList().IndexOf("(") + 1);
                        Debug.WriteLine("Imprimo un numero");
                        return "mov al, " + ConversionDec2Hex(int.Parse(aux.Trim())) + "h\naaa\nadd ax, 3030h\npush ax\nmov dl, ah\nmov ah, 02h\nint 21h\npop dx\nmov ah, 02h\nint 21h\n";
                    }
                    else if (new Regex("\\(\\s'[a-zA-Z0-9]{1}'\\s\\)").Matches(cadena.Trim()).Count > 0)
                    {
                        string aux = cadena.Split(' ').ToList().ElementAt(cadena.Split(' ').ToList().IndexOf("(") + 1);
                        return "mov ah, 2\nmov dl, " + (int)char.Parse(aux.Substring(1, 1)) + "\nint 21h\n";
                    }
                    else if (new Regex("\\(\\s([0-9]*|[a-z]{1}[a-zA-Z1-9]*){1}\\s(\\+|-){1}\\s([0-9]*|[a-z]{1}[a-zA-Z1-9]*)\\s\\)").Matches(cadena.Trim()).Count > 0)
                    {
                        var tokens = cadena.Trim().Split(' ').Take(cadena.Trim().Split(' ').Length - 1).ToArray();
                        int indPar = tokens.ToList().IndexOf("(") + 1;


                        var aux = "";
                        while (indPar < tokens.Length)
                        {

                            if (new Regex("(\\+|-){1}").IsMatch(tokens[indPar]))
                            {
                                aux += $"{(tokens[indPar].Trim().Equals("+") ? "add" : "sub")} al, {(new Regex("[a-z]{1}[a-zA-Z0-9]*").IsMatch(tokens[indPar + 1]) ? TablaIdentificadores.Where(id => tokens[indPar + 1] == id.Nombre).FirstOrDefault().Nombre : ConversionDec2Hex(int.Parse(tokens[indPar + 1].Trim())))}\n";
                                indPar++;
                            }
                            else
                            {
                                aux += $"mov al, {(new Regex("[a-z]{1}[a-zA-Z0-9]*").IsMatch(tokens[indPar]) ? TablaIdentificadores.Where(id => tokens[indPar] == id.Nombre).FirstOrDefault().Nombre : ConversionDec2Hex(int.Parse(tokens[indPar].Trim())))}\n";
                            }
                            indPar++;
                        }
                        aux += "\naam\nadd ax, 3030h\npush ax\nmov dl, ah\nmov ah, 02h\nint 21h\npop dx\nmov ah, 02h\nint 21h\nmov dl,0ah\nint 21h\n";

                        return aux;
                    }
                }
            }
            else if (cadena.Contains("Leer"))
            {

                string aux = cadena.Trim().Split(' ').ToList().ElementAt(cadena.Trim().Split(' ').ToList().IndexOf("(") + 1);
                Lexico.Identificador id = TablaIdentificadores.Where(i => i.Nombre == aux).FirstOrDefault();
                if (id != null)
                {
                    if (id.Tipo.Equals("Car"))
                    {
                        return "mov ah,1\nint 21h\nmov " + id.Nombre + ",al";
                    }
                    else if (id.Tipo.Equals("Cade"))
                    {
                        return "mov si,00h\nleer:\nmov ax,0000\nmov ah,01h\nint 21h\nmov " + id.Nombre + "[si], al\ninc si\ncmp al,0dh\nja leer\njb leer\n\n";
                    }
                }
            }
            else if (cadena.Contains("Si"))
            {
                Si = true;
                controlInstruccion = cadena;
            }
            return "";
        }

        private void CargarIdentificadores(List<Lexico.Identificador> TablaIdentificadores)
        {
            foreach (Lexico.Identificador identificador in TablaIdentificadores)
            {
                string varAux = identificador.Nombre + " db";
                if (identificador.Valor != null && identificador.Valor != "")
                {
                    if (identificador.Tipo.Equals("Ent") || identificador.Tipo.Equals("Flot"))
                    {
                        varAux = varAux + " " + ConversionDec2Hex(int.Parse(identificador.Valor)) + "h";
                    }
                    else if (identificador.Tipo.Equals("Cade") || identificador.Tipo.Equals("Car"))
                    {
                        varAux = varAux + " 0DH,0AH, " + identificador.Valor + ",'$'";
                    }
                    else if (identificador.Tipo.Equals("Bool"))
                    {
                        if (identificador.Valor.Equals("Verdad"))
                        {
                            varAux = varAux + " 1d";
                        }
                        else
                        {
                            varAux = varAux + " 0d";
                        }
                    }
                }
                else
                {
                    if (identificador.Tipo == "Cade")
                    {
                        varAux = varAux + " 255 DUP ('$')";
                    }
                    else
                    {
                        varAux = varAux + " ?";
                    }
                }
                CodigoASM += $"{ varAux}\n";

            }

        }

        private string ConversionDec2Hex(int numero)
        {
            string hex = numero.ToString("X");
            return hex.Length < 2 ? "0" + hex : hex;

        }

    }
}
