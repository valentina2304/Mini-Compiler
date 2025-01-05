//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from myGrammar.g4 by ANTLR 4.13.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="myGrammarParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.2")]
[System.CLSCompliant(false)]
public interface ImyGrammarVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] myGrammarParser.ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.globalVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGlobalVariable([NotNull] myGrammarParser.GlobalVariableContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunction([NotNull] myGrammarParser.FunctionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.parameterList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParameterList([NotNull] myGrammarParser.ParameterListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.parameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParameter([NotNull] myGrammarParser.ParameterContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] myGrammarParser.BlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] myGrammarParser.StatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.variableDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableDeclaration([NotNull] myGrammarParser.VariableDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignment([NotNull] myGrammarParser.AssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStatement([NotNull] myGrammarParser.IfStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.whileStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileStatement([NotNull] myGrammarParser.WhileStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.forStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForStatement([NotNull] myGrammarParser.ForStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.returnStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnStatement([NotNull] myGrammarParser.ReturnStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall([NotNull] myGrammarParser.FunctionCallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.argumentList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgumentList([NotNull] myGrammarParser.ArgumentListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] myGrammarParser.ExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.primary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrimary([NotNull] myGrammarParser.PrimaryContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitType([NotNull] myGrammarParser.TypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.assignmentOperator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignmentOperator([NotNull] myGrammarParser.AssignmentOperatorContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.operator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperator([NotNull] myGrammarParser.OperatorContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="myGrammarParser.unaryOperator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnaryOperator([NotNull] myGrammarParser.UnaryOperatorContext context);
}
