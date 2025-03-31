Mini Compiler in C# using ANTLR

Overview

This project is a mini compiler built in C#, utilizing ANTLR for lexical and syntactical analysis. The compiler processes a custom language, generating an Abstract Syntax Tree (AST) and performing basic semantic analysis.

Features

Lexical Analysis: Tokenizes the input source code.

Parsing: Builds an AST based on grammar rules.

Semantic Analysis: Validates variable usage and basic expressions.

Code Generation: Outputs an intermediate representation.

Technologies Used

C#: Main programming language for implementation.

ANTLR: Used for defining grammar and generating parser/lexer.

.NET Core: Provides runtime support.

Visual Studio: Recommended IDE for development.

Installation & Setup

Clone the repository:

git clone https://github.com/your-repo/mini-compiler.git

Install ANTLR via NuGet:

dotnet add package Antlr4.Runtime.Standard

Compile and run:

dotnet build
dotnet run

Usage

Define your source code in a file (e.g., program.txt).

Run the compiler and provide the file as input.

View the generated AST or intermediate representation.

Example Code

Sample input for the mini compiler:

int x = 5;
print(x + 10);

The compiler will parse and validate this input, then generate the necessary output.

Future Enhancements

Add optimization passes.

Implement bytecode generation.

Extend language features.
