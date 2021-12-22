using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Models.Extensions;
using static Models.Constants;

namespace AntlrParser
{
    internal class AstBuilderVisitor : CoolParserBaseVisitor<Token>
    {
        public override Token VisitClass_nonterminal(CoolParser.Class_nonterminalContext context)
        {
            if (context.native_nonterminal() != null)
            {
                return new ClassToken(
                    context.NameToken().First().GetText(),
                    (Formals)Visit(context.formals()),
                    NOTHING_TYPE,
                    new Tokens(new List<Token>().AsValueSemantics()),
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
                    new Tokens(new List<Token>().AsValueSemantics()),
                    (Tokens)Visit(context.features()));
            }

            throw new ArgumentException();
        }

        public override Token VisitFeatures(CoolParser.FeaturesContext context)
        {
            return Visit(context.many_features());
        }

        public override Token VisitNative_nonterminal(CoolParser.Native_nonterminalContext context)
        {
            return new NativeToken();
        }

        public override Token VisitNull_arm(CoolParser.Null_armContext context)
        {
            return new NullArmToken(Visit(context.expr()));
        }

        public override Token VisitMany_features(CoolParser.Many_featuresContext context)
        {
            var single = context.feature();
            var many = context.many_features();
            
            // Epsilon
            if (single is null or { IsEmpty: true } && many is null or { IsEmpty: true })
            {
                return new Tokens(new List<Token>().AsValueSemantics());
            }

            if (many is null or { IsEmpty: true })
            {
                return new Tokens(new List<Token> { Visit(single) }.AsValueSemantics());
            }

            var lhs = (Tokens)Visit(many);
            var rhs = Visit(single);
            return new Tokens(lhs.Inner.Concat(new[] { rhs }).AsValueSemantics());
        }

        public override Token VisitFeature(CoolParser.FeatureContext context)
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

        public override Token VisitClasses(CoolParser.ClassesContext context)
        {
            var classes = context.class_nonterminal().Select(Visit).Cast<ClassToken>();

            return new Classes(classes.AsValueSemantics());
        }

        public override Token VisitTyped_arm(CoolParser.Typed_armContext context)
        {
            return new TypedArmToken(
                context.NameToken().First().GetText(),
                context.NameToken().Last().GetText(),
                Visit(context.expr()));
        }

        public override Token VisitExpr(CoolParser.ExprContext context)
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
            
            if (context.MinusToken() != null && context.expr().Length == 1)
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
            
            if (context.DotToken() != null && Visit(context.expr().Last()) is FunctionCallToken functionCall)
            {
                return new AccessToken(
                    Visit(context.expr().First()),
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
                return new VariableToken(context.NameToken().First().GetText());
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
            
            return base.VisitExpr(context);
        }

        public override Token VisitFormal(CoolParser.FormalContext context)
        {
            return new Formal(context.NameToken().First().GetText(), context.NameToken().Last().GetText());
        }

        public override Token VisitMany_formal(CoolParser.Many_formalContext context)
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

        public override Token VisitFormals(CoolParser.FormalsContext context)
        {
            return Visit(context.many_formal());
        }

        public override Token VisitArm(CoolParser.ArmContext context)
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

        public override Token VisitArms(CoolParser.ArmsContext context)
        {
            return Visit(context.many_arm());
        }

        public override Token VisitMany_arm(CoolParser.Many_armContext context)
        {
            var single = context.arm();
            var many = context.many_arm();
            
            // Epsilon
            if (single is null or { IsEmpty: true } && many is null or { IsEmpty: true })
            {
                return new Arms(new List<ArmToken>().AsValueSemantics());
            }

            if (many is null or { IsEmpty: true })
            {
                return new Arms(new List<ArmToken> { (ArmToken)Visit(single) }.AsValueSemantics());
            }

            var lhs = (Arms)Visit(many);
            var rhs = (ArmToken)Visit(single);
            return new Arms(lhs.Inner.Concat(new[] { rhs }).AsValueSemantics());
        }

        public override Token VisitActual(CoolParser.ActualContext context)
        {
            return Visit(context.expr());
        }

        public override Token VisitMany_actual(CoolParser.Many_actualContext context)
        {
            var single = context.actual();
            var many = context.many_actual();
            
            // Epsilon
            if (single is null or { IsEmpty: true } && many is null or { IsEmpty: true })
            {
                return new Tokens(new List<Token>().AsValueSemantics());
            }

            if (many is null or { IsEmpty: true })
            {
                return new Tokens(new List<Token> { Visit(single) }.AsValueSemantics());
            }

            var lhs = (Tokens)Visit(many);
            var rhs = Visit(single);
            return new Tokens(lhs.Inner.Concat(new[] { rhs }).AsValueSemantics());
        }

        public override Token VisitActuals(CoolParser.ActualsContext context)
        {
            return Visit(context.many_actual());
        }

        public override Token VisitFunction_decl(CoolParser.Function_declContext context)
        {
            return new FunctionDeclToken(
                context.OverrideToken() != null,
                context.NameToken().First().GetText(),
                (Formals)VisitFormals(context.formals()),
                context.NameToken().Last().GetText(),
                Visit(context.expr()));
        }

        public override Token VisitExpr_many(CoolParser.Expr_manyContext context)
        {
            var single = context.expr();
            var many = context.expr_many();
            
            // Epsilon
            if (single is null or { IsEmpty: true } && many is null or { IsEmpty: true })
            {
                return new Tokens(new List<Token>().AsValueSemantics());
            }

            if (many is null or { IsEmpty: true })
            {
                return new Tokens(new List<Token> { Visit(single) }.AsValueSemantics());
            }

            var lhs = (Tokens)Visit(many);
            var rhs = Visit(single);
            return new Tokens(lhs.Inner.Concat(new[] { rhs }).AsValueSemantics());
        }

        public override Token VisitAtomic(CoolParser.AtomicContext context)
        {
            if (context.NullLiteralToken() != null)
            {
                return new AtomicToken(null);
            }
            
            if (context.StringLiteralToken() != null)
            {
                return new AtomicToken(context.StringLiteralToken().GetText());
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