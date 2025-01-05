using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class Program
{
    public static void Main(string[] args)
{
    try
    {
        // Citește codul sursă
        string sourceCode = File.ReadAllText("input.in");
        
        // Creează lexerul și parserul
        var inputStream = new AntlrInputStream(sourceCode);
        var lexer = new myGrammarLexer(inputStream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new myGrammarParser(tokens);
        
        // Analizează programul
        var programContext = parser.program();
        var visitor = new CompilerVisitor();
        var result = visitor.Visit(programContext);
        
        // Salvează rezultatele
        //SaveResults(result);
        
        // Afișează erorile dacă există
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

}