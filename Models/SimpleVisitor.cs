using System;

namespace Models
{
    public class Void
    {
    }

    public class SimpleVisitor : IVisitor<Void>
    {
        private readonly Func<IToken, bool> _visit;

        public SimpleVisitor(Func<IToken, bool> visit)
        {
            _visit = visit;
        }

        public IVisitor<Void> AsVisitor()
        {
            return this;
        }

        public Void Visit(AndToken andToken)
        {
            if (!_visit(andToken))
            {
                return new Void();
            }

            AsVisitor().Visit(andToken.Left);

            AsVisitor().Visit(andToken.Right);

            return new Void();
        }

        public Void Visit(OrToken orToken)
        {
            if (!_visit(orToken))
            {
                return new Void();
            }

            AsVisitor().Visit(orToken.Left);

            AsVisitor().Visit(orToken.Right);

            return new Void();
        }

        public Void Visit(NativeToken nativeToken)
        {
            _visit(nativeToken);

            return new Void();
        }

        public Void Visit(AssignToken assignToken)
        {
            if (!_visit(assignToken))
            {
                return new Void();
            }

            AsVisitor().Visit(assignToken.Body);

            return new Void();
        }

        public Void Visit(WhileToken whileToken)
        {
            if (!_visit(whileToken))
            {
                return new Void();
            }

            AsVisitor().Visit(whileToken.Condition);

            AsVisitor().Visit(whileToken.Body);

            return new Void();
        }

        public Void Visit(CondToken condToken)
        {
            if (!_visit(condToken))
            {
                return new Void();
            }

            AsVisitor().Visit(condToken.Condition);

            AsVisitor().Visit(condToken.IfToken);

            AsVisitor().Visit(condToken.ElseToken);

            return new Void();
        }

        public Void Visit(VarDeclToken varDeclToken)
        {
            if (!_visit(varDeclToken))
            {
                return new Void();
            }

            AsVisitor().Visit(varDeclToken.Body);

            return new Void();
        }

        public Void Visit(FunctionDeclToken functionDeclToken)
        {
            if (!_visit(functionDeclToken))
            {
                return new Void();
            }

            AsVisitor().Visit(functionDeclToken.Formals);

            AsVisitor().Visit(functionDeclToken.Body);

            return new Void();
        }

        public Void Visit(BlockToken blockToken)
        {
            if (!_visit(blockToken))
            {
                return new Void();
            }

            AsVisitor().Visit(blockToken.Tokens);

            return new Void();
        }

        public Void Visit(FunctionCallToken functionCallToken)
        {
            if (!_visit(functionCallToken))
            {
                return new Void();
            }

            AsVisitor().Visit(functionCallToken.Actuals);

            return new Void();
        }

        public Void Visit(NegateToken negateToken)
        {
            if (!_visit(negateToken))
            {
                return new Void();
            }

            AsVisitor().Visit(negateToken.Token);

            return new Void();
        }

        public Void Visit(NotToken notToken)
        {
            if (!_visit(notToken))
            {
                return new Void();
            }

            AsVisitor().Visit(notToken.Token);

            return new Void();
        }

        public Void Visit(AddToken addToken)
        {
            if (!_visit(addToken))
            {
                return new Void();
            }

            AsVisitor().Visit(addToken.Left);

            AsVisitor().Visit(addToken.Right);

            return new Void();
        }

        public Void Visit(EqualsToken equalsToken)
        {
            if (!_visit(equalsToken))
            {
                return new Void();
            }

            AsVisitor().Visit(equalsToken.Left);

            AsVisitor().Visit(equalsToken.Right);

            return new Void();
        }

        public Void Visit(NotEqualsToken notEqualsToken)
        {
            if (!_visit(notEqualsToken))
            {
                return new Void();
            }

            AsVisitor().Visit(notEqualsToken.Left);

            AsVisitor().Visit(notEqualsToken.Right);

            return new Void();
        }

        public Void Visit(LessThanToken lessThanToken)
        {
            if (!_visit(lessThanToken))
            {
                return new Void();
            }

            AsVisitor().Visit(lessThanToken.Left);

            AsVisitor().Visit(lessThanToken.Right);

            return new Void();
        }

        public Void Visit(LessThanEqualsToken lessThanEqualsToken)
        {
            if (!_visit(lessThanEqualsToken))
            {
                return new Void();
            }

            AsVisitor().Visit(lessThanEqualsToken.Left);

            AsVisitor().Visit(lessThanEqualsToken.Right);

            return new Void();
        }

        public Void Visit(SubtractToken subtractToken)
        {
            if (!_visit(subtractToken))
            {
                return new Void();
            }

            AsVisitor().Visit(subtractToken.Left);

            AsVisitor().Visit(subtractToken.Right);

            return new Void();
        }

        public Void Visit(DivideToken divideToken)
        {
            if (!_visit(divideToken))
            {
                return new Void();
            }

            AsVisitor().Visit(divideToken.Left);

            AsVisitor().Visit(divideToken.Right);

            return new Void();
        }

        public Void Visit(MultiplyToken multiplyToken)
        {
            if (!_visit(multiplyToken))
            {
                return new Void();
            }

            AsVisitor().Visit(multiplyToken.Left);

            AsVisitor().Visit(multiplyToken.Right);

            return new Void();
        }

        public Void Visit(AtomicToken atomicToken)
        {
            _visit(atomicToken);

            return new Void();
        }

        public Void Visit(VariableToken variableToken)
        {
            _visit(variableToken);

            return new Void();
        }

        public Void Visit(AccessToken accessToken)
        {
            if (!_visit(accessToken))
            {
                return new Void();
            }

            AsVisitor().Visit(accessToken.Receiver);

            return new Void();
        }

        public Void Visit(InstantiationToken instantiationToken)
        {
            if (!_visit(instantiationToken))
            {
                return new Void();
            }

            AsVisitor().Visit(instantiationToken.Actuals);

            return new Void();
        }

        public Void Visit(Formal formal)
        {
            _visit(formal);

            return new Void();
        }

        public Void Visit(ClassToken classToken)
        {
            if (!_visit(classToken))
            {
                return new Void();
            }

            AsVisitor().Visit(classToken.Formals);

            AsVisitor().Visit(classToken.Actuals);

            AsVisitor().Visit(classToken.Features);

            return new Void();
        }

        public Void Visit(TypedArmToken typedArmToken)
        {
            if (!_visit(typedArmToken))
            {
                return new Void();
            }

            AsVisitor().Visit(typedArmToken.Result);

            return new Void();
        }

        public Void Visit(NullArmToken nullArmToken)
        {
            if (!_visit(nullArmToken))
            {
                return new Void();
            }

            AsVisitor().Visit(nullArmToken.Result);

            return new Void();
        }

        public Void Visit(Formals formals)
        {
            if (!_visit(formals))
            {
                return new Void();
            }

            foreach (var formal in formals.Inner)
            {
                AsVisitor().Visit(formal);
            }

            return new Void();
        }

        public Void Visit(Tokens tokens)
        {
            if (!_visit(tokens))
            {
                return new Void();
            }

            foreach (var token in tokens.Inner)
            {
                AsVisitor().Visit(token);
            }

            return new Void();
        }

        public Void Visit(Classes classes)
        {
            if (!_visit(classes))
            {
                return new Void();
            }

            foreach (var classToken in classes.Inner)
            {
                AsVisitor().Visit(classToken);
            }

            return new Void();
        }

        public Void Visit(Match match)
        {
            if (!_visit(match))
            {
                return new Void();
            }

            AsVisitor().Visit(match.Arms);

            return new Void();
        }

        public Void Visit(Arms arms)
        {
            if (!_visit(arms))
            {
                return new Void();
            }

            foreach (var arm in arms.Inner)
            {
                AsVisitor().Visit(arm);
            }

            return new Void();
        }
    }
}