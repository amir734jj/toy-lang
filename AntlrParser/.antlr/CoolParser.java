// Generated from /home/amir-pc/RiderProjects/toy-lang/AntlrParser/CoolParser.g4 by ANTLR 4.8
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class CoolParser extends CoolParserBase {
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
		RULE_classes = 0, RULE_class_nonterminal = 1, RULE_feature = 2, RULE_many_features = 3, 
		RULE_features = 4, RULE_function_decl = 5, RULE_formal = 6, RULE_many_formal = 7, 
		RULE_formals = 8, RULE_actual = 9, RULE_many_actual = 10, RULE_actuals = 11, 
		RULE_native_nonterminal = 12, RULE_expr_many = 13, RULE_expr = 14, RULE_atomic = 15, 
		RULE_null_arm = 16, RULE_typed_arm = 17, RULE_arm = 18, RULE_many_arm = 19, 
		RULE_arms = 20;
	private static String[] makeRuleNames() {
		return new String[] {
			"classes", "class_nonterminal", "feature", "many_features", "features", 
			"function_decl", "formal", "many_formal", "formals", "actual", "many_actual", 
			"actuals", "native_nonterminal", "expr_many", "expr", "atomic", "null_arm", 
			"typed_arm", "arm", "many_arm", "arms"
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

	@Override
	public String getGrammarFileName() { return "CoolParser.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public CoolParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class ClassesContext extends ParserRuleContext {
		public TerminalNode EOF() { return getToken(CoolParser.EOF, 0); }
		public List<Class_nonterminalContext> class_nonterminal() {
			return getRuleContexts(Class_nonterminalContext.class);
		}
		public Class_nonterminalContext class_nonterminal(int i) {
			return getRuleContext(Class_nonterminalContext.class,i);
		}
		public ClassesContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_classes; }
	}

	public final ClassesContext classes() throws RecognitionException {
		ClassesContext _localctx = new ClassesContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_classes);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(45);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==ClassToken) {
				{
				{
				setState(42);
				class_nonterminal();
				}
				}
				setState(47);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(48);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Class_nonterminalContext extends ParserRuleContext {
		public TerminalNode ClassToken() { return getToken(CoolParser.ClassToken, 0); }
		public List<TerminalNode> NameToken() { return getTokens(CoolParser.NameToken); }
		public TerminalNode NameToken(int i) {
			return getToken(CoolParser.NameToken, i);
		}
		public FormalsContext formals() {
			return getRuleContext(FormalsContext.class,0);
		}
		public TerminalNode ExtendsToken() { return getToken(CoolParser.ExtendsToken, 0); }
		public ActualsContext actuals() {
			return getRuleContext(ActualsContext.class,0);
		}
		public FeaturesContext features() {
			return getRuleContext(FeaturesContext.class,0);
		}
		public Native_nonterminalContext native_nonterminal() {
			return getRuleContext(Native_nonterminalContext.class,0);
		}
		public Class_nonterminalContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_class_nonterminal; }
	}

	public final Class_nonterminalContext class_nonterminal() throws RecognitionException {
		Class_nonterminalContext _localctx = new Class_nonterminalContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_class_nonterminal);
		try {
			setState(70);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,1,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(50);
				match(ClassToken);
				setState(51);
				match(NameToken);
				setState(52);
				formals();
				setState(53);
				match(ExtendsToken);
				setState(54);
				match(NameToken);
				setState(55);
				actuals();
				setState(56);
				features();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(58);
				match(ClassToken);
				setState(59);
				match(NameToken);
				setState(60);
				formals();
				setState(61);
				match(ExtendsToken);
				setState(62);
				native_nonterminal();
				setState(63);
				features();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(65);
				match(ClassToken);
				setState(66);
				match(NameToken);
				setState(67);
				formals();
				setState(68);
				features();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FeatureContext extends ParserRuleContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public Function_declContext function_decl() {
			return getRuleContext(Function_declContext.class,0);
		}
		public FeatureContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_feature; }
	}

	public final FeatureContext feature() throws RecognitionException {
		FeatureContext _localctx = new FeatureContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_feature);
		try {
			setState(74);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,2,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(72);
				expr(0);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(73);
				function_decl();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Many_featuresContext extends ParserRuleContext {
		public FeatureContext feature() {
			return getRuleContext(FeatureContext.class,0);
		}
		public Many_featuresContext many_features() {
			return getRuleContext(Many_featuresContext.class,0);
		}
		public List<TerminalNode> SemiColonToken() { return getTokens(CoolParser.SemiColonToken); }
		public TerminalNode SemiColonToken(int i) {
			return getToken(CoolParser.SemiColonToken, i);
		}
		public Many_featuresContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_many_features; }
	}

	public final Many_featuresContext many_features() throws RecognitionException {
		return many_features(0);
	}

	private Many_featuresContext many_features(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		Many_featuresContext _localctx = new Many_featuresContext(_ctx, _parentState);
		Many_featuresContext _prevctx = _localctx;
		int _startState = 6;
		enterRecursionRule(_localctx, 6, RULE_many_features, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(79);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,3,_ctx) ) {
			case 1:
				{
				setState(77);
				feature();
				}
				break;
			case 2:
				{
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(89);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,5,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					{
					_localctx = new Many_featuresContext(_parentctx, _parentState);
					pushNewRecursionContext(_localctx, _startState, RULE_many_features);
					setState(81);
					if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
					setState(82);
					match(SemiColonToken);
					setState(83);
					feature();
					setState(85);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,4,_ctx) ) {
					case 1:
						{
						setState(84);
						match(SemiColonToken);
						}
						break;
					}
					}
					} 
				}
				setState(91);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,5,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class FeaturesContext extends ParserRuleContext {
		public TerminalNode OpenBraceToken() { return getToken(CoolParser.OpenBraceToken, 0); }
		public Many_featuresContext many_features() {
			return getRuleContext(Many_featuresContext.class,0);
		}
		public TerminalNode CloseBraceToken() { return getToken(CoolParser.CloseBraceToken, 0); }
		public FeaturesContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_features; }
	}

	public final FeaturesContext features() throws RecognitionException {
		FeaturesContext _localctx = new FeaturesContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_features);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(92);
			match(OpenBraceToken);
			setState(93);
			many_features(0);
			setState(94);
			match(CloseBraceToken);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Function_declContext extends ParserRuleContext {
		public TerminalNode DefToken() { return getToken(CoolParser.DefToken, 0); }
		public List<TerminalNode> NameToken() { return getTokens(CoolParser.NameToken); }
		public TerminalNode NameToken(int i) {
			return getToken(CoolParser.NameToken, i);
		}
		public FormalsContext formals() {
			return getRuleContext(FormalsContext.class,0);
		}
		public TerminalNode ColonToken() { return getToken(CoolParser.ColonToken, 0); }
		public TerminalNode AssignToken() { return getToken(CoolParser.AssignToken, 0); }
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public TerminalNode OverrideToken() { return getToken(CoolParser.OverrideToken, 0); }
		public Function_declContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_function_decl; }
	}

	public final Function_declContext function_decl() throws RecognitionException {
		Function_declContext _localctx = new Function_declContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_function_decl);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(97);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==OverrideToken) {
				{
				setState(96);
				match(OverrideToken);
				}
			}

			setState(99);
			match(DefToken);
			setState(100);
			match(NameToken);
			setState(101);
			formals();
			setState(102);
			match(ColonToken);
			setState(103);
			match(NameToken);
			setState(104);
			match(AssignToken);
			setState(105);
			expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FormalContext extends ParserRuleContext {
		public List<TerminalNode> NameToken() { return getTokens(CoolParser.NameToken); }
		public TerminalNode NameToken(int i) {
			return getToken(CoolParser.NameToken, i);
		}
		public TerminalNode ColonToken() { return getToken(CoolParser.ColonToken, 0); }
		public FormalContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_formal; }
	}

	public final FormalContext formal() throws RecognitionException {
		FormalContext _localctx = new FormalContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_formal);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(107);
			match(NameToken);
			setState(108);
			match(ColonToken);
			setState(109);
			match(NameToken);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Many_formalContext extends ParserRuleContext {
		public FormalContext formal() {
			return getRuleContext(FormalContext.class,0);
		}
		public Many_formalContext many_formal() {
			return getRuleContext(Many_formalContext.class,0);
		}
		public List<TerminalNode> CommaToken() { return getTokens(CoolParser.CommaToken); }
		public TerminalNode CommaToken(int i) {
			return getToken(CoolParser.CommaToken, i);
		}
		public Many_formalContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_many_formal; }
	}

	public final Many_formalContext many_formal() throws RecognitionException {
		return many_formal(0);
	}

	private Many_formalContext many_formal(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		Many_formalContext _localctx = new Many_formalContext(_ctx, _parentState);
		Many_formalContext _prevctx = _localctx;
		int _startState = 14;
		enterRecursionRule(_localctx, 14, RULE_many_formal, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(114);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,7,_ctx) ) {
			case 1:
				{
				setState(112);
				formal();
				}
				break;
			case 2:
				{
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(124);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,9,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					{
					_localctx = new Many_formalContext(_parentctx, _parentState);
					pushNewRecursionContext(_localctx, _startState, RULE_many_formal);
					setState(116);
					if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
					setState(117);
					match(CommaToken);
					setState(118);
					formal();
					setState(120);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,8,_ctx) ) {
					case 1:
						{
						setState(119);
						match(CommaToken);
						}
						break;
					}
					}
					} 
				}
				setState(126);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,9,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class FormalsContext extends ParserRuleContext {
		public TerminalNode OpenParenToken() { return getToken(CoolParser.OpenParenToken, 0); }
		public Many_formalContext many_formal() {
			return getRuleContext(Many_formalContext.class,0);
		}
		public TerminalNode CloseParenToken() { return getToken(CoolParser.CloseParenToken, 0); }
		public FormalsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_formals; }
	}

	public final FormalsContext formals() throws RecognitionException {
		FormalsContext _localctx = new FormalsContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_formals);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(127);
			match(OpenParenToken);
			setState(128);
			many_formal(0);
			setState(129);
			match(CloseParenToken);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ActualContext extends ParserRuleContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public ActualContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_actual; }
	}

	public final ActualContext actual() throws RecognitionException {
		ActualContext _localctx = new ActualContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_actual);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(131);
			expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Many_actualContext extends ParserRuleContext {
		public ActualContext actual() {
			return getRuleContext(ActualContext.class,0);
		}
		public Many_actualContext many_actual() {
			return getRuleContext(Many_actualContext.class,0);
		}
		public List<TerminalNode> CommaToken() { return getTokens(CoolParser.CommaToken); }
		public TerminalNode CommaToken(int i) {
			return getToken(CoolParser.CommaToken, i);
		}
		public Many_actualContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_many_actual; }
	}

	public final Many_actualContext many_actual() throws RecognitionException {
		return many_actual(0);
	}

	private Many_actualContext many_actual(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		Many_actualContext _localctx = new Many_actualContext(_ctx, _parentState);
		Many_actualContext _prevctx = _localctx;
		int _startState = 20;
		enterRecursionRule(_localctx, 20, RULE_many_actual, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(136);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,10,_ctx) ) {
			case 1:
				{
				setState(134);
				actual();
				}
				break;
			case 2:
				{
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(146);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,12,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					{
					_localctx = new Many_actualContext(_parentctx, _parentState);
					pushNewRecursionContext(_localctx, _startState, RULE_many_actual);
					setState(138);
					if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
					setState(139);
					match(CommaToken);
					setState(140);
					actual();
					setState(142);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,11,_ctx) ) {
					case 1:
						{
						setState(141);
						match(CommaToken);
						}
						break;
					}
					}
					} 
				}
				setState(148);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,12,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class ActualsContext extends ParserRuleContext {
		public TerminalNode OpenParenToken() { return getToken(CoolParser.OpenParenToken, 0); }
		public Many_actualContext many_actual() {
			return getRuleContext(Many_actualContext.class,0);
		}
		public TerminalNode CloseParenToken() { return getToken(CoolParser.CloseParenToken, 0); }
		public ActualsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_actuals; }
	}

	public final ActualsContext actuals() throws RecognitionException {
		ActualsContext _localctx = new ActualsContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_actuals);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(149);
			match(OpenParenToken);
			setState(150);
			many_actual(0);
			setState(151);
			match(CloseParenToken);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Native_nonterminalContext extends ParserRuleContext {
		public TerminalNode NativeToken() { return getToken(CoolParser.NativeToken, 0); }
		public Native_nonterminalContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_native_nonterminal; }
	}

	public final Native_nonterminalContext native_nonterminal() throws RecognitionException {
		Native_nonterminalContext _localctx = new Native_nonterminalContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_native_nonterminal);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(153);
			match(NativeToken);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Expr_manyContext extends ParserRuleContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public Expr_manyContext expr_many() {
			return getRuleContext(Expr_manyContext.class,0);
		}
		public List<TerminalNode> SemiColonToken() { return getTokens(CoolParser.SemiColonToken); }
		public TerminalNode SemiColonToken(int i) {
			return getToken(CoolParser.SemiColonToken, i);
		}
		public Expr_manyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expr_many; }
	}

	public final Expr_manyContext expr_many() throws RecognitionException {
		return expr_many(0);
	}

	private Expr_manyContext expr_many(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		Expr_manyContext _localctx = new Expr_manyContext(_ctx, _parentState);
		Expr_manyContext _prevctx = _localctx;
		int _startState = 26;
		enterRecursionRule(_localctx, 26, RULE_expr_many, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(158);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,13,_ctx) ) {
			case 1:
				{
				setState(156);
				expr(0);
				}
				break;
			case 2:
				{
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(168);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,15,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					{
					_localctx = new Expr_manyContext(_parentctx, _parentState);
					pushNewRecursionContext(_localctx, _startState, RULE_expr_many);
					setState(160);
					if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
					setState(161);
					match(SemiColonToken);
					setState(162);
					expr(0);
					setState(164);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,14,_ctx) ) {
					case 1:
						{
						setState(163);
						match(SemiColonToken);
						}
						break;
					}
					}
					} 
				}
				setState(170);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,15,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class ExprContext extends ParserRuleContext {
		public TerminalNode MinusToken() { return getToken(CoolParser.MinusToken, 0); }
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public TerminalNode NotToken() { return getToken(CoolParser.NotToken, 0); }
		public Native_nonterminalContext native_nonterminal() {
			return getRuleContext(Native_nonterminalContext.class,0);
		}
		public TerminalNode VarToken() { return getToken(CoolParser.VarToken, 0); }
		public List<TerminalNode> NameToken() { return getTokens(CoolParser.NameToken); }
		public TerminalNode NameToken(int i) {
			return getToken(CoolParser.NameToken, i);
		}
		public TerminalNode ColonToken() { return getToken(CoolParser.ColonToken, 0); }
		public TerminalNode AssignToken() { return getToken(CoolParser.AssignToken, 0); }
		public TerminalNode NewToken() { return getToken(CoolParser.NewToken, 0); }
		public ActualsContext actuals() {
			return getRuleContext(ActualsContext.class,0);
		}
		public TerminalNode IfToken() { return getToken(CoolParser.IfToken, 0); }
		public TerminalNode OpenParenToken() { return getToken(CoolParser.OpenParenToken, 0); }
		public TerminalNode CloseParenToken() { return getToken(CoolParser.CloseParenToken, 0); }
		public TerminalNode ElseToken() { return getToken(CoolParser.ElseToken, 0); }
		public TerminalNode WhileToken() { return getToken(CoolParser.WhileToken, 0); }
		public TerminalNode MatchToken() { return getToken(CoolParser.MatchToken, 0); }
		public TerminalNode WithToken() { return getToken(CoolParser.WithToken, 0); }
		public ArmsContext arms() {
			return getRuleContext(ArmsContext.class,0);
		}
		public TerminalNode OpenBraceToken() { return getToken(CoolParser.OpenBraceToken, 0); }
		public Expr_manyContext expr_many() {
			return getRuleContext(Expr_manyContext.class,0);
		}
		public TerminalNode CloseBraceToken() { return getToken(CoolParser.CloseBraceToken, 0); }
		public AtomicContext atomic() {
			return getRuleContext(AtomicContext.class,0);
		}
		public TerminalNode DotToken() { return getToken(CoolParser.DotToken, 0); }
		public TerminalNode MultiplyToken() { return getToken(CoolParser.MultiplyToken, 0); }
		public TerminalNode DivideToken() { return getToken(CoolParser.DivideToken, 0); }
		public TerminalNode PlusToken() { return getToken(CoolParser.PlusToken, 0); }
		public TerminalNode LessThanToken() { return getToken(CoolParser.LessThanToken, 0); }
		public TerminalNode LessThanEqualsToken() { return getToken(CoolParser.LessThanEqualsToken, 0); }
		public TerminalNode EqualsToken() { return getToken(CoolParser.EqualsToken, 0); }
		public TerminalNode NotEqualsToken() { return getToken(CoolParser.NotEqualsToken, 0); }
		public TerminalNode AndToken() { return getToken(CoolParser.AndToken, 0); }
		public TerminalNode OrToken() { return getToken(CoolParser.OrToken, 0); }
		public ExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expr; }
	}

	public final ExprContext expr() throws RecognitionException {
		return expr(0);
	}

	private ExprContext expr(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExprContext _localctx = new ExprContext(_ctx, _parentState);
		ExprContext _prevctx = _localctx;
		int _startState = 28;
		enterRecursionRule(_localctx, 28, RULE_expr, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(220);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,16,_ctx) ) {
			case 1:
				{
				}
				break;
			case 2:
				{
				setState(172);
				match(MinusToken);
				setState(173);
				expr(24);
				}
				break;
			case 3:
				{
				setState(174);
				match(NotToken);
				setState(175);
				expr(23);
				}
				break;
			case 4:
				{
				setState(176);
				native_nonterminal();
				}
				break;
			case 5:
				{
				setState(177);
				match(VarToken);
				setState(178);
				match(NameToken);
				setState(179);
				match(ColonToken);
				setState(180);
				match(NameToken);
				setState(181);
				match(AssignToken);
				setState(182);
				expr(11);
				}
				break;
			case 6:
				{
				setState(183);
				match(NewToken);
				setState(184);
				match(NameToken);
				setState(185);
				actuals();
				}
				break;
			case 7:
				{
				setState(186);
				match(IfToken);
				setState(187);
				match(OpenParenToken);
				setState(188);
				expr(0);
				setState(189);
				match(CloseParenToken);
				setState(190);
				expr(0);
				setState(191);
				match(ElseToken);
				setState(192);
				expr(9);
				}
				break;
			case 8:
				{
				setState(194);
				match(WhileToken);
				setState(195);
				match(OpenParenToken);
				setState(196);
				expr(0);
				setState(197);
				match(CloseParenToken);
				setState(198);
				expr(8);
				}
				break;
			case 9:
				{
				setState(200);
				match(MatchToken);
				setState(201);
				expr(0);
				setState(202);
				match(WithToken);
				setState(203);
				arms();
				}
				break;
			case 10:
				{
				setState(205);
				match(NameToken);
				setState(206);
				match(AssignToken);
				setState(207);
				expr(6);
				}
				break;
			case 11:
				{
				setState(208);
				match(OpenBraceToken);
				setState(209);
				expr_many(0);
				setState(210);
				match(CloseBraceToken);
				}
				break;
			case 12:
				{
				setState(212);
				atomic();
				}
				break;
			case 13:
				{
				setState(213);
				match(NameToken);
				setState(214);
				actuals();
				}
				break;
			case 14:
				{
				setState(215);
				match(NameToken);
				}
				break;
			case 15:
				{
				setState(216);
				match(OpenParenToken);
				setState(217);
				expr(0);
				setState(218);
				match(CloseParenToken);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(257);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,18,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(255);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,17,_ctx) ) {
					case 1:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(222);
						if (!(precpred(_ctx, 25))) throw new FailedPredicateException(this, "precpred(_ctx, 25)");
						setState(223);
						match(DotToken);
						setState(224);
						expr(26);
						}
						break;
					case 2:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(225);
						if (!(precpred(_ctx, 22))) throw new FailedPredicateException(this, "precpred(_ctx, 22)");
						setState(226);
						match(MultiplyToken);
						setState(227);
						expr(23);
						}
						break;
					case 3:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(228);
						if (!(precpred(_ctx, 21))) throw new FailedPredicateException(this, "precpred(_ctx, 21)");
						setState(229);
						match(DivideToken);
						setState(230);
						expr(22);
						}
						break;
					case 4:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(231);
						if (!(precpred(_ctx, 20))) throw new FailedPredicateException(this, "precpred(_ctx, 20)");
						setState(232);
						match(PlusToken);
						setState(233);
						expr(21);
						}
						break;
					case 5:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(234);
						if (!(precpred(_ctx, 19))) throw new FailedPredicateException(this, "precpred(_ctx, 19)");
						setState(235);
						match(MinusToken);
						setState(236);
						expr(20);
						}
						break;
					case 6:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(237);
						if (!(precpred(_ctx, 18))) throw new FailedPredicateException(this, "precpred(_ctx, 18)");
						setState(238);
						match(LessThanToken);
						setState(239);
						expr(19);
						}
						break;
					case 7:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(240);
						if (!(precpred(_ctx, 17))) throw new FailedPredicateException(this, "precpred(_ctx, 17)");
						setState(241);
						match(LessThanEqualsToken);
						setState(242);
						expr(18);
						}
						break;
					case 8:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(243);
						if (!(precpred(_ctx, 16))) throw new FailedPredicateException(this, "precpred(_ctx, 16)");
						setState(244);
						match(EqualsToken);
						setState(245);
						expr(17);
						}
						break;
					case 9:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(246);
						if (!(precpred(_ctx, 15))) throw new FailedPredicateException(this, "precpred(_ctx, 15)");
						setState(247);
						match(NotEqualsToken);
						setState(248);
						expr(16);
						}
						break;
					case 10:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(249);
						if (!(precpred(_ctx, 14))) throw new FailedPredicateException(this, "precpred(_ctx, 14)");
						setState(250);
						match(AndToken);
						setState(251);
						expr(15);
						}
						break;
					case 11:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(252);
						if (!(precpred(_ctx, 13))) throw new FailedPredicateException(this, "precpred(_ctx, 13)");
						setState(253);
						match(OrToken);
						setState(254);
						expr(14);
						}
						break;
					}
					} 
				}
				setState(259);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,18,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class AtomicContext extends ParserRuleContext {
		public TerminalNode NullLiteralToken() { return getToken(CoolParser.NullLiteralToken, 0); }
		public TerminalNode BooleanLiteralToken() { return getToken(CoolParser.BooleanLiteralToken, 0); }
		public TerminalNode DecimalLiteralToken() { return getToken(CoolParser.DecimalLiteralToken, 0); }
		public TerminalNode StringLiteralToken() { return getToken(CoolParser.StringLiteralToken, 0); }
		public TerminalNode OpenParenToken() { return getToken(CoolParser.OpenParenToken, 0); }
		public TerminalNode CloseParenToken() { return getToken(CoolParser.CloseParenToken, 0); }
		public AtomicContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_atomic; }
	}

	public final AtomicContext atomic() throws RecognitionException {
		AtomicContext _localctx = new AtomicContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_atomic);
		try {
			setState(266);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case NullLiteralToken:
				enterOuterAlt(_localctx, 1);
				{
				setState(260);
				match(NullLiteralToken);
				}
				break;
			case BooleanLiteralToken:
				enterOuterAlt(_localctx, 2);
				{
				setState(261);
				match(BooleanLiteralToken);
				}
				break;
			case DecimalLiteralToken:
				enterOuterAlt(_localctx, 3);
				{
				setState(262);
				match(DecimalLiteralToken);
				}
				break;
			case StringLiteralToken:
				enterOuterAlt(_localctx, 4);
				{
				setState(263);
				match(StringLiteralToken);
				}
				break;
			case OpenParenToken:
				enterOuterAlt(_localctx, 5);
				{
				setState(264);
				match(OpenParenToken);
				setState(265);
				match(CloseParenToken);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Null_armContext extends ParserRuleContext {
		public TerminalNode CaseToken() { return getToken(CoolParser.CaseToken, 0); }
		public TerminalNode NullLiteralToken() { return getToken(CoolParser.NullLiteralToken, 0); }
		public TerminalNode ArrowToken() { return getToken(CoolParser.ArrowToken, 0); }
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public Null_armContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_null_arm; }
	}

	public final Null_armContext null_arm() throws RecognitionException {
		Null_armContext _localctx = new Null_armContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_null_arm);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(268);
			match(CaseToken);
			setState(269);
			match(NullLiteralToken);
			setState(270);
			match(ArrowToken);
			setState(271);
			expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Typed_armContext extends ParserRuleContext {
		public TerminalNode CaseToken() { return getToken(CoolParser.CaseToken, 0); }
		public List<TerminalNode> NameToken() { return getTokens(CoolParser.NameToken); }
		public TerminalNode NameToken(int i) {
			return getToken(CoolParser.NameToken, i);
		}
		public TerminalNode ColonToken() { return getToken(CoolParser.ColonToken, 0); }
		public TerminalNode ArrowToken() { return getToken(CoolParser.ArrowToken, 0); }
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public Typed_armContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_typed_arm; }
	}

	public final Typed_armContext typed_arm() throws RecognitionException {
		Typed_armContext _localctx = new Typed_armContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_typed_arm);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(273);
			match(CaseToken);
			setState(274);
			match(NameToken);
			setState(275);
			match(ColonToken);
			setState(276);
			match(NameToken);
			setState(277);
			match(ArrowToken);
			setState(278);
			expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ArmContext extends ParserRuleContext {
		public Null_armContext null_arm() {
			return getRuleContext(Null_armContext.class,0);
		}
		public Typed_armContext typed_arm() {
			return getRuleContext(Typed_armContext.class,0);
		}
		public ArmContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arm; }
	}

	public final ArmContext arm() throws RecognitionException {
		ArmContext _localctx = new ArmContext(_ctx, getState());
		enterRule(_localctx, 36, RULE_arm);
		try {
			setState(282);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,20,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(280);
				null_arm();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(281);
				typed_arm();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Many_armContext extends ParserRuleContext {
		public ArmContext arm() {
			return getRuleContext(ArmContext.class,0);
		}
		public Many_armContext many_arm() {
			return getRuleContext(Many_armContext.class,0);
		}
		public TerminalNode CommaToken() { return getToken(CoolParser.CommaToken, 0); }
		public Many_armContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_many_arm; }
	}

	public final Many_armContext many_arm() throws RecognitionException {
		return many_arm(0);
	}

	private Many_armContext many_arm(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		Many_armContext _localctx = new Many_armContext(_ctx, _parentState);
		Many_armContext _prevctx = _localctx;
		int _startState = 38;
		enterRecursionRule(_localctx, 38, RULE_many_arm, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			{
			setState(285);
			arm();
			}
			_ctx.stop = _input.LT(-1);
			setState(292);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,21,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					{
					_localctx = new Many_armContext(_parentctx, _parentState);
					pushNewRecursionContext(_localctx, _startState, RULE_many_arm);
					setState(287);
					if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
					setState(288);
					match(CommaToken);
					setState(289);
					arm();
					}
					} 
				}
				setState(294);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,21,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class ArmsContext extends ParserRuleContext {
		public TerminalNode OpenBraceToken() { return getToken(CoolParser.OpenBraceToken, 0); }
		public Many_armContext many_arm() {
			return getRuleContext(Many_armContext.class,0);
		}
		public TerminalNode CloseBraceToken() { return getToken(CoolParser.CloseBraceToken, 0); }
		public ArmsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arms; }
	}

	public final ArmsContext arms() throws RecognitionException {
		ArmsContext _localctx = new ArmsContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_arms);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(295);
			match(OpenBraceToken);
			setState(296);
			many_arm(0);
			setState(297);
			match(CloseBraceToken);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 3:
			return many_features_sempred((Many_featuresContext)_localctx, predIndex);
		case 7:
			return many_formal_sempred((Many_formalContext)_localctx, predIndex);
		case 10:
			return many_actual_sempred((Many_actualContext)_localctx, predIndex);
		case 13:
			return expr_many_sempred((Expr_manyContext)_localctx, predIndex);
		case 14:
			return expr_sempred((ExprContext)_localctx, predIndex);
		case 19:
			return many_arm_sempred((Many_armContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean many_features_sempred(Many_featuresContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 3);
		}
		return true;
	}
	private boolean many_formal_sempred(Many_formalContext _localctx, int predIndex) {
		switch (predIndex) {
		case 1:
			return precpred(_ctx, 3);
		}
		return true;
	}
	private boolean many_actual_sempred(Many_actualContext _localctx, int predIndex) {
		switch (predIndex) {
		case 2:
			return precpred(_ctx, 3);
		}
		return true;
	}
	private boolean expr_many_sempred(Expr_manyContext _localctx, int predIndex) {
		switch (predIndex) {
		case 3:
			return precpred(_ctx, 3);
		}
		return true;
	}
	private boolean expr_sempred(ExprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 4:
			return precpred(_ctx, 25);
		case 5:
			return precpred(_ctx, 22);
		case 6:
			return precpred(_ctx, 21);
		case 7:
			return precpred(_ctx, 20);
		case 8:
			return precpred(_ctx, 19);
		case 9:
			return precpred(_ctx, 18);
		case 10:
			return precpred(_ctx, 17);
		case 11:
			return precpred(_ctx, 16);
		case 12:
			return precpred(_ctx, 15);
		case 13:
			return precpred(_ctx, 14);
		case 14:
			return precpred(_ctx, 13);
		}
		return true;
	}
	private boolean many_arm_sempred(Many_armContext _localctx, int predIndex) {
		switch (predIndex) {
		case 15:
			return precpred(_ctx, 2);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3/\u012e\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\3\2\7\2.\n\2\f\2\16\2\61\13\2"+
		"\3\2\3\2\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3"+
		"\3\3\3\3\3\3\3\3\3\5\3I\n\3\3\4\3\4\5\4M\n\4\3\5\3\5\3\5\5\5R\n\5\3\5"+
		"\3\5\3\5\3\5\5\5X\n\5\7\5Z\n\5\f\5\16\5]\13\5\3\6\3\6\3\6\3\6\3\7\5\7"+
		"d\n\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\b\3\b\3\b\3\b\3\t\3\t\3\t\5\t"+
		"u\n\t\3\t\3\t\3\t\3\t\5\t{\n\t\7\t}\n\t\f\t\16\t\u0080\13\t\3\n\3\n\3"+
		"\n\3\n\3\13\3\13\3\f\3\f\3\f\5\f\u008b\n\f\3\f\3\f\3\f\3\f\5\f\u0091\n"+
		"\f\7\f\u0093\n\f\f\f\16\f\u0096\13\f\3\r\3\r\3\r\3\r\3\16\3\16\3\17\3"+
		"\17\3\17\5\17\u00a1\n\17\3\17\3\17\3\17\3\17\5\17\u00a7\n\17\7\17\u00a9"+
		"\n\17\f\17\16\17\u00ac\13\17\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3"+
		"\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3"+
		"\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3"+
		"\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\5\20\u00df"+
		"\n\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20"+
		"\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3\20"+
		"\3\20\3\20\3\20\3\20\3\20\3\20\7\20\u0102\n\20\f\20\16\20\u0105\13\20"+
		"\3\21\3\21\3\21\3\21\3\21\3\21\5\21\u010d\n\21\3\22\3\22\3\22\3\22\3\22"+
		"\3\23\3\23\3\23\3\23\3\23\3\23\3\23\3\24\3\24\5\24\u011d\n\24\3\25\3\25"+
		"\3\25\3\25\3\25\3\25\7\25\u0125\n\25\f\25\16\25\u0128\13\25\3\26\3\26"+
		"\3\26\3\26\3\26\2\b\b\20\26\34\36(\27\2\4\6\b\n\f\16\20\22\24\26\30\32"+
		"\34\36 \"$&(*\2\2\2\u0148\2/\3\2\2\2\4H\3\2\2\2\6L\3\2\2\2\bQ\3\2\2\2"+
		"\n^\3\2\2\2\fc\3\2\2\2\16m\3\2\2\2\20t\3\2\2\2\22\u0081\3\2\2\2\24\u0085"+
		"\3\2\2\2\26\u008a\3\2\2\2\30\u0097\3\2\2\2\32\u009b\3\2\2\2\34\u00a0\3"+
		"\2\2\2\36\u00de\3\2\2\2 \u010c\3\2\2\2\"\u010e\3\2\2\2$\u0113\3\2\2\2"+
		"&\u011c\3\2\2\2(\u011e\3\2\2\2*\u0129\3\2\2\2,.\5\4\3\2-,\3\2\2\2.\61"+
		"\3\2\2\2/-\3\2\2\2/\60\3\2\2\2\60\62\3\2\2\2\61/\3\2\2\2\62\63\7\2\2\3"+
		"\63\3\3\2\2\2\64\65\7)\2\2\65\66\7+\2\2\66\67\5\22\n\2\678\7*\2\289\7"+
		"+\2\29:\5\30\r\2:;\5\n\6\2;I\3\2\2\2<=\7)\2\2=>\7+\2\2>?\5\22\n\2?@\7"+
		"*\2\2@A\5\32\16\2AB\5\n\6\2BI\3\2\2\2CD\7)\2\2DE\7+\2\2EF\5\22\n\2FG\5"+
		"\n\6\2GI\3\2\2\2H\64\3\2\2\2H<\3\2\2\2HC\3\2\2\2I\5\3\2\2\2JM\5\36\20"+
		"\2KM\5\f\7\2LJ\3\2\2\2LK\3\2\2\2M\7\3\2\2\2NO\b\5\1\2OR\5\6\4\2PR\3\2"+
		"\2\2QN\3\2\2\2QP\3\2\2\2R[\3\2\2\2ST\f\5\2\2TU\7\t\2\2UW\5\6\4\2VX\7\t"+
		"\2\2WV\3\2\2\2WX\3\2\2\2XZ\3\2\2\2YS\3\2\2\2Z]\3\2\2\2[Y\3\2\2\2[\\\3"+
		"\2\2\2\\\t\3\2\2\2][\3\2\2\2^_\7\7\2\2_`\5\b\5\2`a\7\b\2\2a\13\3\2\2\2"+
		"bd\7\"\2\2cb\3\2\2\2cd\3\2\2\2de\3\2\2\2ef\7\'\2\2fg\7+\2\2gh\5\22\n\2"+
		"hi\7\f\2\2ij\7+\2\2jk\7\13\2\2kl\5\36\20\2l\r\3\2\2\2mn\7+\2\2no\7\f\2"+
		"\2op\7+\2\2p\17\3\2\2\2qr\b\t\1\2ru\5\16\b\2su\3\2\2\2tq\3\2\2\2ts\3\2"+
		"\2\2u~\3\2\2\2vw\f\5\2\2wx\7\n\2\2xz\5\16\b\2y{\7\n\2\2zy\3\2\2\2z{\3"+
		"\2\2\2{}\3\2\2\2|v\3\2\2\2}\u0080\3\2\2\2~|\3\2\2\2~\177\3\2\2\2\177\21"+
		"\3\2\2\2\u0080~\3\2\2\2\u0081\u0082\7\5\2\2\u0082\u0083\5\20\t\2\u0083"+
		"\u0084\7\6\2\2\u0084\23\3\2\2\2\u0085\u0086\5\36\20\2\u0086\25\3\2\2\2"+
		"\u0087\u0088\b\f\1\2\u0088\u008b\5\24\13\2\u0089\u008b\3\2\2\2\u008a\u0087"+
		"\3\2\2\2\u008a\u0089\3\2\2\2\u008b\u0094\3\2\2\2\u008c\u008d\f\5\2\2\u008d"+
		"\u008e\7\n\2\2\u008e\u0090\5\24\13\2\u008f\u0091\7\n\2\2\u0090\u008f\3"+
		"\2\2\2\u0090\u0091\3\2\2\2\u0091\u0093\3\2\2\2\u0092\u008c\3\2\2\2\u0093"+
		"\u0096\3\2\2\2\u0094\u0092\3\2\2\2\u0094\u0095\3\2\2\2\u0095\27\3\2\2"+
		"\2\u0096\u0094\3\2\2\2\u0097\u0098\7\5\2\2\u0098\u0099\5\26\f\2\u0099"+
		"\u009a\7\6\2\2\u009a\31\3\2\2\2\u009b\u009c\7(\2\2\u009c\33\3\2\2\2\u009d"+
		"\u009e\b\17\1\2\u009e\u00a1\5\36\20\2\u009f\u00a1\3\2\2\2\u00a0\u009d"+
		"\3\2\2\2\u00a0\u009f\3\2\2\2\u00a1\u00aa\3\2\2\2\u00a2\u00a3\f\5\2\2\u00a3"+
		"\u00a4\7\t\2\2\u00a4\u00a6\5\36\20\2\u00a5\u00a7\7\t\2\2\u00a6\u00a5\3"+
		"\2\2\2\u00a6\u00a7\3\2\2\2\u00a7\u00a9\3\2\2\2\u00a8\u00a2\3\2\2\2\u00a9"+
		"\u00ac\3\2\2\2\u00aa\u00a8\3\2\2\2\u00aa\u00ab\3\2\2\2\u00ab\35\3\2\2"+
		"\2\u00ac\u00aa\3\2\2\2\u00ad\u00df\b\20\1\2\u00ae\u00af\7\17\2\2\u00af"+
		"\u00df\5\36\20\32\u00b0\u00b1\7\20\2\2\u00b1\u00df\5\36\20\31\u00b2\u00df"+
		"\5\32\16\2\u00b3\u00b4\7 \2\2\u00b4\u00b5\7+\2\2\u00b5\u00b6\7\f\2\2\u00b6"+
		"\u00b7\7+\2\2\u00b7\u00b8\7\13\2\2\u00b8\u00df\5\36\20\r\u00b9\u00ba\7"+
		"\37\2\2\u00ba\u00bb\7+\2\2\u00bb\u00df\5\30\r\2\u00bc\u00bd\7%\2\2\u00bd"+
		"\u00be\7\5\2\2\u00be\u00bf\5\36\20\2\u00bf\u00c0\7\6\2\2\u00c0\u00c1\5"+
		"\36\20\2\u00c1\u00c2\7\36\2\2\u00c2\u00c3\5\36\20\13\u00c3\u00df\3\2\2"+
		"\2\u00c4\u00c5\7$\2\2\u00c5\u00c6\7\5\2\2\u00c6\u00c7\5\36\20\2\u00c7"+
		"\u00c8\7\6\2\2\u00c8\u00c9\5\36\20\n\u00c9\u00df\3\2\2\2\u00ca\u00cb\7"+
		"#\2\2\u00cb\u00cc\5\36\20\2\u00cc\u00cd\7&\2\2\u00cd\u00ce\5*\26\2\u00ce"+
		"\u00df\3\2\2\2\u00cf\u00d0\7+\2\2\u00d0\u00d1\7\13\2\2\u00d1\u00df\5\36"+
		"\20\b\u00d2\u00d3\7\7\2\2\u00d3\u00d4\5\34\17\2\u00d4\u00d5\7\b\2\2\u00d5"+
		"\u00df\3\2\2\2\u00d6\u00df\5 \21\2\u00d7\u00d8\7+\2\2\u00d8\u00df\5\30"+
		"\r\2\u00d9\u00df\7+\2\2\u00da\u00db\7\5\2\2\u00db\u00dc\5\36\20\2\u00dc"+
		"\u00dd\7\6\2\2\u00dd\u00df\3\2\2\2\u00de\u00ad\3\2\2\2\u00de\u00ae\3\2"+
		"\2\2\u00de\u00b0\3\2\2\2\u00de\u00b2\3\2\2\2\u00de\u00b3\3\2\2\2\u00de"+
		"\u00b9\3\2\2\2\u00de\u00bc\3\2\2\2\u00de\u00c4\3\2\2\2\u00de\u00ca\3\2"+
		"\2\2\u00de\u00cf\3\2\2\2\u00de\u00d2\3\2\2\2\u00de\u00d6\3\2\2\2\u00de"+
		"\u00d7\3\2\2\2\u00de\u00d9\3\2\2\2\u00de\u00da\3\2\2\2\u00df\u0103\3\2"+
		"\2\2\u00e0\u00e1\f\33\2\2\u00e1\u00e2\7\r\2\2\u00e2\u0102\5\36\20\34\u00e3"+
		"\u00e4\f\30\2\2\u00e4\u00e5\7\21\2\2\u00e5\u0102\5\36\20\31\u00e6\u00e7"+
		"\f\27\2\2\u00e7\u00e8\7\22\2\2\u00e8\u0102\5\36\20\30\u00e9\u00ea\f\26"+
		"\2\2\u00ea\u00eb\7\16\2\2\u00eb\u0102\5\36\20\27\u00ec\u00ed\f\25\2\2"+
		"\u00ed\u00ee\7\17\2\2\u00ee\u0102\5\36\20\26\u00ef\u00f0\f\24\2\2\u00f0"+
		"\u00f1\7\24\2\2\u00f1\u0102\5\36\20\25\u00f2\u00f3\f\23\2\2\u00f3\u00f4"+
		"\7\25\2\2\u00f4\u0102\5\36\20\24\u00f5\u00f6\f\22\2\2\u00f6\u00f7\7\26"+
		"\2\2\u00f7\u0102\5\36\20\23\u00f8\u00f9\f\21\2\2\u00f9\u00fa\7\27\2\2"+
		"\u00fa\u0102\5\36\20\22\u00fb\u00fc\f\20\2\2\u00fc\u00fd\7\30\2\2\u00fd"+
		"\u0102\5\36\20\21\u00fe\u00ff\f\17\2\2\u00ff\u0100\7\31\2\2\u0100\u0102"+
		"\5\36\20\20\u0101\u00e0\3\2\2\2\u0101\u00e3\3\2\2\2\u0101\u00e6\3\2\2"+
		"\2\u0101\u00e9\3\2\2\2\u0101\u00ec\3\2\2\2\u0101\u00ef\3\2\2\2\u0101\u00f2"+
		"\3\2\2\2\u0101\u00f5\3\2\2\2\u0101\u00f8\3\2\2\2\u0101\u00fb\3\2\2\2\u0101"+
		"\u00fe\3\2\2\2\u0102\u0105\3\2\2\2\u0103\u0101\3\2\2\2\u0103\u0104\3\2"+
		"\2\2\u0104\37\3\2\2\2\u0105\u0103\3\2\2\2\u0106\u010d\7\32\2\2\u0107\u010d"+
		"\7\33\2\2\u0108\u010d\7\34\2\2\u0109\u010d\7,\2\2\u010a\u010b\7\5\2\2"+
		"\u010b\u010d\7\6\2\2\u010c\u0106\3\2\2\2\u010c\u0107\3\2\2\2\u010c\u0108"+
		"\3\2\2\2\u010c\u0109\3\2\2\2\u010c\u010a\3\2\2\2\u010d!\3\2\2\2\u010e"+
		"\u010f\7\35\2\2\u010f\u0110\7\32\2\2\u0110\u0111\7\23\2\2\u0111\u0112"+
		"\5\36\20\2\u0112#\3\2\2\2\u0113\u0114\7\35\2\2\u0114\u0115\7+\2\2\u0115"+
		"\u0116\7\f\2\2\u0116\u0117\7+\2\2\u0117\u0118\7\23\2\2\u0118\u0119\5\36"+
		"\20\2\u0119%\3\2\2\2\u011a\u011d\5\"\22\2\u011b\u011d\5$\23\2\u011c\u011a"+
		"\3\2\2\2\u011c\u011b\3\2\2\2\u011d\'\3\2\2\2\u011e\u011f\b\25\1\2\u011f"+
		"\u0120\5&\24\2\u0120\u0126\3\2\2\2\u0121\u0122\f\4\2\2\u0122\u0123\7\n"+
		"\2\2\u0123\u0125\5&\24\2\u0124\u0121\3\2\2\2\u0125\u0128\3\2\2\2\u0126"+
		"\u0124\3\2\2\2\u0126\u0127\3\2\2\2\u0127)\3\2\2\2\u0128\u0126\3\2\2\2"+
		"\u0129\u012a\7\7\2\2\u012a\u012b\5(\25\2\u012b\u012c\7\b\2\2\u012c+\3"+
		"\2\2\2\30/HLQW[ctz~\u008a\u0090\u0094\u00a0\u00a6\u00aa\u00de\u0101\u0103"+
		"\u010c\u011c\u0126";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}