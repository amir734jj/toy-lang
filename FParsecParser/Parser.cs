using System;
using System.Collections.Generic;
using System.Linq;
using FParsec;
using FParsec.CSharp;
using Microsoft.FSharp.Core;
using Models;
using Models.Extensions;
using Models.Interfaces;
using static FParsec.CSharp.PrimitivesCS; // combinator functions
using static FParsec.CSharp.CharParsersCS; // pre-defined parsers
using static Models.Constants;

namespace FParsecParser
{
    public static class Parser
    {
        /// <summary>
        /// Any type of variable name
        /// </summary>
        public static FSharpFunc<CharStream<Unit>, Reply<string>> Name()
        {
            var invalidChars = new[]
                { ':', '"', ' ', '{', '}', '=', '(', ')', '\n', ';', ',', '*', '!', '.', '<', '>', '+', '-', '*', '/' };
            var reservedKeyword = new[]
            {
                "match", "while", "with", "class", "extends", "if", "else", "case", "def", "null", "var", "new",
                "native", "overrides"
            };

            var identifier = Many1Chars(NoneOf(invalidChars))
                .AndTry(id => reservedKeyword.Contains(id) ? Fail<string>("reserved") : Return(id))
                .Label("identifier");

            return identifier;
        }

        /// <summary>
        /// Expression are:
        ///   1) Assignment: [Variable] = [Expression]
        ///   2) Declaration: var [Variable] = [Expression] 
        ///   3) Block: { [Expression]* }
        ///   4) Function call: [Expression] ( [Expression]* )
        ///   5) Conditional: if ( [Expression] ) [Expression] else [Expression]
        ///   6) Binary: [Expression] [+-/*] [Expression]
        ///   7) Unary: [-!] [Expression]
        ///   7) Variable: [Name]
        /// </summary>
        public static FSharpFunc<CharStream<Unit>, Reply<IToken>> Expression()
        {
            // [Atomic] = Number | Boolean | Null | String
            FSharpFunc<CharStream<Unit>, Reply<IToken>> Atomic(
                FSharpFunc<CharStream<Unit>, Reply<IToken>> expressionRec)
            {
                var quotedStringP = Regex(@"""(?:[^\\""]|\\.)*""")
                    .Label("string")
                    .Map(x => (IToken)new AtomicToken(x.Trim('"')));
                var numberP = Int
                    .Label("number")
                    .Map(x => (IToken)new AtomicToken(x));
                var boolP = StringP("true").Or(StringP("false"))
                    .Lbl("bool")
                    .Map(x => (IToken)new AtomicToken(x == "true"));
                var nullP = Skip("null")
                    .Label("null")
                    .Return((IToken)new AtomicToken(null));
                var unitLiteral = Wrap('(', ')')
                    .Return((IToken)new AtomicToken(UNIT_SYMBOL_VALUE));

                var atomicP = Choice(nullP, numberP, quotedStringP, boolP, unitLiteral).Label("atomic");

                return SkipComments(atomicP);
            }

            static FSharpFunc<CharStream<Unit>, Reply<IToken>> Declaration(
                FSharpFunc<CharStream<Unit>, Reply<IToken>> expressionRec)
            {
                // Declaration
                var declarationP = Skip("var").AndTry_(WS1).AndRTry(Name()).AndLTry(WS).AndLTry(Skip(':'))
                    .AndLTry(WS).AndTry(Name()).AndLTry(WS).AndLTry(Skip('=')).AndLTry(WS)
                    .AndTry(expressionRec)
                    .Label("decl")
                    .Map(x => (IToken)new VarDeclToken(x.Item1.Item1, x.Item1.Item2, x.Item2));

                return SkipComments(declarationP);
            }

            static FSharpFunc<CharStream<Unit>, Reply<IToken>> Assignment(
                FSharpFunc<CharStream<Unit>, Reply<IToken>> expressionRec)
            {
                // Assignment
                var assignmentP = Name().AndLTry(WS).AndLTry(Skip('=')).AndLTry(WS).AndTry(expressionRec)
                    .Label("assign")
                    .Map(x => (IToken)new AssignToken(x.Item1, x.Item2));

                return SkipComments(assignmentP);
            }

            static FSharpFunc<CharStream<Unit>, Reply<IToken>> Conditional(
                FSharpFunc<CharStream<Unit>, Reply<IToken>> expressionRec)
            {
                // Conditional
                var conditionalP = Skip("if").And_(WS)
                    .AndRTry(Wrap('(', expressionRec, ')'))
                    .AndLTry(WS)
                    .AndTry(expressionRec).AndTry(WS).AndLTry(Skip("else")).AndLTry(WS).AndTry(expressionRec)
                    .Label("cond")
                    .Map(x =>
                        (IToken)new CondToken(x.Item1.Item1, x.Item1.Item2, x.Item2));

                return SkipComments(conditionalP);
            }

            static FSharpFunc<CharStream<Unit>, Reply<IToken>> While(
                FSharpFunc<CharStream<Unit>, Reply<IToken>> expressionRec)
            {
                // While
                var conditionalP = Skip("while").AndTry_(WS)
                    .AndRTry(Wrap('(', expressionRec, ')'))
                    .AndLTry(WS)
                    .AndTry(expressionRec)
                    .Label("while")
                    .Map(x =>
                        (IToken)new WhileToken(x.Item1, x.Item2));

                return SkipComments(conditionalP);
            }

            static FSharpFunc<CharStream<Unit>, Reply<IToken>> Block(
                FSharpFunc<CharStream<Unit>, Reply<IToken>> expressionRec)
            {
                // Block
                var blockExprP = SepBy('{', expressionRec, '}', Skip(';'), canEndWithSep: true, canBeEmpty: true)
                    .Label("block")
                    .Map(x => (IToken)new BlockToken(new Tokens(x)));

                return SkipComments(blockExprP);
            }

            static FSharpFunc<CharStream<Unit>, Reply<IToken>> Variable(
                FSharpFunc<CharStream<Unit>, Reply<IToken>> expressionRec)
            {
                // Variable
                var variableP = Name()
                    .Label("variable")
                    .Map(x => (IToken)new VariableToken(x));

                return SkipComments(variableP);
            }

            static FSharpFunc<CharStream<Unit>, Reply<IToken>> Instantiation(
                FSharpFunc<CharStream<Unit>, Reply<IToken>> expressionRec)
            {
                // Instantiation
                var instantiationP = Skip("new")
                    .AndTry_(WS)
                    .AndRTry(Name())
                    .AndTry(SepBy('(', expressionRec, ')', Skip(','), canBeEmpty: true, canEndWithSep: false))
                    .Label("instantiation")
                    .Map(x => (IToken)new InstantiationToken(x.Item1, new Tokens(x.Item2)));

                return SkipComments(instantiationP);
            }

            static FSharpFunc<CharStream<Unit>, Reply<IToken>> FunctionCall(
                FSharpFunc<CharStream<Unit>, Reply<IToken>> expressionRec)
            {
                // Function call
                var functionCallP = Name()
                    .AndTry(SepBy('(', expressionRec, ')', Skip(','), canBeEmpty: true, canEndWithSep: false))
                    .Label("functionCall")
                    .Map(x => (IToken)new FunctionCallToken(x.Item1, new Tokens(x.Item2)));

                return SkipComments(functionCallP);
            }

            static FSharpFunc<CharStream<Unit>, Reply<IToken>> Native(
                FSharpFunc<CharStream<Unit>, Reply<IToken>> expressionRec)
            {
                // Native keyword
                var nativeP = Skip("native")
                    .Label("native")
                    .Return((IToken)new NativeToken());

                return SkipComments(nativeP);
            }

            static FSharpFunc<CharStream<Unit>, Reply<IToken>> Match(
                FSharpFunc<CharStream<Unit>, Reply<IToken>> expressionRec)
            {
                var typeMatch = Name().AndLTry(WS).AndLTry(Skip(':')).AndLTry(WS).AndTry(Name()).AndLTry(WS)
                    .AndLTry(Skip("=>")).AndLTry(WS).AndTry(expressionRec)
                    .Map(x => (IArmToken)new TypedArmToken(x.Item1.Item1, x.Item1.Item2, x.Item2))
                    .Label("typeBranch");

                var nullMatch = Skip("null").AndTry_(WS).AndTry_(Skip("=>")).AndTry_(WS).AndRTry(expressionRec)
                    .Map(x => (IArmToken)new NullArmToken(x))
                    .Label("nullBranch");

                var arm = Skip("case").AndTry_(WS1)
                    .AndRTry(Choice(typeMatch, nullMatch))
                    .Label("arm");

                var arms = SepBy('{', arm, '}', Skip(','), canEndWithSep: true, canBeEmpty: false)
                    .Label("arms");

                var matchP = Skip("match").AndTry_(WS1).AndRTry(expressionRec)
                    .AndLTry(PreviousCharSatisfies(char.IsWhiteSpace)).AndL(Skip("with"))
                    .AndLTry(WS)
                    .AndTry(arms)
                    .Label("match")
                    .Map(x => (IToken)new Match(x.Item1, new Arms(x.Item2.AsValueSemantics())));

                return SkipComments(matchP);
            }

            // Source: https://en.wikipedia.org/wiki/Order_of_operations#Programming_languages
            var expressionP = new OPPBuilder<Unit, IToken, Unit>()
                .WithOperators(ops => ops
                    .AddInfix(".", 10, WS,
                        (x, y) => new AccessToken(x, (FunctionCallToken)Guard(y, y is FunctionCallToken)))
                    .AddPrefix("-", 9, WS, token => new NegateToken(token))
                    .AddPrefix("!", 9, WS, token => new NotToken(token))
                    .AddInfix("*", 8, WS, (x, y) => new MultiplyToken(x, y))
                    .AddInfix("/", 8, WS, (x, y) => new DivideToken(x, y))
                    .AddInfix("+", 7, WS, (x, y) => new AddToken(x, y))
                    .AddInfix("-", 7, WS, (x, y) => new SubtractToken(x, y))
                    .AddInfix("<=", 6, WS, (x, y) => new LessThanEqualsToken(x, y))
                    .AddInfix("<", 6, WS, (x, y) => new LessThanToken(x, y))
                    .AddInfix("==", 5, WS, (x, y) => new EqualsToken(x, y))
                    .AddInfix("!=", 5, WS, (x, y) => new NotEqualsToken(x, y))
                    .AddInfix("&&", 4, WS, (x, y) => new AndToken(x, y))
                    .AddInfix("||", 3, WS, (x, y) => new OrToken(x, y))
                )
                .WithTerms(term => Choice(
                        Native(term),
                        Declaration(term),
                        Instantiation(term),
                        Conditional(term),
                        While(term),
                        Match(term),
                        Assignment(term),
                        Block(term),
                        Atomic(term),
                        FunctionCall(term),
                        Variable(term),
                        Wrap('(', term, ')')
                    )
                )
                .Build()
                .ExpressionParser
                .Label("operator");


            return SkipComments(expressionP);
        }

        /// <summary>
        /// Function has the following form:
        ///   def [Variable](Formals): Type = [Expression]
        /// </summary>false
        public static FSharpFunc<CharStream<Unit>, Reply<FunctionDeclToken>> Function()
        {
            var prefix = Choice(StringP("override").AndLTry(WS1).AndLTry(Skip("def")), StringP("def"));

            var functionDeclP = prefix.AndLTry(WS1).AndTry_(Name()).AndLTry(WS).AndTry(Formals())
                .AndLTry(WS)
                .AndLTry(Skip(':'))
                .AndTry(SkipComments(Name()))
                .AndLTry(Skip('='))
                .AndTry(Expression())
                .Label("functionDecl")
                .Map(x => new FunctionDeclToken(
                    x.Item1.Item1.Item1.Item1 == "override",
                    x.Item1.Item1.Item1.Item2,
                    x.Item1.Item1.Item2,
                    x.Item1.Item2,
                    x.Item2));

            return SkipComments(functionDeclP);
        }

        public static FSharpFunc<CharStream<Unit>, Reply<CommentToken>> Comment()
        {
            var singleLineCommentP = Skip("//").AndRTry(RestOfLine(true))
                .Map(x => new CommentToken(x.Trim()));
            var multipleLineComment = Skip("/*").AndRTry(ManyTill(AnyChar, Skip("*/")))
                .Map(x => new CommentToken(new string(x.ToArray()).Trim()));

            return SkipWs(Choice(singleLineCommentP, multipleLineComment)
                .Label("comment"));
        }

        public static FSharpFunc<CharStream<Unit>, Reply<CommentsToken>> Comments()
        {
            var commentsP = Many(Comment(), sep: Skip(WS), canEndWithSep: true)
                .Label("comments")
                .Map(x => new CommentsToken(x.AsValueSemantics()));

            return SkipWs(commentsP);
        }

        public static FSharpFunc<CharStream<Unit>, Reply<IValueCollection<IToken>>> Features()
        {
            var items = Choice(Function().Map(x => (IToken)x), Expression())
                .Label("feature");
            var featureP = Many(items, sep: Choice(Skip(';')), canEndWithSep: true)
                .Label("features")
                .Map(x => x.AsValueSemantics());

            return SkipComments(featureP);
        }

        /// <summary>
        /// Formal are argument to functions to argument to class constructors
        /// </summary>
        /// <returns></returns>
        public static FSharpFunc<CharStream<Unit>, Reply<Formal>> Formal()
        {
            var argumentP = Name().AndLTry(WS).AndLTry(Skip(':')).AndLTry(WS).AndTry(Name())
                .Label("formal")
                .Map(x => new Formal(x.Item1, x.Item2));

            return SkipComments(argumentP);
        }

        /// <summary>
        /// Formals are list of formals separated by ','
        /// </summary>
        public static FSharpFunc<CharStream<Unit>, Reply<Formals>> Formals()
        {
            var formalsP = SepBy('(', Formal(), ')', Skip(','), canBeEmpty: true, canEndWithSep: false)
                .Label("formals")
                .Map(x => new Formals(x));

            return SkipComments(formalsP);
        }

        /// <summary>
        /// Class has the following form:
        ///   class [Variable] ( [Expression]* ) extends [Variable] ( [Expression]* ) { [Feature]* }
        /// </summary>
        public static FSharpFunc<CharStream<Unit>, Reply<ClassToken>> Class()
        {
            var classSignatureP = Name().AndLTry(WS).AndTry(Formals());
            var classPrefix = Skip("class").AndTry_(WS).AndRTry(classSignatureP);

            // class A() extends B() { }
            var classP1 = classPrefix
                .AndLTry(WS)
                .AndLTry(Skip("extends"))
                .AndLTry(WS1)
                .AndTry(Name())
                .AndTry(SepBy('(', Expression(), ')', Skip(','), canBeEmpty: true, canEndWithSep: false))
                .AndTry(Wrap('{', Features(), '}'))
                .Label("class")
                .Map(x => new ClassToken(
                    x.Item1.Item1.Item1.Item1,
                    x.Item1.Item1.Item1.Item2,
                    x.Item1.Item1.Item2,
                    new Tokens(x.Item1.Item2),
                    new Tokens(x.Item2)
                ));

            // class A() { }
            var classP2 = classPrefix
                .AndTry(Wrap('{', Features(), '}'))
                .Label("class")
                .Map(x => new ClassToken(
                    x.Item1.Item1,
                    x.Item1.Item2,
                    ANY_TYPE,
                    new Tokens(new List<IToken>().AsValueSemantics()),
                    new Tokens(x.Item2)
                ));

            // class A() extends native { }
            var classP3 = classPrefix
                .AndLTry(Skip("extends"))
                .AndTry(WS1)
                .AndTry(Skip("native"))
                .AndTry(Wrap('{', Features(), '}'))
                .Label("class")
                .Map(x => new ClassToken(
                    x.Item1.Item1,
                    x.Item1.Item2,
                    NOTHING_TYPE,
                    new Tokens(new List<IToken>().AsValueSemantics()),
                    new Tokens(x.Item2)
                ));

            return SkipComments(Choice(classP1, classP2, classP3));
        }

        public static FSharpFunc<CharStream<Unit>, Reply<Classes>> Classes()
        {
            var classesP = Many(Class(), sep: Skip(WS), canEndWithSep: true)
                .Label("classes")
                .Map(x => new Classes(x.AsValueSemantics()));

            return SkipComments(classesP);
        }

        private static FSharpFunc<CharStream<Unit>, Reply<Unit>> Wrap(
            char start,
            char end)
        {
            var wrapP = SkipComments(CharP(start)).AndRTry(SkipComments(CharP(end)));
            return Skip(wrapP);
        }
        
        private static FSharpFunc<CharStream<Unit>, Reply<T>> Wrap<T>(
            char start,
            FSharpFunc<CharStream<Unit>, Reply<T>> p,
            char end)
        {
            var wrapP = Between(SkipComments(CharP(start)), SkipComments(p), SkipComments(CharP(end)));
            return wrapP;
        }

        private static FSharpFunc<CharStream<Unit>, Reply<IValueCollection<T>>> SepBy<T>(
            char start,
            FSharpFunc<CharStream<Unit>, Reply<T>> p,
            char end,
            FSharpFunc<CharStream<Unit>, Reply<Unit>> delimiterP,
            bool canEndWithSep,
            bool canBeEmpty)
        {
            var arrItems = canBeEmpty
                ? Many(SkipComments(p), sep: delimiterP, canEndWithSep: canEndWithSep)
                : Many1(SkipComments(p), sep: delimiterP, canEndWithSep: canEndWithSep);
            var arrayP = Wrap(start, arrItems, end)
                .Map(elems => elems.AsValueSemantics());
            return arrayP;
        }

        private static FSharpFunc<CharStream<Unit>, Reply<T>> SkipComments<T>(
            FSharpFunc<CharStream<Unit>, Reply<T>> p
        )
        {
            return Skip(Comments()).AndRTry(p).AndLTry(Skip(Comments()));
        }

        private static FSharpFunc<CharStream<Unit>, Reply<T>> SkipWs<T>(
            FSharpFunc<CharStream<Unit>, Reply<T>> p
        )
        {
            return Skip(WS).AndRTry(p).AndLTry(Skip(WS));
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static T Guard<T>(T instance, bool check) where T : IToken
        {
            if (!check)
            {
                throw new Exception($"Unexpected instance: {instance}");
            }

            return instance;
        }
    }
}