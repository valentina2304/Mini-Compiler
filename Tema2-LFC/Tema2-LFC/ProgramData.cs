using System;
using System.Collections.Generic;

public class CompilerSymbols
{
    public class Variable
    {
        public enum Type
        {
            Int,
            Float,
            Double,
            String,
            Void
        }
        public Type VariableType { get; set; }
        public string Name { get; set; }
        public dynamic? InitialValue { get; set; }
        public int DeclarationLine { get; set; }
        public bool IsGlobal { get; set; }
    }

    public class Function
    {
        public enum FunctionType
        {
            Normal,
            Main,
            Recursive
        }
        public Variable.Type ReturnType { get; set; }
        public string Name { get; set; }
        public FunctionType Type { get; set; }
        public List<Variable> Parameters { get; set; } = new();
        public List<Variable> LocalVariables { get; set; } = new();
        public List<ControlStructure> ControlStructures { get; set; } = new();
        public int DeclarationLine { get; set; }
    }

    public class ControlStructure
    {
        public enum StructureType
        {
            If,
            IfElse,
            While,
            For
        }
        public StructureType Type { get; set; }
        public int StartLine { get; set; }
        public int EndLine { get; set; }
        public string Condition { get; set; }
    }

    public List<Variable> GlobalVariables { get; set; } = new();
    public List<Function> Functions { get; set; } = new();
    public List<CompilerError> Errors { get; set; } = new();
    public List<Token> Tokens { get; set; } = new();
}

public class CompilerError
{
    public enum ErrorType
    {
        Lexical,
        Syntactic,
        Semantic
    }
    public ErrorType Type { get; set; }
    public int Line { get; set; }
    public string Message { get; set; }
}

public class Token
{
    public string Text { get; set; }
    public string Type { get; set; }
    public int Line { get; set; }
}