# 🧬 Metamorphic Code Generator PRO

A C#-based metamorphic code generator that dynamically creates unique source files with equivalent behavior. It manipulates control structures, injects junk code, and randomizes names and formatting to simulate natural code variation.

## 💡 What It Does

This project generates new `.cs` source files that behave the same as a base logic, but look completely different in structure. It does so by:

- Wrapping real instructions in randomly chosen control structures (e.g. `if`, `for`, `try/finally`, `while`)
- Injecting harmless "junk code" (variable declarations, math ops, object instantiations, etc.)
- Adding random developer-like comments to simulate human-written code
- Using random names for functions, classes, and variables
- Compiling and running the generated file automatically after creation

## 🔍 Use Cases

- ✨ **Code obfuscation**: Make reverse engineering more difficult by generating unpredictable code structure
- 🛡️ **Malware research**: Study metamorphic behaviors without actual payloads
- 🧪 **Static analysis testing**: Challenge static analyzers and antivirus heuristics
- 🎓 **Educational purposes**: Learn about metamorphic techniques and dynamic code generation

## 🚀 How It Works

1. Run the main program.
2. It generates a new C# source file with randomized structure and content.
3. The file is compiled on the fly using `csc.exe`.
4. If compilation is successful, the new executable is launched.

Example output:
Generando nueva mutación PRO... Nuevo archivo fuente generado: mutacion_abcdef1234567890.cs Compilación exitosa: mutacion_abcdef1234567890.exe Nueva mutación ejecutándose.


## 🛠 Requirements

- Windows OS (required for `csc.exe` from .NET Framework)
- .NET Framework 4.x installed
- Visual Studio Code or any C#-compatible IDE (optional for editing)

## 📁 Project Structure

/MetamorphicCodeGenerator │ ├── Program.cs // Entry point ├── CodeGenerator.cs // Main generator and compiler ├── JunkCode.cs // Harmless random code (junk instructions) ├── RandomGenerator.cs // Name/comment/control-structure randomizer
