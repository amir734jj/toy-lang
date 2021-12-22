lexer grammar CoolLexer;

channels {
	ERROR
}

options {
	superClass = CoolLexerBase;
}

MultiLineComment: '/*' .*? '*/' -> channel(HIDDEN);
SingleLineComment: '//' ~[\r\n]* -> channel(HIDDEN);

OpenParenToken: '(';
CloseParenToken: ')';
OpenBraceToken: '{';
CloseBraceToken: '}';
SemiColonToken: ';';
CommaToken: ',';
AssignToken: '=';
ColonToken: ':';
DotToken: '.';
PlusToken: '+';
MinusToken: '-';
NotToken: '!';
MultiplyToken: '*';
DivideToken: '/';
ArrowToken: '=>';
LessThanToken: '<';
LessThanEqualsToken: '<=';
EqualsToken: '==';
NotEqualsToken: '!=';
AndToken: '&&';
OrToken: '||';

NullLiteralToken: 'null';
BooleanLiteralToken: 'true' | 'false';
DecimalLiteralToken: '0' | [1-9] [0-9]*;

CaseToken: 'case';
ElseToken: 'else';
NewToken: 'new';
VarToken: 'var';
CatchToken: 'catch';
OverrideToken: 'override';
MatchToken: 'match';
WhileToken: 'while';
IfToken: 'if';
WithToken: 'with';
DefToken: 'def';
NativeToken: 'native';
ClassToken: 'class';
ExtendsToken: 'extends';

NameToken: ~[:" {}=()\n;,!.<>*-+/]+;
StringLiteralToken: '"' ( ~[\\"\r\n])* '"';
WhiteSpacesToken: [\t]+ -> channel(HIDDEN);
LineTerminatorToken: [\r\n] -> channel(HIDDEN);
UnexpectedCharacterToken: . -> channel(ERROR);
