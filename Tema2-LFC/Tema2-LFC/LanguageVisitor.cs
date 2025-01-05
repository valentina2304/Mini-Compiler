using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CompilerVisitor : myGrammarBaseVisitor<CompilerSymbols>
{
    private CompilerSymbols _symbols = new();
    private CompilerSymbols.Function? _currentFunction;
    private Stack<CompilerSymbols.ControlStructure> _controlStructureStack = new();

    public override CompilerSymbols VisitProgram([NotNull] myGrammarParser.ProgramContext context)
    {
        foreach (var child in context.children)
        {
            Visit(child);
        }
        ValidateProgram();
        return _symbols;
    }

    public override CompilerSymbols VisitGlobalVariable([NotNull] myGrammarParser.GlobalVariableContext context)
    {
        var variable = CreateVariable(context.type(), context.IDENTIFIER().GetText(), true, context.Start.Line);

        // Check for duplicate global variable
        if (_symbols.GlobalVariables.Any(v => v.Name == variable.Name))
        {
            AddError(CompilerError.ErrorType.Semantic, context.Start.Line,
                    $"Duplicate global variable declaration: {variable.Name}");
            return _symbols;
        }

        // If there's an initial value, validate and set it
        if (context.expression() != null)
        {
            ValidateInitialValue(variable, context.expression());
        }

        _symbols.GlobalVariables.Add(variable);
        return _symbols;
    }

    public override CompilerSymbols VisitFunction([NotNull] myGrammarParser.FunctionContext context)
    {
        var function = new CompilerSymbols.Function
        {
            Name = context.IDENTIFIER().GetText(),
            DeclarationLine = context.Start.Line,
            ReturnType = ParseType(context.type().GetText())
        };

        // Check for duplicate function
        if (_symbols.Functions.Any(f => f.Name == function.Name))
        {
            AddError(CompilerError.ErrorType.Semantic, context.Start.Line,
                    $"Duplicate function declaration: {function.Name}");
            return _symbols;
        }

        _currentFunction = function;

        // Visit parameters
        if (context.parameterList() != null)
        {
            Visit(context.parameterList());
        }

        // Visit function body
        Visit(context.block());

        // Determine function type
        DetermineFunctionType(function, context);

        _symbols.Functions.Add(function);
        _currentFunction = null;

        return _symbols;
    }

    public override CompilerSymbols VisitParameterList([NotNull] myGrammarParser.ParameterListContext context)
    {
        foreach (var param in context.parameter())
        {
            var parameter = CreateVariable(param.type(), param.IDENTIFIER().GetText(), false, param.Start.Line);

            // Check for duplicate parameters
            if (_currentFunction.Parameters.Any(p => p.Name == parameter.Name))
            {
                AddError(CompilerError.ErrorType.Semantic, param.Start.Line,
                        $"Duplicate parameter name in function {_currentFunction.Name}: {parameter.Name}");
                continue;
            }

            _currentFunction.Parameters.Add(parameter);
        }
        return _symbols;
    }

    public override CompilerSymbols VisitVariableDeclaration([NotNull] myGrammarParser.VariableDeclarationContext context)
    {
        if (_currentFunction == null)
        {
            AddError(CompilerError.ErrorType.Semantic, context.Start.Line,
                    "Variable declaration outside of function context");
            return _symbols;
        }

        var variable = CreateVariable(context.type(), context.IDENTIFIER().GetText(), false, context.Start.Line);

        // Check for duplicate local variable
        if (_currentFunction.LocalVariables.Any(v => v.Name == variable.Name))
        {
            AddError(CompilerError.ErrorType.Semantic, context.Start.Line,
                    $"Duplicate local variable declaration: {variable.Name}");
            return _symbols;
        }

        // If there's an initial value, validate and set it
        if (context.expression() != null)
        {
            ValidateInitialValue(variable, context.expression());
        }

        _currentFunction.LocalVariables.Add(variable);
        return _symbols;
    }

    public override CompilerSymbols VisitIfStatement([NotNull] myGrammarParser.IfStatementContext context)
    {
        var structure = new CompilerSymbols.ControlStructure
        {
            Type = context.ELSE() != null ?
                   CompilerSymbols.ControlStructure.StructureType.IfElse :
                   CompilerSymbols.ControlStructure.StructureType.If,
            StartLine = context.Start.Line,
            EndLine = context.Stop.Line,
            Condition = context.expression().GetText()
        };

        _controlStructureStack.Push(structure);
        _currentFunction?.ControlStructures.Add(structure);

        Visit(context.statement(0));
        if (context.ELSE() != null)
        {
            Visit(context.statement(1));
        }

        _controlStructureStack.Pop();
        return _symbols;
    }

    public override CompilerSymbols VisitWhileStatement([NotNull] myGrammarParser.WhileStatementContext context)
    {
        var structure = new CompilerSymbols.ControlStructure
        {
            Type = CompilerSymbols.ControlStructure.StructureType.While,
            StartLine = context.Start.Line,
            EndLine = context.Stop.Line,
            Condition = context.expression().GetText()
        };

        _controlStructureStack.Push(structure);
        _currentFunction?.ControlStructures.Add(structure);

        Visit(context.statement());

        _controlStructureStack.Pop();
        return _symbols;
    }

    public override CompilerSymbols VisitForStatement([NotNull] myGrammarParser.ForStatementContext context)
    {
        var structure = new CompilerSymbols.ControlStructure
        {
            Type = CompilerSymbols.ControlStructure.StructureType.For,
            StartLine = context.Start.Line,
            EndLine = context.Stop.Line,
            Condition = context.expression().First()?.GetText() ?? ""
        };

        _controlStructureStack.Push(structure);
        _currentFunction?.ControlStructures.Add(structure);

        Visit(context.statement());

        _controlStructureStack.Pop();
        return _symbols;
    }

    public override CompilerSymbols VisitFunctionCall([NotNull] myGrammarParser.FunctionCallContext context)
    {
        var functionName = context.IDENTIFIER().GetText();

        // Check if function exists
        if (!_symbols.Functions.Any(f => f.Name == functionName))
        {
            AddError(CompilerError.ErrorType.Semantic, context.Start.Line,
                    $"Call to undefined function: {functionName}");
        }

        // Validate argument count if function exists
        var calledFunction = _symbols.Functions.FirstOrDefault(f => f.Name == functionName);
        if (calledFunction != null)
        {
            var expectedArgs = calledFunction.Parameters.Count;
            var providedArgs = context.argumentList()?.expression().Length ?? 0;

            if (expectedArgs != providedArgs)
            {
                AddError(CompilerError.ErrorType.Semantic, context.Start.Line,
                        $"Function {functionName} expects {expectedArgs} arguments but got {providedArgs}");
            }
        }

        return _symbols;
    }

    private CompilerSymbols.Variable CreateVariable(myGrammarParser.TypeContext typeContext,
                                                  string name, bool isGlobal, int line)
    {
        return new CompilerSymbols.Variable
        {
            Name = name,
            VariableType = ParseType(typeContext.GetText()),
            IsGlobal = isGlobal,
            DeclarationLine = line
        };
    }

    private CompilerSymbols.Variable.Type ParseType(string typeText)
    {
        return typeText switch
        {
            "int" => CompilerSymbols.Variable.Type.Int,
            "float" => CompilerSymbols.Variable.Type.Float,
            "double" => CompilerSymbols.Variable.Type.Double,
            "string" => CompilerSymbols.Variable.Type.String,
            "void" => CompilerSymbols.Variable.Type.Void,
            _ => throw new Exception($"Unknown type {typeText}")
        };
    }

    private void ValidateInitialValue(CompilerSymbols.Variable variable,
                                    myGrammarParser.ExpressionContext expression)
    {
        var value = expression.GetText();

        try
        {
            switch (variable.VariableType)
            {
                case CompilerSymbols.Variable.Type.Int:
                    variable.InitialValue = int.Parse(value);
                    break;
                case CompilerSymbols.Variable.Type.Float:
                case CompilerSymbols.Variable.Type.Double:
                    variable.InitialValue = double.Parse(value);
                    break;
                case CompilerSymbols.Variable.Type.String:
                    if (!value.StartsWith("\"") || !value.EndsWith("\""))
                    {
                        throw new Exception("String value must be enclosed in quotes");
                    }
                    variable.InitialValue = value.Trim('"');
                    break;
                default:
                    throw new Exception($"Cannot initialize type {variable.VariableType}");
            }
        }
        catch (Exception ex)
        {
            AddError(CompilerError.ErrorType.Semantic, expression.Start.Line,
                    $"Invalid value for type {variable.VariableType}: {value} - {ex.Message}");
        }
    }

    private void DetermineFunctionType(CompilerSymbols.Function function,
                                     myGrammarParser.FunctionContext context)
    {
        if (function.Name.ToLower() == "main")
        {
            function.Type = CompilerSymbols.Function.FunctionType.Main;
        }
        else
        {
            // Check for recursion by looking for self-calls in the function body
            var functionCalls = context.block()
                .GetText()
                .Count(c => c == function.Name[0]); // Simplified recursion check

            function.Type = functionCalls > 0 ?
                CompilerSymbols.Function.FunctionType.Recursive :
                CompilerSymbols.Function.FunctionType.Normal;
        }
    }

    private void ValidateProgram()
    {
        // Check for main function
        if (!_symbols.Functions.Any(f => f.Name.ToLower() == "main"))
        {
            AddError(CompilerError.ErrorType.Semantic, 0, "No main function found in program");
        }

        // Check for undefined variable references
        // This would require additional context tracking during visits

        // Additional program-wide validations can be added here
    }

    private void AddError(CompilerError.ErrorType type, int line, string message)
    {
        _symbols.Errors.Add(new CompilerError
        {
            Type = type,
            Line = line,
            Message = message
        });
    }
}
