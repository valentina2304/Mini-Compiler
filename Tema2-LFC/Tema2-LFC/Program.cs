using Antlr4.Runtime;
using System;
using System.IO;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            string sourceCode = File.ReadAllText("input.in");

            var inputStream = new AntlrInputStream(sourceCode);
            var lexer = new BasicLanguageLexer(inputStream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new BasicLanguageParser(tokens);

            var programContext = parser.program();
            var visitor = new CompilerVisitor();
            var result = visitor.Visit(programContext);

            SaveTokens(tokens, "tokens.txt");

            SaveCompilerData(result);

            if (result.Errors.Any())
            {
                Console.WriteLine("Erori găsite:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Linia {error.Line}: {error.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
    }

    private static void SaveTokens(CommonTokenStream tokens, string outputFile)
    {
        using var writer = new StreamWriter(outputFile);
        foreach (var token in tokens.GetTokens())
        {
            if (token.Channel != Lexer.Hidden)
            {
                writer.WriteLine($"<{token.Text}, {token.Type}, line {token.Line}>");
            }
        }
    }

    private static void SaveCompilerData(CompilerSymbols symbols)
    {
        using (var writer = new StreamWriter("global_variables.txt"))
        {
            foreach (var variable in symbols.GlobalVariables)
            {
                writer.WriteLine($"Type: {variable.VariableType}, Name: {variable.Name}, " +
                               $"Initial Value: {variable.InitialValue ?? "none"}");
            }
        }

        using (var writer = new StreamWriter("functions.txt"))
        {
            foreach (var function in symbols.Functions)
            {
                writer.WriteLine($"Function: {function.Name}");
                writer.WriteLine($"Type: {function.Type}");
                writer.WriteLine($"Return Type: {function.ReturnType}");
                writer.WriteLine("Parameters:");
                foreach (var param in function.Parameters)
                {
                    writer.WriteLine($"  {param.VariableType} {param.Name}");
                }
                writer.WriteLine("Local Variables:");
                foreach (var var in function.LocalVariables)
                {
                    writer.WriteLine($"  {var.VariableType} {var.Name} = {var.InitialValue ?? "none"}");
                }
                writer.WriteLine("Control Structures:");
                foreach (var structure in function.ControlStructures)
                {
                    writer.WriteLine($"  {structure.Type} at lines {structure.StartLine}-{structure.EndLine}");
                    writer.WriteLine($"  Condition: {structure.Condition}");
                }
                writer.WriteLine();
            }
        }
    }
}