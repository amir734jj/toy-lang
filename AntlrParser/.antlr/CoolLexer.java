// Generated from /home/amir-pc/RiderProjects/toy-lang/AntlrParser/CoolLexer.g4 by ANTLR 4.8
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class CoolLexer extends CoolLexerBase {
	static { RuntimeMetaData.checkVersion("4.8", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		MultiLineComment=1, SingleLineComment=2, OpenParenToken=3, CloseParenToken=4, 
		OpenBraceToken=5, CloseBraceToken=6, SemiColonToken=7, CommaToken=8, AssignToken=9, 
		ColonToken=10, DotToken=11, PlusToken=12, MinusToken=13, NotToken=14, 
		MultiplyToken=15, DivideToken=16, ArrowToken=17, LessThanToken=18, LessThanEqualsToken=19, 
		EqualsToken=20, NotEqualsToken=21, AndToken=22, OrToken=23, NullLiteralToken=24, 
		BooleanLiteralToken=25, DecimalLiteralToken=26, CaseToken=27, ElseToken=28, 
		NewToken=29, VarToken=30, CatchToken=31, OverrideToken=32, MatchToken=33, 
		WhileToken=34, IfToken=35, WithToken=36, DefToken=37, NativeToken=38, 
		ClassToken=39, ExtendsToken=40, NameToken=41, StringLiteralToken=42, WhiteSpacesToken=43, 
		LineTerminatorToken=44, UnexpectedCharacterToken=45;
	public static final int
		ERROR=2;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN", "ERROR"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"MultiLineComment", "SingleLineComment", "OpenParenToken", "CloseParenToken", 
			"OpenBraceToken", "CloseBraceToken", "SemiColonToken", "CommaToken", 
			"AssignToken", "ColonToken", "DotToken", "PlusToken", "MinusToken", "NotToken", 
			"MultiplyToken", "DivideToken", "ArrowToken", "LessThanToken", "LessThanEqualsToken", 
			"EqualsToken", "NotEqualsToken", "AndToken", "OrToken", "NullLiteralToken", 
			"BooleanLiteralToken", "DecimalLiteralToken", "CaseToken", "ElseToken", 
			"NewToken", "VarToken", "CatchToken", "OverrideToken", "MatchToken", 
			"WhileToken", "IfToken", "WithToken", "DefToken", "NativeToken", "ClassToken", 
			"ExtendsToken", "NameToken", "StringLiteralToken", "WhiteSpacesToken", 
			"LineTerminatorToken", "UnexpectedCharacterToken"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, null, null, "'('", "')'", "'{'", "'}'", "';'", "','", "'='", "':'", 
			"'.'", "'+'", "'-'", "'!'", "'*'", "'/'", "'=>'", "'<'", "'<='", "'=='", 
			"'!='", "'&&'", "'||'", "'null'", null, null, "'case'", "'else'", "'new'", 
			"'var'", "'catch'", "'override'", "'match'", "'while'", "'if'", "'with'", 
			"'def'", "'native'", "'class'", "'extends'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, "MultiLineComment", "SingleLineComment", "OpenParenToken", "CloseParenToken", 
			"OpenBraceToken", "CloseBraceToken", "SemiColonToken", "CommaToken", 
			"AssignToken", "ColonToken", "DotToken", "PlusToken", "MinusToken", "NotToken", 
			"MultiplyToken", "DivideToken", "ArrowToken", "LessThanToken", "LessThanEqualsToken", 
			"EqualsToken", "NotEqualsToken", "AndToken", "OrToken", "NullLiteralToken", 
			"BooleanLiteralToken", "DecimalLiteralToken", "CaseToken", "ElseToken", 
			"NewToken", "VarToken", "CatchToken", "OverrideToken", "MatchToken", 
			"WhileToken", "IfToken", "WithToken", "DefToken", "NativeToken", "ClassToken", 
			"ExtendsToken", "NameToken", "StringLiteralToken", "WhiteSpacesToken", 
			"LineTerminatorToken", "UnexpectedCharacterToken"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}


	public CoolLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "CoolLexer.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2/\u012b\b\1\4\2\t"+
		"\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\3\2\3\2\3\2\3\2\7\2b\n\2\f\2\16\2e\13\2\3\2\3\2\3\2\3"+
		"\2\3\2\3\3\3\3\3\3\3\3\7\3p\n\3\f\3\16\3s\13\3\3\3\3\3\3\4\3\4\3\5\3\5"+
		"\3\6\3\6\3\7\3\7\3\b\3\b\3\t\3\t\3\n\3\n\3\13\3\13\3\f\3\f\3\r\3\r\3\16"+
		"\3\16\3\17\3\17\3\20\3\20\3\21\3\21\3\22\3\22\3\22\3\23\3\23\3\24\3\24"+
		"\3\24\3\25\3\25\3\25\3\26\3\26\3\26\3\27\3\27\3\27\3\30\3\30\3\30\3\31"+
		"\3\31\3\31\3\31\3\31\3\32\3\32\3\32\3\32\3\32\3\32\3\32\3\32\3\32\5\32"+
		"\u00b5\n\32\3\33\3\33\3\33\7\33\u00ba\n\33\f\33\16\33\u00bd\13\33\5\33"+
		"\u00bf\n\33\3\34\3\34\3\34\3\34\3\34\3\35\3\35\3\35\3\35\3\35\3\36\3\36"+
		"\3\36\3\36\3\37\3\37\3\37\3\37\3 \3 \3 \3 \3 \3 \3!\3!\3!\3!\3!\3!\3!"+
		"\3!\3!\3\"\3\"\3\"\3\"\3\"\3\"\3#\3#\3#\3#\3#\3#\3$\3$\3$\3%\3%\3%\3%"+
		"\3%\3&\3&\3&\3&\3\'\3\'\3\'\3\'\3\'\3\'\3\'\3(\3(\3(\3(\3(\3(\3)\3)\3"+
		")\3)\3)\3)\3)\3)\3*\6*\u0110\n*\r*\16*\u0111\3+\3+\7+\u0116\n+\f+\16+"+
		"\u0119\13+\3+\3+\3,\6,\u011e\n,\r,\16,\u011f\3,\3,\3-\3-\3-\3-\3.\3.\3"+
		".\3.\3c\2/\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33"+
		"\17\35\20\37\21!\22#\23%\24\'\25)\26+\27-\30/\31\61\32\63\33\65\34\67"+
		"\359\36;\37= ?!A\"C#E$G%I&K\'M(O)Q*S+U,W-Y.[/\3\2\b\4\2\f\f\17\17\3\2"+
		"\63;\3\2\62;\b\2\f\f\"$*.\60@}}\177\177\6\2\f\f\17\17$$^^\3\2\13\13\2"+
		"\u0132\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2"+
		"\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3"+
		"\2\2\2\2\31\3\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2"+
		"\2#\3\2\2\2\2%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2\2"+
		"/\3\2\2\2\2\61\3\2\2\2\2\63\3\2\2\2\2\65\3\2\2\2\2\67\3\2\2\2\29\3\2\2"+
		"\2\2;\3\2\2\2\2=\3\2\2\2\2?\3\2\2\2\2A\3\2\2\2\2C\3\2\2\2\2E\3\2\2\2\2"+
		"G\3\2\2\2\2I\3\2\2\2\2K\3\2\2\2\2M\3\2\2\2\2O\3\2\2\2\2Q\3\2\2\2\2S\3"+
		"\2\2\2\2U\3\2\2\2\2W\3\2\2\2\2Y\3\2\2\2\2[\3\2\2\2\3]\3\2\2\2\5k\3\2\2"+
		"\2\7v\3\2\2\2\tx\3\2\2\2\13z\3\2\2\2\r|\3\2\2\2\17~\3\2\2\2\21\u0080\3"+
		"\2\2\2\23\u0082\3\2\2\2\25\u0084\3\2\2\2\27\u0086\3\2\2\2\31\u0088\3\2"+
		"\2\2\33\u008a\3\2\2\2\35\u008c\3\2\2\2\37\u008e\3\2\2\2!\u0090\3\2\2\2"+
		"#\u0092\3\2\2\2%\u0095\3\2\2\2\'\u0097\3\2\2\2)\u009a\3\2\2\2+\u009d\3"+
		"\2\2\2-\u00a0\3\2\2\2/\u00a3\3\2\2\2\61\u00a6\3\2\2\2\63\u00b4\3\2\2\2"+
		"\65\u00be\3\2\2\2\67\u00c0\3\2\2\29\u00c5\3\2\2\2;\u00ca\3\2\2\2=\u00ce"+
		"\3\2\2\2?\u00d2\3\2\2\2A\u00d8\3\2\2\2C\u00e1\3\2\2\2E\u00e7\3\2\2\2G"+
		"\u00ed\3\2\2\2I\u00f0\3\2\2\2K\u00f5\3\2\2\2M\u00f9\3\2\2\2O\u0100\3\2"+
		"\2\2Q\u0106\3\2\2\2S\u010f\3\2\2\2U\u0113\3\2\2\2W\u011d\3\2\2\2Y\u0123"+
		"\3\2\2\2[\u0127\3\2\2\2]^\7\61\2\2^_\7,\2\2_c\3\2\2\2`b\13\2\2\2a`\3\2"+
		"\2\2be\3\2\2\2cd\3\2\2\2ca\3\2\2\2df\3\2\2\2ec\3\2\2\2fg\7,\2\2gh\7\61"+
		"\2\2hi\3\2\2\2ij\b\2\2\2j\4\3\2\2\2kl\7\61\2\2lm\7\61\2\2mq\3\2\2\2np"+
		"\n\2\2\2on\3\2\2\2ps\3\2\2\2qo\3\2\2\2qr\3\2\2\2rt\3\2\2\2sq\3\2\2\2t"+
		"u\b\3\2\2u\6\3\2\2\2vw\7*\2\2w\b\3\2\2\2xy\7+\2\2y\n\3\2\2\2z{\7}\2\2"+
		"{\f\3\2\2\2|}\7\177\2\2}\16\3\2\2\2~\177\7=\2\2\177\20\3\2\2\2\u0080\u0081"+
		"\7.\2\2\u0081\22\3\2\2\2\u0082\u0083\7?\2\2\u0083\24\3\2\2\2\u0084\u0085"+
		"\7<\2\2\u0085\26\3\2\2\2\u0086\u0087\7\60\2\2\u0087\30\3\2\2\2\u0088\u0089"+
		"\7-\2\2\u0089\32\3\2\2\2\u008a\u008b\7/\2\2\u008b\34\3\2\2\2\u008c\u008d"+
		"\7#\2\2\u008d\36\3\2\2\2\u008e\u008f\7,\2\2\u008f \3\2\2\2\u0090\u0091"+
		"\7\61\2\2\u0091\"\3\2\2\2\u0092\u0093\7?\2\2\u0093\u0094\7@\2\2\u0094"+
		"$\3\2\2\2\u0095\u0096\7>\2\2\u0096&\3\2\2\2\u0097\u0098\7>\2\2\u0098\u0099"+
		"\7?\2\2\u0099(\3\2\2\2\u009a\u009b\7?\2\2\u009b\u009c\7?\2\2\u009c*\3"+
		"\2\2\2\u009d\u009e\7#\2\2\u009e\u009f\7?\2\2\u009f,\3\2\2\2\u00a0\u00a1"+
		"\7(\2\2\u00a1\u00a2\7(\2\2\u00a2.\3\2\2\2\u00a3\u00a4\7~\2\2\u00a4\u00a5"+
		"\7~\2\2\u00a5\60\3\2\2\2\u00a6\u00a7\7p\2\2\u00a7\u00a8\7w\2\2\u00a8\u00a9"+
		"\7n\2\2\u00a9\u00aa\7n\2\2\u00aa\62\3\2\2\2\u00ab\u00ac\7v\2\2\u00ac\u00ad"+
		"\7t\2\2\u00ad\u00ae\7w\2\2\u00ae\u00b5\7g\2\2\u00af\u00b0\7h\2\2\u00b0"+
		"\u00b1\7c\2\2\u00b1\u00b2\7n\2\2\u00b2\u00b3\7u\2\2\u00b3\u00b5\7g\2\2"+
		"\u00b4\u00ab\3\2\2\2\u00b4\u00af\3\2\2\2\u00b5\64\3\2\2\2\u00b6\u00bf"+
		"\7\62\2\2\u00b7\u00bb\t\3\2\2\u00b8\u00ba\t\4\2\2\u00b9\u00b8\3\2\2\2"+
		"\u00ba\u00bd\3\2\2\2\u00bb\u00b9\3\2\2\2\u00bb\u00bc\3\2\2\2\u00bc\u00bf"+
		"\3\2\2\2\u00bd\u00bb\3\2\2\2\u00be\u00b6\3\2\2\2\u00be\u00b7\3\2\2\2\u00bf"+
		"\66\3\2\2\2\u00c0\u00c1\7e\2\2\u00c1\u00c2\7c\2\2\u00c2\u00c3\7u\2\2\u00c3"+
		"\u00c4\7g\2\2\u00c48\3\2\2\2\u00c5\u00c6\7g\2\2\u00c6\u00c7\7n\2\2\u00c7"+
		"\u00c8\7u\2\2\u00c8\u00c9\7g\2\2\u00c9:\3\2\2\2\u00ca\u00cb\7p\2\2\u00cb"+
		"\u00cc\7g\2\2\u00cc\u00cd\7y\2\2\u00cd<\3\2\2\2\u00ce\u00cf\7x\2\2\u00cf"+
		"\u00d0\7c\2\2\u00d0\u00d1\7t\2\2\u00d1>\3\2\2\2\u00d2\u00d3\7e\2\2\u00d3"+
		"\u00d4\7c\2\2\u00d4\u00d5\7v\2\2\u00d5\u00d6\7e\2\2\u00d6\u00d7\7j\2\2"+
		"\u00d7@\3\2\2\2\u00d8\u00d9\7q\2\2\u00d9\u00da\7x\2\2\u00da\u00db\7g\2"+
		"\2\u00db\u00dc\7t\2\2\u00dc\u00dd\7t\2\2\u00dd\u00de\7k\2\2\u00de\u00df"+
		"\7f\2\2\u00df\u00e0\7g\2\2\u00e0B\3\2\2\2\u00e1\u00e2\7o\2\2\u00e2\u00e3"+
		"\7c\2\2\u00e3\u00e4\7v\2\2\u00e4\u00e5\7e\2\2\u00e5\u00e6\7j\2\2\u00e6"+
		"D\3\2\2\2\u00e7\u00e8\7y\2\2\u00e8\u00e9\7j\2\2\u00e9\u00ea\7k\2\2\u00ea"+
		"\u00eb\7n\2\2\u00eb\u00ec\7g\2\2\u00ecF\3\2\2\2\u00ed\u00ee\7k\2\2\u00ee"+
		"\u00ef\7h\2\2\u00efH\3\2\2\2\u00f0\u00f1\7y\2\2\u00f1\u00f2\7k\2\2\u00f2"+
		"\u00f3\7v\2\2\u00f3\u00f4\7j\2\2\u00f4J\3\2\2\2\u00f5\u00f6\7f\2\2\u00f6"+
		"\u00f7\7g\2\2\u00f7\u00f8\7h\2\2\u00f8L\3\2\2\2\u00f9\u00fa\7p\2\2\u00fa"+
		"\u00fb\7c\2\2\u00fb\u00fc\7v\2\2\u00fc\u00fd\7k\2\2\u00fd\u00fe\7x\2\2"+
		"\u00fe\u00ff\7g\2\2\u00ffN\3\2\2\2\u0100\u0101\7e\2\2\u0101\u0102\7n\2"+
		"\2\u0102\u0103\7c\2\2\u0103\u0104\7u\2\2\u0104\u0105\7u\2\2\u0105P\3\2"+
		"\2\2\u0106\u0107\7g\2\2\u0107\u0108\7z\2\2\u0108\u0109\7v\2\2\u0109\u010a"+
		"\7g\2\2\u010a\u010b\7p\2\2\u010b\u010c\7f\2\2\u010c\u010d\7u\2\2\u010d"+
		"R\3\2\2\2\u010e\u0110\n\5\2\2\u010f\u010e\3\2\2\2\u0110\u0111\3\2\2\2"+
		"\u0111\u010f\3\2\2\2\u0111\u0112\3\2\2\2\u0112T\3\2\2\2\u0113\u0117\7"+
		"$\2\2\u0114\u0116\n\6\2\2\u0115\u0114\3\2\2\2\u0116\u0119\3\2\2\2\u0117"+
		"\u0115\3\2\2\2\u0117\u0118\3\2\2\2\u0118\u011a\3\2\2\2\u0119\u0117\3\2"+
		"\2\2\u011a\u011b\7$\2\2\u011bV\3\2\2\2\u011c\u011e\t\7\2\2\u011d\u011c"+
		"\3\2\2\2\u011e\u011f\3\2\2\2\u011f\u011d\3\2\2\2\u011f\u0120\3\2\2\2\u0120"+
		"\u0121\3\2\2\2\u0121\u0122\b,\2\2\u0122X\3\2\2\2\u0123\u0124\t\2\2\2\u0124"+
		"\u0125\3\2\2\2\u0125\u0126\b-\2\2\u0126Z\3\2\2\2\u0127\u0128\13\2\2\2"+
		"\u0128\u0129\3\2\2\2\u0129\u012a\b.\3\2\u012a\\\3\2\2\2\13\2cq\u00b4\u00bb"+
		"\u00be\u0111\u0117\u011f\4\2\3\2\2\4\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}