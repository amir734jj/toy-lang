using System;
using System.IO;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using Models;

namespace AntlrParser
{
    class Amir : CoolParserBaseVisitor<Token>
    {
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
            return new Tokens();
        }

        public override Token VisitActual(CoolParser.ActualContext context)
        {
            return Visit(context.expr());
        }
    }
}