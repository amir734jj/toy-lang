using System;

namespace Models
{
    public class Void { }
    
    public class SimpleVisitor : Visitor<Void>
    {
        private readonly Func<IToken, bool> _visit;

        public SimpleVisitor(Func<IToken, bool> visit)
        {
            _visit = visit;
        }

        public override Void Visit(AndToken andToken)
        {
            if (!_visit(andToken))
            {
                return new Void();
            }

            Visit(andToken.Left);

            Visit(andToken.Right);

            return new Void();
        }

        public override Void Visit(OrToken orToken)
        {
            if (!_visit(orToken))
            {
                return new Void();
            }

            Visit(orToken.Left);

            Visit(orToken.Right);

            return new Void();
        }

        public override Void Visit(NativeToken nativeToken)
        {
            _visit(nativeToken);

            return new Void();
        }

        public override Void Visit(AssignToken assignToken)
        {
            if (!_visit(assignToken))
            {
                return new Void();
            }

            Visit(assignToken.Body);

            return new Void();
        }

        public override Void Visit(WhileToken whileToken)
        {
            if (!_visit(whileToken))
            {
                return new Void();
            }

            Visit(whileToken.Condition);

            Visit(whileToken.Body);

            return new Void();
        }

        public override Void Visit(CondToken condToken)
        {
            if (!_visit(condToken))
            {
                return new Void();
            }

            Visit(condToken.Condition);

            Visit(condToken.IfToken);

            Visit(condToken.ElseToken);

            return new Void();
        }

        public override Void Visit(VarDeclToken varDeclToken)
        {
            if (!_visit(varDeclToken))
            {
                return new Void();
            }

            Visit(varDeclToken.Body);
            
            return new Void();
        }

        public override Void Visit(FunctionDeclToken functionDeclToken)
        {
            if (!_visit(functionDeclToken))
            {
                return new Void();
            }

            Visit(functionDeclToken.Formals);
            
            Visit(functionDeclToken.Body);
            
            return new Void();
        }

        public override Void Visit(BlockToken blockToken)
        {
            if (!_visit(blockToken))
            {
                return new Void();
            }

            Visit(blockToken.Tokens);

            return new Void();
        }

        public override Void Visit(FunctionCallToken functionCallToken)
        {
            if (!_visit(functionCallToken))
            {
                return new Void();
            }

            Visit(functionCallToken.Actuals);

            return new Void();
        }

        public override Void Visit(NegateToken negateToken)
        {
            if (!_visit(negateToken))
            {
                return new Void();
            }

            Visit(negateToken.Token);

            return new Void();
        }

        public override Void Visit(NotToken notToken)
        {
            if (!_visit(notToken))
            {
                return new Void();
            }

            Visit(notToken.Token);

            return new Void();
        }

        public override Void Visit(AddToken addToken)
        {
            if (!_visit(addToken))
            {
                return new Void();
            }

            Visit(addToken.Left);

            Visit(addToken.Right);

            return new Void();
        }

        public override Void Visit(EqualsToken equalsToken)
        {
            if (!_visit(equalsToken))
            {
                return new Void();
            }

            Visit(equalsToken.Left);

            Visit(equalsToken.Right);

            return new Void();
        }

        public override Void Visit(NotEqualsToken notEqualsToken)
        {
            if (!_visit(notEqualsToken))
            {
                return new Void();
            }

            Visit(notEqualsToken.Left);

            Visit(notEqualsToken.Right);

            return new Void();
        }

        public override Void Visit(LessThanToken lessThanToken)
        {
            if (!_visit(lessThanToken))
            {
                return new Void();
            }

            Visit(lessThanToken.Left);

            Visit(lessThanToken.Right);

            return new Void();
        }

        public override Void Visit(LessThanEqualsToken lessThanEqualsToken)
        {
            if (!_visit(lessThanEqualsToken))
            {
                return new Void();
            }

            Visit(lessThanEqualsToken.Left);

            Visit(lessThanEqualsToken.Right);

            return new Void();
        }

        public override Void Visit(SubtractToken subtractToken)
        {
            if (!_visit(subtractToken))
            {
                return new Void();
            }

            Visit(subtractToken.Left);

            Visit(subtractToken.Right);

            return new Void();
        }

        public override Void Visit(DivideToken divideToken)
        {
            if (!_visit(divideToken))
            {
                return new Void();
            }

            Visit(divideToken.Left);

            Visit(divideToken.Right);

            return new Void();
        }

        public override Void Visit(MultiplyToken multiplyToken)
        {
            if (!_visit(multiplyToken))
            {
                return new Void();
            }

            Visit(multiplyToken.Left);

            Visit(multiplyToken.Right);

            return new Void();
        }

        public override Void Visit(AtomicToken atomicToken)
        {
            _visit(atomicToken);

            return new Void();
        }

        public override Void Visit(VariableToken variableToken)
        {
            _visit(variableToken);

            return new Void();
        }

        public override Void Visit(AccessToken accessToken)
        {
            if (!_visit(accessToken))
            {
                return new Void();
            }

            Visit(accessToken.Receiver);

            return new Void();
        }

        public override Void Visit(InstantiationToken instantiationToken)
        {
            if (!_visit(instantiationToken))
            {
                return new Void();
            }

            Visit(instantiationToken.Actuals);

            return new Void();
        }

        public override Void Visit(Formal formal)
        {
            _visit(formal);

            return new Void();
        }

        public override Void Visit(ClassToken classToken)
        {
            if (!_visit(classToken))
            {
                return new Void();
            }

            Visit(classToken.Formals);

            Visit(classToken.Actuals);
            
            Visit(classToken.Features);

            return new Void();
        }

        public override Void Visit(TypedArmToken typedArmToken)
        {
            if (!_visit(typedArmToken))
            {
                return new Void();
            }

            Visit(typedArmToken.Result);

            return new Void();
        }

        public override Void Visit(NullArmToken nullArmToken)
        {
            if (!_visit(nullArmToken))
            {
                return new Void();
            }

            Visit(nullArmToken.Result);

            return new Void();
        }

        public override Void Visit(Formals formals)
        {
            if (!_visit(formals))
            {
                return new Void();
            }

            foreach (var formal in formals.Inner)
            {
                Visit(formal);
            }

            return new Void();
        }

        public override Void Visit(Tokens tokens)
        {
            if (!_visit(tokens))
            {
                return new Void();
            }

            foreach (var token in tokens.Inner)
            {
                Visit(token);
            }

            return new Void();
        }

        public override Void Visit(Classes classes)
        {
            if (!_visit(classes))
            {
                return new Void();
            }

            foreach (var classToken in classes.Inner)
            {
                Visit(classToken);
            }

            return new Void();
        }

        public override Void Visit(Match match)
        {
            if (!_visit(match))
            {
                return new Void();
            }

            Visit(match.Arms);

            return new Void();
        }

        public override Void Visit(Arms arms)
        {
            if (!_visit(arms))
            {
                return new Void();
            }

            foreach (var arm in arms.Inner)
            {
                Visit(arm);
            }

            return new Void();
        }
    }
}