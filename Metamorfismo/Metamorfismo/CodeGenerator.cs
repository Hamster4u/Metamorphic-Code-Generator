using System;
using System.IO;
using System.Text;
using System.Diagnostics;

public static class CodeGenerator
{
    public static void GenerarCodigo()
    {
        Random rnd = new Random();
        string[] accionesPrincipales = new string[]
        {
            "Console.WriteLine(\"Accion 1 ejecutada.\");",
            "Console.WriteLine(\"Accion 2 ejecutada.\");",
            "Console.WriteLine(\"Accion 3 ejecutada.\");"
        };

        StringBuilder nuevoCodigo = new StringBuilder();
        string nombreClase = RandomGenerator.GenerarNombreAleatorio();
        string nombreFuncion = RandomGenerator.GenerarNombreAleatorio();

        nuevoCodigo.AppendLine("using System;");
        nuevoCodigo.AppendLine();
        nuevoCodigo.AppendLine($"class {nombreClase}");
        nuevoCodigo.AppendLine("{");
        nuevoCodigo.AppendLine($"    static void {nombreFuncion}()");

        nuevoCodigo.AppendLine("    {");

        foreach (var accion in accionesPrincipales)
        {
            string instruccion = RandomGenerator.WrapConEstructuraRandom(accion);
            nuevoCodigo.AppendLine($"        {instruccion}");
        }

        nuevoCodigo.AppendLine("        Console.WriteLine(\"\\nPresiona cualquier tecla para salir...\");");
        nuevoCodigo.AppendLine("        Console.ReadKey();");
        nuevoCodigo.AppendLine("    }");

        nuevoCodigo.AppendLine("    static void Main()");
        nuevoCodigo.AppendLine("    {");
        nuevoCodigo.AppendLine($"        {nombreFuncion}();");
        nuevoCodigo.AppendLine("    }");

        nuevoCodigo.AppendLine("}");

        string nuevoArchivo = $"mutacion_{Guid.NewGuid().ToString("N")}.cs";
        File.WriteAllText(nuevoArchivo, nuevoCodigo.ToString());
        Console.WriteLine($"Nuevo archivo fuente generado: {nuevoArchivo}");

        CompilarCodigo(nuevoArchivo);
    }

    private static void CompilarCodigo(string nuevoArchivo)
    {
        string cscPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), @"Microsoft.NET\Framework\v4.0.30319\csc.exe");

        if (!File.Exists(cscPath))
        {
            Console.WriteLine("ERROR: No se encontró csc.exe");
            return;
        }

        string exeFinal = Path.ChangeExtension(nuevoArchivo, ".exe");
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = cscPath,
            Arguments = $"/target:exe /out:{exeFinal} {nuevoArchivo}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process p = Process.Start(psi))
        {
            string output = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();
            p.WaitForExit();

            if (p.ExitCode == 0)
            {
                Console.WriteLine($"Compilación exitosa: {exeFinal}");
                Process.Start(new ProcessStartInfo
                {
                    FileName = exeFinal,
                    UseShellExecute = true
                });
                Console.WriteLine("Nueva mutación ejecutándose...");
            }
            else
            {
                Console.WriteLine("ERROR en compilación:");
                Console.WriteLine(output);
                Console.WriteLine(error);
            }
        }
    }
}
