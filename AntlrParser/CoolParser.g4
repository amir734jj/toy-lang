parser grammar CoolParser;

options {
	tokenVocab = CoolLexer;
	superClass = CoolParserBase;
}

classes: class_nonterminal* EOF;

class_nonterminal:
	ClassToken NameToken formals ExtendsToken NameToken actuals features
	| ClassToken NameToken formals ExtendsToken native_nonterminal features
	| ClassToken NameToken formals features;

feature: expr | function_decl;

many_features:
	many_features SemiColonToken feature SemiColonToken?
	| feature
	| /* epsilon */;

features: OpenBraceToken many_features CloseBraceToken;

function_decl:
	OverrideToken? DefToken NameToken formals ColonToken NameToken AssignToken expr;

formal: NameToken ColonToken NameToken;

many_formal:
	many_formal CommaToken formal CommaToken?
	| formal
	| /* epsilon */;

formals: OpenParenToken many_formal CloseParenToken;

actual: expr;

many_actual:
	many_actual CommaToken actual CommaToken?
	| actual
	| /* epsilon */;

actuals: OpenParenToken many_actual CloseParenToken;

native_nonterminal: NativeToken;

expr_many:
	expr_many SemiColonToken expr SemiColonToken?
	| expr
	| /* epsilon */;

expr:
	VarToken NameToken ColonToken NameToken AssignToken expr
	| NameToken AssignToken expr
	| IfToken OpenParenToken expr CloseParenToken expr ElseToken expr
	| WhileToken OpenParenToken expr CloseParenToken expr
	| OpenBraceToken expr_many CloseBraceToken
	| expr PlusToken expr
	| expr MinusToken expr
	| expr MultiplyToken expr
	| expr DivideToken expr
	| expr LessThanToken expr
	| expr LessThanEqualsToken expr
	| expr EqualsToken expr
	| expr NotEqualsToken expr
	| expr AndToken expr
	| expr OrToken expr
	| expr DotToken expr
	| NameToken actuals
	| MatchToken expr WithToken arms
	| atomic
	| native_nonterminal
	| NameToken
	| NewToken NameToken actuals
	| MinusToken expr
	| NotToken expr
	| OpenBraceToken expr CloseBraceToken;

atomic:
	NullLiteralToken
	| StringLiteralToken
	| DecimalLiteralToken
	| BooleanLiteralToken;

null_arm: CaseToken NullLiteralToken ArrowToken expr;
typed_arm:
	CaseToken NameToken ColonToken NameToken ArrowToken expr;
arm: null_arm | typed_arm;

many_arm: many_arm CommaToken arm | arm;

arms: OpenBraceToken many_arm CloseBraceToken;
