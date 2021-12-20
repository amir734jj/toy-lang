using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Semantics.Models;
using static Models.Constants;

namespace Semantics
{
    public record Unit;

    internal class SemanticsVisitor : Visitor<Unit>
    {
        private readonly Unit _unit = new();

        private Contour _contour = new();

        private readonly Dictionary<Token, string> _types = new();

        private readonly Dictionary<string, string> _hierarchy = new();

        private readonly string[] _nativeTypes = { "String", "Int", "Boolean", "Unit", "Any", "ArrayAny", "Symbol" };

        // ReSharper disable once MemberCanBePrivate.Global
        public readonly List<string> Errors = new();

        public override Unit Visit(NativeToken nativeToken)
        {
            return _unit;
        }

        public override Unit Visit(AssignToken assignToken)
        {
            _contour = _contour.Push();
            Visit(assignToken.Body);
            
            if (!_contour.Lookup(assignToken.Variable, out var variableToken))
            {
                Errors.Add($"Cannot assign to a variable {assignToken.Variable} that has not be defined yet.");
            }
            
            if (TypeLub(_types[variableToken], _types[assignToken.Body]) != _types[variableToken])
            {
                Errors.Add("Type of LHS should be a superset equals of right hand side");
            }
            
            _contour = _contour.Pop();

            return _unit;
        }

        public override Unit Visit(WhileToken whileToken)
        {
            _contour = _contour.Push();
            Visit(whileToken.Condition);
            _contour = _contour.Pop();

            if (_types[whileToken.Condition] != "Boolean")
            {
                Errors.Add("While loop condition should have a type of Boolean.");
            }
            
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
            
            if (_types[condToken.Condition] != "Boolean")
            {
                Errors.Add("If condition should have a type of Boolean.");
            }

            return _unit;
        }

        public override Unit Visit(VarDeclToken varDeclToken)
        {
            _contour = _contour.Push();
            Visit(varDeclToken.Body);

            _types[varDeclToken] = varDeclToken.Type;
            if (TypeLub(_types[varDeclToken], _types[varDeclToken.Body]) != _types[varDeclToken])
            {
                Errors.Add("Type of LHS should be a superset equals of right hand side");
            }
            
            _contour = _contour.Pop();
            
            _contour.Update(varDeclToken.Variable, varDeclToken);
            
            return _unit;
        }

        public override Unit Visit(FunctionDeclToken functionDeclToken)
        {
            _contour.Update(functionDeclToken.Name, functionDeclToken);
            
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
            
            if (_types[equalsToken.Left] != "Boolean")
            {
                Errors.Add("LHS of equal should have a type of Boolean.");
            }
            
            if (_types[equalsToken.Right] != "Boolean")
            {
                Errors.Add("RHS of equal should have a type of Boolean.");
            }
            
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
            
            if (_types[notEqualsToken.Left] != "Int")
            {
                Errors.Add("LHS of not equal should have a type of Boolean.");
            }
            
            if (_types[notEqualsToken.Right] != "Int")
            {
                Errors.Add("RHS of not equal should have a type of Boolean.");
            }
            
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
            
            if (_types[lessThanToken.Left] != "Int")
            {
                Errors.Add("LHS of less than should have a type of Int.");
            }
            
            if (_types[lessThanToken.Right] != "Int")
            {
                Errors.Add("RHS of less than should have a type of Int.");
            }
            
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
            
            if (_types[lessThanEqualsToken.Left] != "Int")
            {
                Errors.Add("LHS of less than or equal should have a type of Int.");
            }
            
            if (_types[lessThanEqualsToken.Right] != "Int")
            {
                Errors.Add("RHS of less than or equal should have a type of Int.");
            }
            
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
            
            if (_types[subtractToken.Left] != "Int")
            {
                Errors.Add("LHS of subtract should have a type of Int.");
            }
            
            if (_types[subtractToken.Right] != "Int")
            {
                Errors.Add("RHS of subtract should have a type of Int.");
            }
            
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
            
            if (_types[divideToken.Left] != "Int")
            {
                Errors.Add("LHS of divide should have a type of Int.");
            }
            
            if (_types[divideToken.Right] != "Int")
            {
                Errors.Add("RHS of divide should have a type of Int.");
            }
            
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

            if (_types[multiplyToken.Left] != "Int")
            {
                Errors.Add("LHS of multiply should have a type of Int.");
            }
            
            if (_types[multiplyToken.Right] != "Int")
            {
                Errors.Add("RHS of multiply should have a type of Int.");
            }
            
            return _unit;
        }

        public override Unit Visit(AtomicToken atomicToken)
        {
            _types[atomicToken] = atomicToken.Value switch
            {
                string _ => "String",
                int _ => "Int",
                bool _ => "Boolean",
                null => ROOT_TYPE,
                _ => _types[atomicToken]
            };

            return _unit;
        }

        public override Unit Visit(VariableToken variableToken)
        {
            if (variableToken.Variable != "this" && !_contour.Lookup(variableToken.Variable, out _))
            {
                Errors.Add($"Variable {variableToken.Variable} does not exist.");
            }
            
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
            if (!_contour.Lookup(instantiationToken.Class, out _))
            {
                Errors.Add($"Instantiation of {instantiationToken.Class} which does not exist.");
            }
            
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
            if (classToken.Name == classToken.Inherits)
            {
                Errors.Add($"Class {classToken.Name} extends itself.");
            }

            if (_nativeTypes.Contains(classToken.Inherits))
            {
                Errors.Add($"Class {classToken.Name} cannot extend a native type.");
            }
            
            if (classToken.Inherits != "native" && classToken.Inherits != ROOT_TYPE && !_contour.Lookup(classToken.Inherits, out _))
            {
                Errors.Add($"Extended class {classToken.Inherits} does not exist.");
            }
            
            _contour.Update(classToken.Name, classToken);

            _contour.Push();

            Visit(classToken.Formals);

            Visit(classToken.Features);

            _contour.Pop();

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
                _hierarchy.Add(classToken.Name, classToken.Inherits);
            }
            
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
            Visit(match.Inner);
            _contour = _contour.Pop();
            
            return _unit;
        }

        private List<string> TypePathToRoot(string t)
        {
            return _hierarchy.TryGetValue(t, out var parent)
                ? new List<string> { t }.Concat(TypePathToRoot(parent)).ToList()
                : new List<string> { ROOT_TYPE };
        }

        private string TypeLub(string t1, string t2)
        {
            if (t1 == t2)
            {
                return t2;
            }

            if (t1 == ROOT_TYPE || t2 == ROOT_TYPE)
            {
                return ROOT_TYPE;
            }

            var p1 = TypePathToRoot(t1);
            var p2 = TypePathToRoot(t2);

            return p1.First(x => p2.Contains(x));
        }
    }
}