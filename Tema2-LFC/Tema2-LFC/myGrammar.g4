grammar myGrammar;

options {
	language = CSharp;
}


// Reguli pentru unitatile lexicale
ID          : [a-zA-Z_][a-zA-Z_0-9]* ;        // Identificatori
NUMBER      : [0-9]+ ('.' [0-9]+)? ;          // Numere intregi si reale
STRING      : '"' .*? '"' ;                   // Literal string
KEYWORD     : 'int' | 'float' | 'double' | 'string' | 'void' 
            | 'if' | 'else' | 'for' | 'while' | 'return' ;
OP_ARITH    : '+' | '-' | '*' | '/' | '%' ;
OP_REL      : '<' | '>' | '<=' | '>=' | '==' | '!=' ;
OP_LOGIC    : '&&' | '||' | '!' ;
OP_ASSIGN   : '=' | '+=' | '-=' | '*=' | '/=' | '%=' ;
OP_INC_DEC  : '++' | '--' ;
DELIM       : '(' | ')' | '{' | '}' | ',' | ';' ;

// Reguli pentru spatii si comentarii
WS          : [ \t\r\n]+ -> skip ;            // Ignora spatiile
COMMENT     : '//' ~[\r\n]* -> skip ;         // Comentarii linie
BLOCK_COMMENT : '/*' .*? '*/' -> skip ;       // Comentarii bloc

// Reguli pentru expresii
expr        : expr OP_ARITH expr
            | expr OP_REL expr
            | expr OP_LOGIC expr
            | '(' expr ')'
            | ID
            | NUMBER
            | STRING ;

// Structuri de baza
declaration : KEYWORD ID ('=' expr)? ';' ;
ifStatement : 'if' '(' expr ')' block ('else' block)? ;
whileLoop   : 'while' '(' expr ')' block ;
forLoop     : 'for' '(' declaration expr ';' expr ')' block ;
function    : KEYWORD ID '(' (declaration (',' declaration)*)? ')' block ;
block       : '{' (declaration | statement)* '}' ;
statement   : declaration | ifStatement | whileLoop | forLoop | expr ';' ;
