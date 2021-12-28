using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Models;

namespace JavaScriptCodeGen
{
    internal class ReturnExpressionVisitor : IVisitor<IEnumerable<(Guid, IToken)>>
    {
        public IVisitor<IEnumerable<(Guid, IToken)>> AsVisitor()
        {
            return this;
        }

        public IEnumerable<(Guid, IToken)> Visit(AndToken andToken)
        {
            return new[] { (andToken.Id, (IToken)andToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(OrToken orToken)
        {
            return new[] { (orToken.Id, (IToken)orToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(NativeToken nativeToken)
        {
            return new[] { (nativeToken.Id, (IToken)nativeToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(AssignToken assignToken)
        {
            return new[] { (assignToken.Id, (IToken)assignToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(WhileToken whileToken)
        {
            return Enumerable.Empty<(Guid, IToken)>();
        }

        public IEnumerable<(Guid, IToken)> Visit(CondToken condToken)
        {
            return new[] { (condToken.Id, (IToken)condToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(VarDeclToken varDeclToken)
        {
            return new[] { (varDeclToken.Id, (IToken)varDeclToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(FunctionDeclToken functionDeclToken)
        {
            return AsVisitor().Visit(functionDeclToken.Body);
        }

        public IEnumerable<(Guid, IToken)> Visit(BlockToken blockToken)
        {
            var rest = Enumerable.Empty<(Guid, IToken)>();
            if (blockToken.Tokens.Inner.Count > 0)
            {
                var last = blockToken.Tokens.Inner[^1];
                rest = new[] { (last.Id, last) };
            }

            return new[] { (blockToken.Id, (IToken)blockToken) }.Concat(rest).Concat(Visit(blockToken.Tokens).Where(x => blockToken.Tokens.Inner.All(y => x.Item1 != y.Id))).ToList();
        }

        public IEnumerable<(Guid, IToken)> Visit(FunctionCallToken functionCallToken)
        {
            return new[] { (functionCallToken.Id, (IToken)functionCallToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(NegateToken negateToken)
        {
            return new[] { (negateToken.Id, (IToken)negateToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(NotToken notToken)
        {
            return new[] { (notToken.Id, (IToken)notToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(AddToken addToken)
        {
            return new[] { (addToken.Id, (IToken)addToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(EqualsToken equalsToken)
        {
            return new[] { (equalsToken.Id, (IToken)equalsToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(NotEqualsToken notEqualsToken)
        {
            return new[] { (notEqualsToken.Id, (IToken)notEqualsToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(LessThanToken lessThanToken)
        {
            return new[] { (lessThanToken.Id, (IToken)lessThanToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(LessThanEqualsToken lessThanEqualsToken)
        {
            return new[] { (lessThanEqualsToken.Id, (IToken)lessThanEqualsToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(SubtractToken subtractToken)
        {
            return new[] { (subtractToken.Id, (IToken)subtractToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(DivideToken divideToken)
        {
            return new[] { (divideToken.Id, (IToken)divideToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(MultiplyToken multiplyToken)
        {
            return new[] { (multiplyToken.Id, (IToken)multiplyToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(AtomicToken atomicToken)
        {
            return new[] { (atomicToken.Id, (IToken)atomicToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(VariableToken variableToken)
        {
            return new[] { (variableToken.Id, (IToken)variableToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(AccessToken accessToken)
        {
            return new[] { (accessToken.Id, (IToken)accessToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(InstantiationToken instantiationToken)
        {
            return new[] { (instantiationToken.Id, (IToken)instantiationToken) };
        }

        public IEnumerable<(Guid, IToken)> Visit(Formal formal)
        {
            return new[] { (formal.Id, (IToken)formal) };
        }

        public IEnumerable<(Guid, IToken)> Visit(ClassToken classToken)
        {
            var result = classToken.Features.Inner.SelectMany(AsVisitor().Visit)
                .Where(x => classToken.Features.Inner.All(y => x.Item1 != y.Id))
                .ToList();

            return result;
        }

        public IEnumerable<(Guid, IToken)> Visit(TypedArmToken typedArmToken)
        {
            return Visit(typedArmToken).Where(x => x.Item1 != typedArmToken.Result.Id).ToList();
        }

        public IEnumerable<(Guid, IToken)> Visit(NullArmToken nullArmToken)
        {
            return Visit(nullArmToken).Where(x => x.Item1 != nullArmToken.Result.Id).ToList();
        }

        public IEnumerable<(Guid, IToken)> Visit(Formals formals)
        {
            return Enumerable.Empty<(Guid, IToken)>();
        }

        public IEnumerable<(Guid, IToken)> Visit(Tokens tokens)
        {
            return tokens.Inner
                .SelectMany(AsVisitor().Visit)
                .ToImmutableList();
        }

        public IEnumerable<(Guid, IToken)> Visit(Classes classes)
        {
            return classes.Inner
                .SelectMany(AsVisitor().Visit)
                .ToImmutableList();
        }

        public IEnumerable<(Guid, IToken)> Visit(Match match)
        {
            return new[] { (match.Id, (IToken)match) };
        }

        public IEnumerable<(Guid, IToken)> Visit(Arms arms)
        {
            return arms.Inner
                .SelectMany(AsVisitor().Visit)
                .ToImmutableList();
        }
    }
}