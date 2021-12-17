using Models;
using Semantics.Models;

namespace Semantics
{
    public record Unit;

    internal class SemanticsVisitor : Visitor<Unit>
    {
        private readonly Unit _unit = new();

        private Contour _contour = new();

        public override Unit Visit(NativeToken nativeToken)
        {
            return _unit;
        }

        public override Unit Visit(AssignToken assignToken)
        {
            _contour = _contour.Push();
            Visit(assignToken.Body);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(WhileToken whileToken)
        {
            _contour = _contour.Push();
            Visit(whileToken.Condition);
            _contour = _contour.Pop();
            
            _contour = _contour.Push();
            Visit(whileToken.Body);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(CondToken condToken)
        {
            _contour = _contour.Push();
            Visit(condToken.Condition);
            _contour = _contour.Pop();
            
            _contour = _contour.Push();
            Visit(condToken.IfToken);
            _contour = _contour.Pop();
            
            _contour = _contour.Push();
            Visit(condToken.ElseToken);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(VarDeclToken varDeclToken)
        {
            _contour = _contour.Push();
            Visit(varDeclToken.Body);
            _contour = _contour.Pop();
            
            _contour.Update(varDeclToken.Variable, varDeclToken);
            
            return _unit;
        }

        public override Unit Visit(FunctionDeclToken functionDeclToken)
        {
            _contour = _contour.Push();
            Visit(functionDeclToken.Formals);
            Visit(functionDeclToken.Body);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(BlockToken blockToken)
        {
            _contour = _contour.Push();
            Visit(blockToken.Tokens);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(FunctionCallToken functionCallToken)
        {
            _contour = _contour.Push();
            Visit(functionCallToken.Actuals);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(NegateToken negateToken)
        {
            _contour = _contour.Push();
            Visit(negateToken.Token);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(NotToken notToken)
        {
            _contour = _contour.Push();
            Visit(notToken.Token);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(AddToken addToken)
        {
            _contour = _contour.Push();
            Visit(addToken.Left);
            _contour = _contour.Pop();
            
            _contour = _contour.Push();
            Visit(addToken.Right);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(EqualsToken equalsToken)
        {
            _contour = _contour.Push();
            Visit(equalsToken.Left);
            _contour = _contour.Pop();
            
            _contour = _contour.Push();
            Visit(equalsToken.Right);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(NotEqualsToken notEqualsToken)
        {
            _contour = _contour.Push();
            Visit(notEqualsToken.Left);
            _contour = _contour.Pop();
            
            _contour = _contour.Push();
            Visit(notEqualsToken.Right);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(LessThanToken lessThanToken)
        {
            _contour = _contour.Push();
            Visit(lessThanToken.Left);
            _contour = _contour.Pop();
            
            _contour = _contour.Push();
            Visit(lessThanToken.Right);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(LessThanEqualsToken lessThanEqualsToken)
        {
            _contour = _contour.Push();
            Visit(lessThanEqualsToken.Left);
            _contour = _contour.Pop();
            
            _contour = _contour.Push();
            Visit(lessThanEqualsToken.Right);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(SubtractToken subtractToken)
        {
            _contour = _contour.Push();
            Visit(subtractToken.Left);
            _contour = _contour.Pop();
            
            _contour = _contour.Push();
            Visit(subtractToken.Right);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(DivideToken divideToken)
        {
            _contour = _contour.Push();
            Visit(divideToken.Left);
            _contour = _contour.Pop();
            
            _contour = _contour.Push();
            Visit(divideToken.Right);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(MultiplyToken multiplyToken)
        {
            _contour = _contour.Push();
            Visit(multiplyToken.Left);
            _contour = _contour.Pop();
            
            _contour = _contour.Push();
            Visit(multiplyToken.Right);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(AtomicToken atomicToken)
        {
            return _unit;
        }

        public override Unit Visit(VariableToken variableToken)
        {
            return _unit;
        }

        public override Unit Visit(AccessToken accessToken)
        {
            _contour = _contour.Push();
            Visit(accessToken.Receiver);
            _contour = _contour.Pop();
            
            _contour = _contour.Push();
            Visit(accessToken.Variable);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(InstantiationToken instantiationToken)
        {
            _contour = _contour.Push();
            Visit(instantiationToken.Actuals);
            _contour = _contour.Pop();
            
            return _unit;
        }

        public override Unit Visit(Formal formal)
        {
            _contour.Update(formal.Name, formal);
            
            return _unit;
        }

        public override Unit Visit(ClassToken classToken)
        {
            return _unit;
        }

        public override Unit Visit(TypedArmToken typedArmToken)
        {
            _contour.Update(typedArmToken.Name, typedArmToken);
            
            return _unit;
        }

        public override Unit Visit(NullArmToken nullArmToken)
        {
            return _unit;
        }

        public override Unit Visit(Formals formals)
        {
            foreach (var token in formals.Inner)
            {
                Visit(token);
            }
            
            return _unit;
        }

        public override Unit Visit(Tokens tokens)
        {
            foreach (var token in tokens.Inner)
            {
                Visit(token);
            }

            return _unit;
        }

        public override Unit Visit(Classes classes)
        {
            foreach (var classToken in classes.Inner)
            {
                Visit(classToken);
            }

            return _unit;
        }

        public override Unit Visit(Match match)
        {
            _contour = _contour.Push();
            Visit(match.Token);
            _contour = _contour.Pop();
            
            _contour = _contour.Push();
            Visit(match.Token);
            _contour = _contour.Pop();
            
            return _unit;
        }
    }
}