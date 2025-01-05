grammar myGrammar;

options {
	language = CSharp;
}

// Parser Rules
program: (globalVariable | function)* EOF;

globalVariable: type IDENTIFIER ('=' expression)? ';';

function
    : type IDENTIFIER '(' parameterList? ')' block
    ;

parameterList
    : parameter (',' parameter)*
    ;

parameter: type IDENTIFIER;

block: '{' statement* '}';

statement
    : variableDeclaration
    | assignment
    | ifStatement
    | whileStatement
    | forStatement
    | returnStatement
    | functionCall ';'
    | block
    ;

variableDeclaration
    : type IDENTIFIER ('=' expression)? ';'
    ;

assignment
    : IDENTIFIER assignmentOperator expression ';'
    ;

ifStatement
    : 'if' '(' expression ')' statement ('else' statement)?
    ;

whileStatement
    : 'while' '(' expression ')' statement
    ;

forStatement
    : 'for' '(' (variableDeclaration | assignment)? ';' expression? ';' expression? ')' statement
    ;

returnStatement
    : 'return' expression? ';'
    ;

functionCall
    : IDENTIFIER '(' argumentList? ')'
    ;

argumentList
    : expression (',' expression)*
    ;

expression
    : primary
    | functionCall
    | expression operator expression
    | '(' expression ')'
    | unaryOperator expression
    ;

primary
    : IDENTIFIER
    | INTEGER_LITERAL
    | FLOAT_LITERAL
    | STRING_LITERAL
    | BOOLEAN_LITERAL
    ;

// Lexer Rules
type: 'int' | 'float' | 'double' | 'string' | 'void';
assignmentOperator: '=' | '+=' | '-=' | '*=' | '/=' | '%=';
operator
    : '+' | '-' | '*' | '/' | '%'    // arithmetic
    | '==' | '!=' | '<' | '>' | '<=' | '>='  // relational
    | '&&' | '||'                    // logical
    ;
unaryOperator: '++' | '--' | '!' | '-';

// Tokens
IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;
INTEGER_LITERAL: [0-9]+;
FLOAT_LITERAL: [0-9]+ '.' [0-9]+;
STRING_LITERAL: '"' (~["\r\n])* '"';
BOOLEAN_LITERAL: 'true' | 'false';

// Keywords
IF: 'if';
ELSE: 'else';
WHILE: 'while';
FOR: 'for';
RETURN: 'return';

// Comments to skip
SINGLE_LINE_COMMENT: '//' ~[\r\n]* -> skip;
MULTI_LINE_COMMENT: '/*' .*? '*/' -> skip;

// Whitespace to skip
WS: [ \t\r\n]+ -> skip;