using System;

public static class JunkCode
{
    private static Random rnd = new Random();

    public static string GenerarDeclaracionVariable()
    {
        string tipo;
        int tipoRandom = rnd.Next(0, 6); // Tipos de variables más variados

        switch (tipoRandom)
        {
            case 0: tipo = "int"; break;
            case 1: tipo = "string"; break;
            case 2: tipo = "bool"; break;
            case 3: tipo = "double"; break;
            case 4: tipo = "long"; break;
            case 5: tipo = "float"; break; // Añadido tipo float
            default: tipo = "int"; break;
        }

        string nombreVar = RandomGenerator.GenerarNombreAleatorio();
        string valor = GenerarValorVariable(tipo);
        return $"{tipo} {nombreVar} = {valor};";
    }

    public static string GenerarLlamadaMetodo()
    {
        int methodType = rnd.Next(0, 5); // Añadido más tipos de llamadas a métodos
        switch (methodType)
        {
            case 0: return $"Console.WriteLine(\"{RandomGenerator.GenerarNombreAleatorio(6)}\");";
            case 1: return $"Guid.NewGuid().ToString();"; // Añadido .ToString() para que sea una instrucción válida
            case 2: return $"Math.Abs({rnd.Next(-100, 100)}).ToString();"; // Asegura que es una instrucción válida
            case 3: return $"Console.ReadKey(true);";
            case 4: return $"DateTime.Now.ToString();";
            default: return "Console.WriteLine();";
        }
    }

    public static string GenerarCreacionObjeto()
    {
        int opcion = rnd.Next(0, 3);
        string varName = RandomGenerator.GenerarNombreAleatorio();

        switch (opcion)
        {
            case 0:
                // Para string, usar una literal de cadena vacía en lugar de new string()
                return $"string {varName} = \"\";";
            case 1:
                // Para Object, podemos usar el constructor sin parámetros
                return $"object {varName} = new object();";
            case 2:
                // Añadimos otro tipo que tenga constructor sin parámetros
                return $"DateTime {varName} = DateTime.Now;";
            default:
                return $"var {varName} = new List<int>();";
        }
    }

    public static string GenerarOperacionAritmetica()
    {
        int opType = rnd.Next(0, 4);
        string varName = RandomGenerator.GenerarNombreAleatorio();

        // Asegurar que las operaciones aritméticas sean instrucciones válidas asignándolas a variables
        switch (opType)
        {
            case 0: return $"int {varName} = {rnd.Next(1, 20)} + {rnd.Next(1, 20)};";
            case 1: return $"int {varName} = {rnd.Next(1, 20)} * {rnd.Next(1, 20)};";
            case 2: return $"int {varName} = {rnd.Next(1, 20)} - {rnd.Next(1, 20)};";
            case 3: return $"int {varName} = {rnd.Next(1, 20)} / {Math.Max(1, rnd.Next(1, 20))};"; // Evitar división por cero
            default: return $"int {varName} = 0;";
        }
    }

    private static string GenerarValorVariable(string tipo)
    {
        switch (tipo)
        {
            case "int":
                return rnd.Next(0, 10000).ToString();
            case "string":
                return "\"" + RandomGenerator.GenerarNombreAleatorio(5) + "\"";
            case "bool":
                return rnd.Next(0, 2) == 0 ? "true" : "false";
            case "double":
                return (rnd.NextDouble() * 100).ToString("0.00");
            case "long":
                return (rnd.Next(10000, 100000) * 10L).ToString();
            case "float":
                return (rnd.NextDouble() * 100).ToString("0.00f"); // Valor tipo float
            default:
                return "0";
        }
    }
}