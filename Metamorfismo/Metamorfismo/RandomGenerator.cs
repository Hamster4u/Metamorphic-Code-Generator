using System;
using System.Text;
using System.Collections.Generic; // Necessary for WrapConEstructuraRandom if using lists (e.g., in try/finally)
using System.Linq; // Necessary if JunkCode uses Linq or similar
// Make sure JunkCode.cs is in your project and accessible.

public static class RandomGenerator
{
    // We use a static instance of Random for this class
    private static Random rnd = new Random();

    // --- Basic Generation Methods ---

    // Generates a random name (variable, function, class)
    // This method is also called by JunkCode, creating a dependency.
    public static string GenerarNombreAleatorio(int length = 8)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        var sb = new StringBuilder();
        if (length <= 0) return "";

        sb.Append(chars[rnd.Next(chars.Length)]);

        const string allChars = "abcdefghijklmnopqrstuvwxyz0123456789";
        for (int i = 1; i < length; i++)
            sb.Append(allChars[rnd.Next(allChars.Length)]);

        return sb.ToString();
    }

    // Generates a random comment
    // This method WILL NOW be called inside WrapConEstructuraRandom
    public static string GenerarComentarioRandom()
    {
        string[] comments = new string[]
{
    "// TODO: review this",
    "// temporary function",
    "// possible bug here",
    "// this line is crucial",
    "// improve performance",
    "// test garbage",
    "// do not delete",
    "// critical part",
    "// fixme: ", // Added
    "// @author: random", // Added
    "// (c) random year", // Added
    "// FIXME: investigate potential issue",
    "// NOTE: needs optimization",
    "// DEBUG: check the output",
    "// REVIEW: verify correctness",
    "// IMPORTANT: verify edge cases",
    "// HACK: quick fix for now",
    "// WARNING: deprecated code",
    "// REMINDER: refactor this section",
    "// TEMP: used for debugging",
    "// BUG: does not work in edge cases",
    "// TODO: add error handling",
    "// TODO: implement logging",
    "// FIX: potential race condition",
    "// OPTIMIZE: improve algorithm efficiency",
    "// TO_BE_DONE: complete unit tests",
    "// NEXT: refactor for readability",
    "// REVIEW_LATER: to be checked later",
    "// FUTURE_ENHANCEMENT: add new feature",
    "// MAINTENANCE: remove unused code",
    "// CHECK: verify with latest release",
    "// ALGORITHM: needs better solution",
    "// REVIEW: this part needs clarification",
    "// WARNING: hardcoded values, fix later",
    "// TEMPORARY: remove after testing",
    "// BACKUP: do not modify without backup",
    "// CODESTYLE: check formatting",
    "// TODO: add null checks",
    "// TESTING: check for edge cases",
    "// OPTIMIZATION: improve memory usage",
    "// COMMENT: remember to update this section",
    "// FEATURE: add additional validation",
    "// DEPRECATED: not used in latest version",
    "// SECURITY: potential vulnerability here",
    "// SYSTEM: verify integration",
    "// LOGIC: review the algorithm",
    "// SYSTEM: update with new specifications",
    "// CONFIGURATION: check the settings",
    "// INTEGRATION: test with external systems"
};
        return comments[rnd.Next(comments.Length)];
    }

    // This method decides WHAT TYPE of junk statement to generate
    // and DELEGATES the detailed generation to the JunkCode class.
    // NOTE: Based on your provided JunkCode, JunkCode methods
    // do not take a Random object as a parameter, they use their own instance.
    // Make sure JunkCode.Generar...() methods return valid C# statements ending in ';'
    public static string GenerarCodigoBasura()
    {
        int tipoSentencia = rnd.Next(0, 5); // 5 types of statements (0 to 4)

        switch (tipoSentencia)
        {
            case 0: return JunkCode.GenerarDeclaracionVariable(); // Calls JunkCode (without passing rnd)
            case 1: return JunkCode.GenerarLlamadaMetodo();      // Calls JunkCode (without passing rnd)
            case 2: return ";"; // Simple empty statement
            case 3: return JunkCode.GenerarCreacionObjeto();     // Calls JunkCode (without passing rnd)
            case 4: return JunkCode.GenerarOperacionAritmetica(); // Calls JunkCode (without passing rnd)
            default: return "int fallback = 0;"; // Fallback
        }
    }

    // --- Transformation Methods ---

    // Wraps an instruction with a random "metamorphic" control structure
    public static string WrapConEstructuraRandom(string instruccion)
    {
        int opcion = rnd.Next(0, 10); // 10 transformation options (0 to 9)

        // Optionally generates comments to add *inside* or *around* the structure
        // We use Environment.NewLine to ensure the comment is on its own line in the generated code.
        string comentarioInterno = (rnd.Next(0, 3) == 0) ? Environment.NewLine + GenerarComentarioRandom() + Environment.NewLine : ""; // 1/3 chance of internal comment(s)
        string comentarioExterno = (rnd.Next(0, 3) == 0) ? GenerarComentarioRandom() + Environment.NewLine : ""; // 1/3 chance of external comment before

        string wrappedInstruction;

        switch (opcion)
        {
            case 0: // Simple if(true)
                wrappedInstruction = $"if (true) {{{comentarioInterno} {instruccion} }}";
                break;

            case 1: // For loop that runs once (forward) - CORRECTED!
                string nombreVarBucleFor1 = GenerarNombreAleatorio(5); // Generate the name ONCE
                wrappedInstruction = $"for (int {nombreVarBucleFor1} = 0; {nombreVarBucleFor1} < 1; {nombreVarBucleFor1}++) {{{comentarioInterno} {instruccion} }}";
                break;

            case 2: // For loop that runs once (backward) - CORRECTED!
                string nombreVarBucleFor2 = GenerarNombreAleatorio(5);
                wrappedInstruction = $"for (int {nombreVarBucleFor2} = 1; {nombreVarBucleFor2} > 0; {nombreVarBucleFor2}--) {{{comentarioInterno} {instruccion} }}";
                break;

            case 3: // while(false) with valid junk code inside - CORRECTED!
                // We call GenerarCodigoBasura() ONCE to get a valid statement
                string codigoBasuraValido = GenerarCodigoBasura();
                // Insert junk statement and real instruction.
                // The internal comment may go after the junk code inside the while, or before the instruction.
                wrappedInstruction = $"while (false) {{ {codigoBasuraValido}{comentarioInterno} }}{comentarioInterno} {instruccion}";
                break;

            case 4: // do/while(false) that runs once
                wrappedInstruction = $"do {{{comentarioInterno} {instruccion} }} while (false);"; // Semicolon required
                break;

            case 5: // Simple scope block
                wrappedInstruction = $"{{{comentarioInterno} {instruccion} }}";
                break;

            case 6: // Unreachable block with if(false) and return
                    // The internal comment may go after the 'return;'
                wrappedInstruction = $"if (false) {{ return; {comentarioInterno} }}{comentarioInterno} {instruccion}";
                break;

            case 7: // try/finally block with optional junk in finally - CORRECTED!
                string finallyJunk = "";
                if (rnd.Next(0, 2) == 1)
                {
                    // We call GenerarCodigoBasura() ONCE
                    finallyJunk = GenerarCodigoBasura();
                }
                // Internal comment may go inside the try or finally
                wrappedInstruction = $"try {{{comentarioInterno} {instruccion} }} finally {{ {finallyJunk}{comentarioInterno} }}";
                break;

            case 8: // if(true) with more complex condition (uses bool variable) - CORRECTED!
                string nombreVarBool = GenerarNombreAleatorio(6); // Generate the name ONCE
                string condicionBool = rnd.Next(0, 2) == 0 ? "(1 == 1)" : "(2 > 1)"; // Conditions that are always true
                wrappedInstruction = $"bool {nombreVarBool} = {condicionBool}; {comentarioInterno}if ({nombreVarBool}) {{{comentarioInterno} {instruccion} }}";
                break;

            case 9: // while(true) with immediate break - CORRECTED!
                wrappedInstruction = $"while (true) {{{comentarioInterno} {instruccion} break; }}";
                break;

            default: // No wrapping
                wrappedInstruction = instruccion; // No place for internal comment naturally here
                break;
        }

        // Returns the wrapped instruction, optionally preceded by an external comment
        return comentarioExterno + wrappedInstruction;
    }
}
