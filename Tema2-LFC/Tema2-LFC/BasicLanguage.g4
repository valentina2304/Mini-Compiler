grammar BasicLanguage;

program: (globalVariableDecl | functionDecl)* EOF;

globalVariableDecl: type IDENTIFIER (EQUAL expr)? SEMICOLON;

functionDecl:
    type (MAIN_FUN | IDENTIFIER) '(' paramList? ')' block;

paramList: param (',' param)*;
param: type IDENTIFIER;

block: '{' statement* '}';

statement:
    variableDecl
    | ifStatement
    | whileStatement
    | forStatement
    | returnStatement
    | exprStatement
    | block;

variableDecl: type IDENTIFIER (EQUAL expr)? SEMICOLON;

ifStatement: IF '(' expr ')' block (ELSE block)?;
whileStatement: WHILE '(' expr ')' block;
forStatement:
    FOR '(' (variableDecl | exprStatement | SEMICOLON) expr? SEMICOLON expr? ')' block;
returnStatement: RETURN expr? SEMICOLON;
exprStatement: expr SEMICOLON;

expr:
    assignmentExpr
    | logicalExpr;

assignmentExpr: 
    IDENTIFIER assignmentOp expr;

logicalExpr: 
    relationalExpr (('&&' | '||') relationalExpr)*;

relationalExpr:
    additiveExpr (('==' | '!=' | '<' | '>' | '<=' | '>=') additiveExpr)*;

additiveExpr:
    multiplicativeExpr (('+' | '-') multiplicativeExpr)*;

multiplicativeExpr: 
    unaryExpr (('*' | '/' | '%') unaryExpr)*;

unaryExpr: 
    ('!' | '-' | '++' | '--') unaryExpr 
    | postfixExpr;

postfixExpr:
    primaryExpr
    | primaryExpr '++'
    | primaryExpr '--';

primaryExpr:
    IDENTIFIER
    | INTEGER_VALUE
    | FLOAT_VALUE
    | STRING_VALUE
    | '(' expr ')'
    | IDENTIFIER '(' exprList? ')';

exprList: expr (',' expr)*;

type: INT | FLOAT | DOUBLE | STRING | VOID;

// Lexer Rules
INTEGER_VALUE: [0-9]+;
FLOAT_VALUE: [0-9]+ '.' [0-9]+;
STRING_VALUE: '"' .*? '"';

// Keywords
IF: 'if';
ELSE: 'else';
WHILE: 'while';
FOR: 'for';
RETURN: 'return';
VOID: 'void';
INT: 'int';
FLOAT: 'float';
DOUBLE: 'double';
STRING: 'string';
MAIN_FUN: 'main';

// Operators and punctuation
SEMICOLON: ';';
LPAREN: '(';
RPAREN: ')';
LBRACE: '{';
RBRACE: '}';
COMMA: ',';
EQUAL: '=';

assignmentOp:
    '=' 
    | '+=' 
    | '-=' 
    | '*=' 
    | '/=' 
    | '%=';

// Identifiers
IDENTIFIER: [a-zA-Z][a-zA-Z0-9_]*;

// Comments
BLOCK_COMMENT: '/*' .*? '*/' -> skip;
LINE_COMMENT: '//' ~[\r\n]* '\r'? '\n' -> skip;

// Whitespace
WS: [ \t\r\n]+ -> skip;