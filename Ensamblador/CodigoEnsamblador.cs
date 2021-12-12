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
            hacer = false;
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
        public int mientrasContador { get; set; }
        public int hacerContador { get; set; }

        public bool mientras { get; set; }
        public bool hacer { get; set; }

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
                    else if (cadena.Contains("Romper"))
                    {
                        if (mientras)
                        {
                            auxCodigo += $"jmp contMient{mientrasContador}";
                        }
                        else if (hacer)
                        {
                            auxCodigo += $"jmp contHacer{hacerContador}";
                        }

                    }
                    else
                    {
                        Debug.WriteLine("Operacion dentro de if");
                        auxCodigo += ConvertirInstruccion(cadena);
                        Debug.WriteLine(auxCodigo);
                    }
                }
            }

            else if (Si && Entonces)
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
                        Entonces = false;
                        parLlaves = 0;
                    }

                }
                else
                {
                    Debug.WriteLine("Operacion dentro de else");
                    auxCodigo += ConvertirInstruccion(cadena);
                    Debug.WriteLine(auxCodigo);
                }
            }
            else if (mientras)
            {
                if (cadena.Trim().Equals("{"))
                {
                    Debug.WriteLine("Escribo while");
                    parLlaves++;

                }
                else if (cadena.Trim().Equals("}"))
                {
                    Debug.WriteLine("Cierro mientras");
                    parLlaves--;
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
                                auxCodigo = $"jmp mientras{mientrasContador}\n\n\nmientras{mientrasContador}:\n" + cmp + $"JLE contMient{mientrasContador} \n" + auxCodigo;

                                break;
                            case "<":
                                auxCodigo = $"jmp mientras{mientrasContador}\n\n\nmientras{mientrasContador}:\n" + cmp + $"JGE contMient{mientrasContador} \n" + auxCodigo;
                                break;
                            case "<=":
                                auxCodigo = $"jmp mientras{mientrasContador}\n\n\nmientras{mientrasContador}:\n" + cmp + $"JG contMient{mientrasContador} \n" + auxCodigo;
                                break;
                            case ">=":
                                auxCodigo = $"jmp mientras{mientrasContador}\n\n\nmientras{mientrasContador}:\n" + cmp + $"JL contMient{mientrasContador} \n" + auxCodigo;
                                break;
                            case "==":
                                auxCodigo = $"jmp mientras{mientrasContador}\n\n\nmientras{mientrasContador}:\n" + cmp + $"JNE contMient{mientrasContador} \n" + auxCodigo;
                                break;
                            case "<>":
                                auxCodigo = $"jmp mientras{mientrasContador}\n\n\nmientras{mientrasContador}:\n" + cmp + $"JE contMient{mientrasContador} \n" + auxCodigo;
                                break;
                        }

                        CodigoASM += auxCodigo + $"jmp mientras{mientrasContador} \n\n\ncontMient{mientrasContador}:\n";
                        Si = false;
                        mientras = false;
                        parLlaves = 0;
                        auxCodigo = "";
                    }

                }
            }
            else if (hacer)
            {
                if (cadena.Trim().Equals("{"))
                {
                    Debug.WriteLine("Escribo do");
                    parLlaves++;
                    auxCodigo = $"jmp hacer{mientrasContador}\n\n\nhacer{mientrasContador}:\n";

                }
                else if (cadena.Trim().Equals("}"))
                {
                    Debug.WriteLine("Cierro do");
                    parLlaves--;
                }
                else if (cadena.Contains("Mientras"))
                {
                    if (parLlaves == 0)
                    {

                        controlInstruccion = cadena;
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
                                cmp += $"JLE contHacer{hacerContador} \n";

                                break;
                            case "<":
                                cmp += $"JGE contHacer{hacerContador} \n";
                                break;
                            case "<=":
                                cmp += $"JG contHacer{hacerContador} \n";
                                break;
                            case ">=":
                                cmp += $"JL contHacer{hacerContador} \n";
                                break;
                            case "==":
                                cmp += $"JNE contHacer{hacerContador} \n";
                                break;
                            case "<>":
                                cmp += $"JE contHacer{hacerContador} \n";
                                break;
                        }

                        CodigoASM += auxCodigo + cmp + $"jmp hacer{hacerContador} \n\n\ncontHacer{hacerContador}:\n";
                        hacer = false;
                        parLlaves = 0;
                        auxCodigo = "";
                    }

                }
                else
                {
                    Debug.WriteLine("Operacion dentro de while");
                    auxCodigo += ConvertirInstruccion(cadena);
                    Debug.WriteLine(auxCodigo);
                }
            }
            else
            {

                Debug.WriteLine("Agregar linea");
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
                    string aux = cadena.Split(' ')[cadena.Split(' ').ToList().IndexOf("(") + 1];

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
                            return "mov al, " + iden.Nombre + "\naam\nadd ax, 3030h\npush ax\nmov dl, ah\nmov ah, 02h\nint 21h\npop dx\nmov ah, 02h\nint 21h\n MOV DL,13\nINT 21h\nMOV DL,10\n INT 21h\n";
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
            else if (cadena.Contains("Mientras"))
            {
                mientras = true;
                controlInstruccion = cadena;
                auxCodigo = "";
            }
            else if (cadena.Contains("Hacer"))
            {
                hacer = true;
                auxCodigo = "";
            }
            else if (new Regex("([a-z]{1}[a-zA-Z0-9]*)\\s=\\s([a-z]{1}[a-zA-Z0-9]*|[0-9]+|\"[a-zA-Z0-9]*\")+(\\s(|\\+|-|\\/|\\*|){1}\\s([a-z]{1}[a-zA-Z0-9]*|[0-9]+|\"[a-zA-Z0-9]*\")+)*").Matches(cadena).Count > 0)
            {
                Debug.WriteLine("Asignacion");
                string[] tokens = cadena.Trim().Split(' ');
                int tokenAsignacion = tokens.ToList().IndexOf("=");

                if (tokenAsignacion - 2 >= 0 && new Regex("(Ent|Bool|Flot|Cade|Car)").IsMatch(tokens[tokenAsignacion - 2]))
                {
                    return "";
                }

                string codigo = "";

                for (int i = tokenAsignacion + 1; i < tokens.Length; i++)
                {
                    if (tokenAsignacion + 1 == tokens.Length)
                    {
                        if (new Regex("[a-z]{1}[a-zA-Z0-9]*").IsMatch(tokens[i]))
                        {
                            return $"mov {tokens[0]}, {tokens[i]}\n";
                        }
                        else if (new Regex("[0-9]+").IsMatch(tokens[i]))
                        {
                            Debug.WriteLine("Asignar numero");
                            return $"mov {tokens[0]}, {ConversionDec2Hex(int.Parse(tokens[i]))}\n";
                        }
                        else if (new Regex("(Verdad|Falso)").IsMatch(tokens[i]))
                        {
                            if (tokens[i].Contains("Verdad"))
                            {
                                return $"mov {tokens[0]}, {ConversionDec2Hex(1)}\n";
                            }
                            else
                            {
                                return $"mov {tokens[0]}, {ConversionDec2Hex(0)}\n";
                            }
                        
                        }
                    }
                    else
                    {
                        if (new Regex("[a-z]{1}[a-zA-Z0-9]*").IsMatch(tokens[i]))
                        {
                            codigo += $"mov al, {tokens[i]}\n";
                        }
                        else if (new Regex("[0-9]+").IsMatch(tokens[i]))
                        {
                            codigo += $"mov al, {ConversionDec2Hex(int.Parse(tokens[i]))}\n";
                        }
                        else if (new Regex("(\\+|-|/|\\*)").IsMatch(tokens[i]))
                        {
                            switch (tokens[i])
                            {

                                case "+":
                                    codigo += $"ADC al, "; if (new Regex("[a-z]{1}[a-zA-Z0-9]*").IsMatch(tokens[i] + 1))
                                    {
                                        codigo += $"{tokens[i + 1]}\n";
                                    }
                                    else if (new Regex("[0-9]+").IsMatch(tokens[i + 1]))
                                    {
                                        codigo += $"{ConversionDec2Hex(int.Parse(tokens[i + 1]))}\n";
                                    }; break;
                                case "-":
                                    codigo += $"SBB al, ";
                                    if (new Regex("[a-z]{1}[a-zA-Z0-9]*").IsMatch(tokens[i] + 1))
                                    {
                                        codigo += $"{tokens[i + 1]}\n";
                                    }
                                    else if (new Regex("[0-9]+").IsMatch(tokens[i + 1]))
                                    {
                                        codigo += $"{ConversionDec2Hex(int.Parse(tokens[i + 1]))}\n";
                                    }
                                    break;
                                case "/":
                                    if (new Regex("[a-z]{1}[a-zA-Z0-9]*").IsMatch(tokens[i] + 1))
                                    {
                                        codigo += $"mov bl, {tokens[i + 1]}\n";
                                    }
                                    else if (new Regex("[0-9]+").IsMatch(tokens[i + 1]))
                                    {
                                        codigo += $"mov bl, {ConversionDec2Hex(int.Parse(tokens[i + 1]))}\n";
                                    }
                                    codigo += $"DIV bl \n"; break;
                                case "*":
                                    if (new Regex("[a-z]{1}[a-zA-Z0-9]*").IsMatch(tokens[i] + 1))
                                    {
                                        codigo += $"mov bl, {tokens[i + 1]}\n";
                                    }
                                    else if (new Regex("[0-9]+").IsMatch(tokens[i + 1]))
                                    {
                                        codigo += $"mov bl, {ConversionDec2Hex(int.Parse(tokens[i + 1]))}\n";
                                    }
                                    codigo += $"MUL bl \n"; break;
                            }
                            i++;
                        }
                    }
                }
                return codigo + $"mov {tokens[0]}, al\n";
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
                        varAux += " " + ConversionDec2Hex(int.Parse(identificador.Valor));
                    }
                    else if (identificador.Tipo.Equals("Cade") || identificador.Tipo.Equals("Car"))
                    {
                        varAux += " 0DH,0AH, " + identificador.Valor + ",'$'";
                    }
                    else if (identificador.Tipo.Equals("Bool"))
                    {
                        if (identificador.Valor.Equals("Verdad"))
                        {
                            varAux += " 1h";
                        }
                        else
                        {
                            varAux += " 0h";
                        }
                    }
                }
                else
                {
                    if (identificador.Tipo == "Cade")
                    {
                        varAux += " 255 DUP ('$')";
                    }
                    else
                    {
                        varAux += " ?";
                    }
                }
                CodigoASM += $"{ varAux}\n";

            }

        }

        private string ConversionDec2Hex(int numero)
        {
            string hex = numero.ToString("X");
            if (!(hex[hex.Length - 1] == 'h' || hex[hex.Length - 1] == 'H'))
            {
                hex += 'h';
            }
            return hex.Length < 3 ? "0" + hex : hex;

        }

    }
}
