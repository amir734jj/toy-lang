using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Models;

namespace JavaScriptCodeGen
{
    internal class ReturnExpressionVisitor : Visitor<IEnumerable<(Guid, IToken)>>
    {
        public override IEnumerable<(Guid, IToken)> Visit(AndToken andToken)
        {
            return new[] { (andToken.Id, (IToken)andToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(OrToken orToken)
        {
            return new[] { (orToken.Id, (IToken)orToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(NativeToken nativeToken)
        {
            return new[] { (nativeToken.Id, (IToken)nativeToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(AssignToken assignToken)
        {
            return new[] { (assignToken.Id, (IToken)assignToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(WhileToken whileToken)
        {
            return Enumerable.Empty<(Guid, IToken)>();
        }

        public override IEnumerable<(Guid, IToken)> Visit(CondToken condToken)
        {
            return new[] { (condToken.Id, (IToken)condToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(VarDeclToken varDeclToken)
        {
            return new[] { (varDeclToken.Id, (IToken)varDeclToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(FunctionDeclToken functionDeclToken)
        {
            return Visit(functionDeclToken.Body);
        }

        public override IEnumerable<(Guid, IToken)> Visit(BlockToken blockToken)
        {
            var rest = Enumerable.Empty<(Guid, IToken)>();
            if (blockToken.Tokens.Inner.Count > 0)
            {
                var last = blockToken.Tokens.Inner[^1];
                rest = new[] { (last.Id, last) };
            }

            return new[] { (blockToken.Id, (IToken)blockToken) }.Concat(rest).Concat(Visit(blockToken.Tokens).Where(x => blockToken.Tokens.Inner.All(y => x.Item1 != y.Id))).ToList();
        }

        public override IEnumerable<(Guid, IToken)> Visit(FunctionCallToken functionCallToken)
        {
            return new[] { (functionCallToken.Id, (IToken)functionCallToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(NegateToken negateToken)
        {
            return new[] { (negateToken.Id, (IToken)negateToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(NotToken notToken)
        {
            return new[] { (notToken.Id, (IToken)notToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(AddToken addToken)
        {
            return new[] { (addToken.Id, (IToken)addToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(EqualsToken equalsToken)
        {
            return new[] { (equalsToken.Id, (IToken)equalsToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(NotEqualsToken notEqualsToken)
        {
            return new[] { (notEqualsToken.Id, (IToken)notEqualsToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(LessThanToken lessThanToken)
        {
            return new[] { (lessThanToken.Id, (IToken)lessThanToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(LessThanEqualsToken lessThanEqualsToken)
        {
            return new[] { (lessThanEqualsToken.Id, (IToken)lessThanEqualsToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(SubtractToken subtractToken)
        {
            return new[] { (subtractToken.Id, (IToken)subtractToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(DivideToken divideToken)
        {
            return new[] { (divideToken.Id, (IToken)divideToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(MultiplyToken multiplyToken)
        {
            return new[] { (multiplyToken.Id, (IToken)multiplyToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(AtomicToken atomicToken)
        {
            return new[] { (atomicToken.Id, (IToken)atomicToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(VariableToken variableToken)
        {
            return new[] { (variableToken.Id, (IToken)variableToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(AccessToken accessToken)
        {
            return new[] { (accessToken.Id, (IToken)accessToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(InstantiationToken instantiationToken)
        {
            return new[] { (instantiationToken.Id, (IToken)instantiationToken) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(Formal formal)
        {
            return new[] { (formal.Id, (IToken)formal) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(ClassToken classToken)
        {
            var result = classToken.Features.Inner.SelectMany(Visit)
                .Where(x => classToken.Features.Inner.All(y => x.Item1 != y.Id))
                .ToList();

            return result;
        }

        public override IEnumerable<(Guid, IToken)> Visit(TypedArmToken typedArmToken)
        {
            return Visit(typedArmToken).Where(x => x.Item1 != typedArmToken.Result.Id).ToList();
        }

        public override IEnumerable<(Guid, IToken)> Visit(NullArmToken nullArmToken)
        {
            return Visit(nullArmToken).Where(x => x.Item1 != nullArmToken.Result.Id).ToList();
        }

        public override IEnumerable<(Guid, IToken)> Visit(Formals formals)
        {
            return Enumerable.Empty<(Guid, IToken)>();
        }

        public override IEnumerable<(Guid, IToken)> Visit(Tokens tokens)
        {
            return tokens.Inner
                .SelectMany(Visit)
                .ToImmutableList();
        }

        public override IEnumerable<(Guid, IToken)> Visit(Classes classes)
        {
            return classes.Inner
                .SelectMany(Visit)
                .ToImmutableList();
        }

        public override IEnumerable<(Guid, IToken)> Visit(Match match)
        {
            return new[] { (match.Id, (IToken)match) };
        }

        public override IEnumerable<(Guid, IToken)> Visit(Arms arms)
        {
            return arms.Inner
                .SelectMany(Visit)
                .ToImmutableList();
        }
    }
}