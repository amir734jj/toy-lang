using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Models.Extensions;

namespace AntlrParser
{
    internal class AstBuilderVisitor : CoolParserBaseVisitor<Token>
    {
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
            
            if (!context.expr_many().IsEmpty)
            {
                var many = (Tokens)Visit(context.expr().First());
                return new BlockToken(many);
            }
            
            if (context.PlusToken() != null)
            {
                return new AddToken(
                    Visit(context.expr().First()),
                    Visit(context.expr().Last()));
            }
            
            if (context.MinusToken() != null)
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
            
            if (!context.actuals().IsEmpty)
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
            
            if (!context.atomic().IsEmpty)
            {
                return Visit(context.atomic());
            }
            
            if (!context.native_nonterminal().IsEmpty)
            {
                return Visit(context.native_nonterminal());
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
            if (single.IsEmpty && many.IsEmpty)
            {
                return new Formals(new List<Formal>().AsValueSemantics());
            }

            if (many.IsEmpty)
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
            
            if (!typedArm.IsEmpty)
            {
                return new TypedArmToken(
                    typedArm.NameToken().First().GetText(),
                    typedArm.NameToken().Last().GetText(),
                    Visit(typedArm.expr()));
            }

            if (!nullArm.IsEmpty)
            {
                return new NullArmToken(
                    Visit(typedArm.expr()));
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
            if (single.IsEmpty && many.IsEmpty)
            {
                return new Arms(new List<ArmToken>().AsValueSemantics());
            }

            if (many.IsEmpty)
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
    }
}