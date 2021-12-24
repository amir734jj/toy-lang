using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Models.Extensions;
using static Models.Constants;

namespace AntlrParser
{
    internal class AstBuilderVisitor : CoolParserBaseVisitor<IToken>
    {
        public override IToken VisitClass_nonterminal(CoolParser.Class_nonterminalContext context)
        {
            if (context.native_nonterminal() != null)
            {
                return new ClassToken(
                    context.NameToken().First().GetText(),
                    (Formals)Visit(context.formals()),
                    NOTHING_TYPE,
                    new Tokens(new List<IToken>().AsValueSemantics()),
                    (Tokens)Visit(context.features()));
            }
            
            if (context.NameToken().Length == 2)
            {
                return new ClassToken(
                    context.NameToken().First().GetText(),
                    (Formals)Visit(context.formals()),
                    context.NameToken().Last().GetText(),
                    (Tokens)Visit(context.actuals()),
                    (Tokens)Visit(context.features()));
            }
            
            if (context.NameToken().Length == 1)
            {
                return new ClassToken(
                    context.NameToken().First().GetText(),
                    (Formals)Visit(context.formals()),
                    ANY_TYPE,
                    new Tokens(new List<IToken>().AsValueSemantics()),
                    (Tokens)Visit(context.features()));
            }

            throw new ArgumentException();
        }

        public override IToken VisitFeatures(CoolParser.FeaturesContext context)
        {
            return Visit(context.many_features());
        }

        public override IToken VisitNative_nonterminal(CoolParser.Native_nonterminalContext context)
        {
            return new NativeToken();
        }

        public override IToken VisitNull_arm(CoolParser.Null_armContext context)
        {
            return new NullArmToken(Visit(context.expr()));
        }

        public override IToken VisitMany_features(CoolParser.Many_featuresContext context)
        {
            var single = context.feature();
            var many = context.many_features();
            
            // Epsilon
            if (single is null or { IsEmpty: true } && many is null or { IsEmpty: true })
            {
                return new Tokens(new List<IToken>().AsValueSemantics());
            }

            if (many is null or { IsEmpty: true })
            {
                return new Tokens(new List<IToken> { Visit(single) }.AsValueSemantics());
            }

            var lhs = (Tokens)Visit(many);
            var rhs = Visit(single);
            return new Tokens(lhs.Inner.Concat(new[] { rhs }).AsValueSemantics());
        }

        public override IToken VisitFeature(CoolParser.FeatureContext context)
        {
            var functionDecl = context.function_decl();
            if (functionDecl is { IsEmpty: false})
            {
                return Visit(functionDecl);
            }

            var expr = context.expr();
            if (expr is { IsEmpty: false})
            {
                return Visit(expr);
            }

            throw new ArgumentException();
        }

        public override IToken VisitClasses(CoolParser.ClassesContext context)
        {
            var classes = context.class_nonterminal().Select(Visit).Cast<ClassToken>();

            return new Classes(classes.AsValueSemantics());
        }

        public override IToken VisitTyped_arm(CoolParser.Typed_armContext context)
        {
            return new TypedArmToken(
                context.NameToken().First().GetText(),
                context.NameToken().Last().GetText(),
                Visit(context.expr()));
        }

        public override IToken VisitExpr(CoolParser.ExprContext context)
        {
            if (context.VarToken() != null)
            {
                return new VarDeclToken(
                    context.NameToken().First().GetText(),
                    context.NameToken().Last().GetText(),
                    Visit(context.expr().First()));
            }
            
            if (context.AssignToken() != null)
            {
                return new AssignToken(
                    context.NameToken().First().GetText(),
                    Visit(context.expr().First()));
            }
            
            if (context.IfToken() != null)
            {
                return new CondToken(
                    Visit(context.expr()[0]),
                    Visit(context.expr()[1]),
                    Visit(context.expr()[2]));
            }
            
            if (context.WhileToken() != null)
            {
                return new WhileToken(
                    Visit(context.expr().First()),
                    Visit(context.expr().Last()));
            }
            
            if (context.expr_many() is { IsEmpty: false })
            {
                var many = (Tokens)Visit(context.expr_many());
                return new BlockToken(many);
            }
            
            if (context.PlusToken() != null)
            {
                return new AddToken(
                    Visit(context.expr().First()),
                    Visit(context.expr().Last()));
            }
            
            if (context.MinusToken() != null && context.expr().Length == 2)
            {
                return new SubtractToken(
                    Visit(context.expr().First()),
                    Visit(context.expr().Last()));
            }
            
            if (context.MultiplyToken() != null)
            {
                return new MultiplyToken(
                    Visit(context.expr().First()),
                    Visit(context.expr().Last()));
            }
            
            if (context.DivideToken() != null)
            {
                return new DivideToken(
                    Visit(context.expr().First()),
                    Visit(context.expr().Last()));
            }
            
            if (context.LessThanToken() != null)
            {
                return new LessThanToken(
                    Visit(context.expr().First()),
                    Visit(context.expr().Last()));
            }
            
            if (context.LessThanEqualsToken() != null)
            {
                return new LessThanEqualsToken(
                    Visit(context.expr().First()),
                    Visit(context.expr().Last()));
            }
            
            if (context.EqualsToken() != null)
            {
                return new EqualsToken(
                    Visit(context.expr().First()),
                    Visit(context.expr().Last()));
            }
            
            if (context.NotEqualsToken() != null)
            {
                return new NotEqualsToken(
                    Visit(context.expr().First()),
                    Visit(context.expr().Last()));
            }
            
            if (context.AndToken() != null)
            {
                return new AndToken(
                    Visit(context.expr().First()),
                    Visit(context.expr().Last()));
            }
            
            if (context.OrToken() != null)
            {
                return new OrToken(
                    Visit(context.expr().First()),
                    Visit(context.expr().Last()));
            }
            
            if (context.DotToken() != null && context.expr().Length == 2 && Visit(context.expr().Last()) is FunctionCallToken functionCall)
            {
                var receiver = Visit(context.expr().First());
                return new AccessToken(
                    receiver,
                    functionCall);
            }
            
            if (context.actuals() is { IsEmpty: false})
            {
                return new FunctionCallToken(
                    context.NameToken().First().GetText(),
                    (Tokens)Visit(context.actuals()));
            }
            
            if (context.MatchToken() != null)
            {
                return new Match(
                    Visit(context.expr().First()),
                    (Arms)Visit(context.arms()));
            }
            
            if (context.atomic() is { IsEmpty: false } )
            {
                return Visit(context.atomic());
            }
            
            if (context.native_nonterminal() is { IsEmpty: false } )
            {
                return Visit(context.native_nonterminal());
            }

            if (context.OpenBraceToken() != null && context.CloseBraceToken() !=null)
            {
                return Visit(context.expr_many());
            }

            if (context.NotToken() != null)
            {
                return new NotToken(Visit(context.expr().First()));
            }

            if (context.MinusToken() != null && context.expr().Length == 1)
            {
                return new NegateToken(Visit(context.expr().First()));
            }

            if (context.NameToken().Length == 1 && context.expr().Length == 0)
            {
                var name = context.NameToken().First().GetText();
                return new VariableToken(name);
            }

            if (context.OpenParenToken() != null && context.CloseParenToken() != null)
            {
                if (context.expr() is { Length: 0 })
                {
                    return new AtomicToken(UNIT_SYMBOL_VALUE);
                }

                if (context.expr() is { Length: 1 })
                {
                    return Visit(context.expr().First());
                }
            }
            
            throw new ArgumentException();
        }

        public override IToken VisitFormal(CoolParser.FormalContext context)
        {
            return new Formal(context.NameToken().First().GetText(), context.NameToken().Last().GetText());
        }

        public override IToken VisitMany_formal(CoolParser.Many_formalContext context)
        {
            var single = context.formal();
            var many = context.many_formal();
            
            // Epsilon
            if (single is null or { IsEmpty: true } && many is null or { IsEmpty: true })
            {
                return new Formals(new List<Formal>().AsValueSemantics());
            }

            if (many is null or { IsEmpty: true })
            {
                return new Formals(new List<Formal> { (Formal)Visit(single) }.AsValueSemantics());
            }

            var lhs = (Formals)Visit(many);
            var rhs = (Formal)Visit(single);
            return new Formals(lhs.Inner.Concat(new[] { rhs }).AsValueSemantics());
        }

        public override IToken VisitFormals(CoolParser.FormalsContext context)
        {
            return Visit(context.many_formal());
        }

        public override IToken VisitArm(CoolParser.ArmContext context)
        {
            var typedArm = context.typed_arm();
            var nullArm = context.null_arm();
            
            if (typedArm is { IsEmpty: false})
            {
                return Visit(context.typed_arm());
            }

            if (nullArm is { IsEmpty: false})
            {
                return Visit(context.null_arm());
            }

            throw new ArgumentException();
        }

        public override IToken VisitArms(CoolParser.ArmsContext context)
        {
            return Visit(context.many_arm());
        }

        public override IToken VisitMany_arm(CoolParser.Many_armContext context)
        {
            var single = context.arm();
            var many = context.many_arm();
            
            // Epsilon
            if (single is null or { IsEmpty: true } && many is null or { IsEmpty: true })
            {
                return new Arms(new List<IArmToken>().AsValueSemantics());
            }

            if (many is null or { IsEmpty: true })
            {
                return new Arms(new List<IArmToken> { (IArmToken)Visit(single) }.AsValueSemantics());
            }

            var lhs = (Arms)Visit(many);
            var rhs = (IArmToken)Visit(single);
            return new Arms(lhs.Inner.Concat(new[] { rhs }).AsValueSemantics());
        }

        public override IToken VisitActual(CoolParser.ActualContext context)
        {
            return Visit(context.expr());
        }

        public override IToken VisitMany_actual(CoolParser.Many_actualContext context)
        {
            var single = context.actual();
            var many = context.many_actual();
            
            // Epsilon
            if (single is null or { IsEmpty: true } && many is null or { IsEmpty: true })
            {
                return new Tokens(new List<IToken>().AsValueSemantics());
            }

            if (many is null or { IsEmpty: true })
            {
                return new Tokens(new List<IToken> { Visit(single) }.AsValueSemantics());
            }

            var lhs = (Tokens)Visit(many);
            var rhs = Visit(single);
            return new Tokens(lhs.Inner.Concat(new[] { rhs }).AsValueSemantics());
        }

        public override IToken VisitActuals(CoolParser.ActualsContext context)
        {
            return Visit(context.many_actual());
        }

        public override IToken VisitFunction_decl(CoolParser.Function_declContext context)
        {
            return new FunctionDeclToken(
                context.OverrideToken() != null,
                context.NameToken().First().GetText(),
                (Formals)VisitFormals(context.formals()),
                context.NameToken().Last().GetText(),
                Visit(context.expr()));
        }

        public override IToken VisitExpr_many(CoolParser.Expr_manyContext context)
        {
            var single = context.expr();
            var many = context.expr_many();
            
            // Epsilon
            if (single is null or { IsEmpty: true } && many is null or { IsEmpty: true })
            {
                return new Tokens(new List<IToken>().AsValueSemantics());
            }

            if (many is null or { IsEmpty: true })
            {
                return new Tokens(new List<IToken> { Visit(single) }.AsValueSemantics());
            }

            var lhs = (Tokens)Visit(many);
            var rhs = Visit(single);
            return new Tokens(lhs.Inner.Concat(new[] { rhs }).AsValueSemantics());
        }

        public override IToken VisitAtomic(CoolParser.AtomicContext context)
        {
            if (context.NullLiteralToken() != null)
            {
                return new AtomicToken(null);
            }
            
            if (context.StringLiteralToken() != null)
            {
                return new AtomicToken(context.StringLiteralToken().GetText().Trim('"'));
            }
            
            if (context.DecimalLiteralToken() != null)
            {
                return new AtomicToken(int.Parse(context.DecimalLiteralToken().GetText()));
            }
            
            if (context.BooleanLiteralToken() != null)
            {
                return new AtomicToken(bool.Parse(context.BooleanLiteralToken().GetText()));
            }
            
            if (context.OpenParenToken() != null && context.OpenParenToken() != null)
            {
                return new AtomicToken(UNIT_SYMBOL_VALUE);
            }
            
            throw new ArgumentException();
        }
    }
}