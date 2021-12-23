using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Models;

namespace JavaScriptCodeGen
{
    internal class ReturnExpressionVisitor : Visitor<IEnumerable<Guid>>
    {
        public override IEnumerable<Guid> Visit(AndToken andToken)
        {
            return new[] { andToken.Id };
        }

        public override IEnumerable<Guid> Visit(OrToken orToken)
        {
            return new[] { orToken.Id };
        }

        public override IEnumerable<Guid> Visit(NativeToken nativeToken)
        {
            return new[] { nativeToken.Id };
        }

        public override IEnumerable<Guid> Visit(AssignToken assignToken)
        {
            return new[] { assignToken.Id };
        }

        public override IEnumerable<Guid> Visit(WhileToken whileToken)
        {
            return Visit(whileToken.Body);
        }

        public override IEnumerable<Guid> Visit(CondToken condToken)
        {
            return  Visit(condToken.IfToken).Concat(Visit(condToken.ElseToken)).ToImmutableList();
        }

        public override IEnumerable<Guid> Visit(VarDeclToken varDeclToken)
        {
            return new[] { varDeclToken.Id };
        }

        public override IEnumerable<Guid> Visit(FunctionDeclToken functionDeclToken)
        {
            return Visit(functionDeclToken.Body);
        }

        public override IEnumerable<Guid> Visit(BlockToken blockToken)
        {
            return blockToken.Tokens.Inner.Count == 0 ? new[] { blockToken.Id } : Visit(blockToken.Tokens.Inner[^1]);
        }

        public override IEnumerable<Guid> Visit(FunctionCallToken functionCallToken)
        {
            return new[] { functionCallToken.Id };
        }

        public override IEnumerable<Guid> Visit(NegateToken negateToken)
        {
            return new[] { negateToken.Id };
        }

        public override IEnumerable<Guid> Visit(NotToken notToken)
        {
            return new[] { notToken.Id };
        }

        public override IEnumerable<Guid> Visit(AddToken addToken)
        {
            return new[] { addToken.Id };
        }

        public override IEnumerable<Guid> Visit(EqualsToken equalsToken)
        {
            return new[] { equalsToken.Id };
        }

        public override IEnumerable<Guid> Visit(NotEqualsToken notEqualsToken)
        {
            return new[] { notEqualsToken.Id };
        }

        public override IEnumerable<Guid> Visit(LessThanToken lessThanToken)
        {
            return new[] { lessThanToken.Id };
        }

        public override IEnumerable<Guid> Visit(LessThanEqualsToken lessThanEqualsToken)
        {
            return new[] { lessThanEqualsToken.Id };
        }

        public override IEnumerable<Guid> Visit(SubtractToken subtractToken)
        {
            return new[] { subtractToken.Id };
        }

        public override IEnumerable<Guid> Visit(DivideToken divideToken)
        {
            return new[] { divideToken.Id };
        }

        public override IEnumerable<Guid> Visit(MultiplyToken multiplyToken)
        {
            return new[] { multiplyToken.Id };
        }

        public override IEnumerable<Guid> Visit(AtomicToken atomicToken)
        {
            return new[] { atomicToken.Id };
        }

        public override IEnumerable<Guid> Visit(VariableToken variableToken)
        {
            return new[] { variableToken.Id };
        }

        public override IEnumerable<Guid> Visit(AccessToken accessToken)
        {
            return new[] { accessToken.Id };
        }

        public override IEnumerable<Guid> Visit(InstantiationToken instantiationToken)
        {
            return new[] { instantiationToken.Id };
        }

        public override IEnumerable<Guid> Visit(Formal formal)
        {
            return new[] { formal.Id };
        }

        public override IEnumerable<Guid> Visit(ClassToken classToken)
        {
            return classToken.Features.Inner
                .Where(x => x is FunctionDeclToken)
                .SelectMany(Visit)
                .ToImmutableList();
        }

        public override IEnumerable<Guid> Visit(TypedArmToken typedArmToken)
        {
            return Visit(typedArmToken.Result);
        }

        public override IEnumerable<Guid> Visit(NullArmToken nullArmToken)
        {
            return Visit(nullArmToken.Result);
        }

        public override IEnumerable<Guid> Visit(Formals formals)
        {
            return Enumerable.Empty<Guid>();
        }

        public override IEnumerable<Guid> Visit(Tokens tokens)
        {
            return tokens.Inner
                .SelectMany(Visit)
                .ToImmutableList();
        }

        public override IEnumerable<Guid> Visit(Classes classes)
        {
            return classes.Inner
                .SelectMany(Visit)
                .ToImmutableList();
        }

        public override IEnumerable<Guid> Visit(Match match)
        {
            return Visit(match.Arms);
        }

        public override IEnumerable<Guid> Visit(Arms arms)
        {
            return arms.Inner
                .SelectMany(Visit)
                .ToImmutableList();
        }
    }
}