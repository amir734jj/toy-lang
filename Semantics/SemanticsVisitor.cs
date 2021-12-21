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

        private Contour<string, Token> _variableContour = new("Variable contour");

        private Contour<Token, string> _typeContour = new("Type contour");

        private readonly Dictionary<string, string> _hierarchy = new();

        private bool _isInsideOfClass = false;

        private readonly string[] _nativeTypes = { "String", "Int", "Boolean", "Unit", "Any", "ArrayAny", "Symbol" };

        // ReSharper disable once MemberCanBePrivate.Global
        public readonly List<(Token, string)> Errors = new();

        public override Unit Visit(NativeToken nativeToken)
        {
            return _unit;
        }

        public override Unit Visit(AssignToken assignToken)
        {
            // Enter contours
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();
            
            Visit(assignToken.Body);
            
            if (!_variableContour.Lookup(assignToken.Variable, out var variableToken))
            {
                Errors[assignToken] = "Cannot assign to a variable that has not be defined yet.";
            }

            if (!_typeContour.Lookup(assignToken.Body, out var exprType))
            {
                Errors[assignToken] = "Type of expression is not defined.";
            }

            if (TypeLub(_typeContour[variableToken], _typeContour[assignToken.Body]) != _typeContour[variableToken])
            {
                Errors[assignToken] = "Type of LHS should be a superset equals of right hand side";
            }
            
            // Exit contours
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            return _unit;
        }

        public override Unit Visit(WhileToken whileToken)
        {
            _variableContour = _variableContour.Push();
            Visit(whileToken.Condition);
            _variableContour = _variableContour.Pop();

            if (_typeContour[whileToken.Condition] != "Boolean")
            {
                Errors.Add("While loop condition should have a type of Boolean.");
            }
            
            _variableContour = _variableContour.Push();
            Visit(whileToken.Body);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            return _unit;
        }

        public override Unit Visit(CondToken condToken)
        {
            _variableContour = _variableContour.Push();
            Visit(condToken.Condition);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            _variableContour = _variableContour.Push();
            Visit(condToken.IfToken);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            _variableContour = _variableContour.Push();
            Visit(condToken.ElseToken);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            if (_typeContour[condToken.Condition] != "Boolean")
            {
                Errors.Add("If condition should have a type of Boolean.");
            }

            return _unit;
        }

        public override Unit Visit(VarDeclToken varDeclToken)
        {
            _variableContour = _variableContour.Push();
            Visit(varDeclToken.Body);

            _typeContour[varDeclToken] = varDeclToken.Type;
            if (TypeLub(_typeContour[varDeclToken], _typeContour[varDeclToken.Body]) != _typeContour[varDeclToken])
            {
                Errors.Add("Type of LHS should be a superset equals of right hand side");
            }
            
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            _variableContour.Update(varDeclToken.Variable, varDeclToken);
            
            return _unit;
        }

        public override Unit Visit(FunctionDeclToken functionDeclToken)
        {
            _variableContour.Update(functionDeclToken.Name, functionDeclToken);
            
            _variableContour = _variableContour.Push();
            Visit(functionDeclToken.Formals);
            Visit(functionDeclToken.Body);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            return _unit;
        }

        public override Unit Visit(BlockToken blockToken)
        {
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();
            
            Visit(blockToken.Tokens);

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            return _unit;
        }

        public override Unit Visit(FunctionCallToken functionCallToken)
        {
            _variableContour = _variableContour.Push();
            Visit(functionCallToken.Actuals);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            return _unit;
        }

        public override Unit Visit(NegateToken negateToken)
        {
            _variableContour = _variableContour.Push();
            Visit(negateToken.Token);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            return _unit;
        }

        public override Unit Visit(NotToken notToken)
        {
            _variableContour = _variableContour.Push();
            Visit(notToken.Token);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            return _unit;
        }

        public override Unit Visit(AddToken addToken)
        {
            _variableContour = _variableContour.Push();
            Visit(addToken.Left);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            _variableContour = _variableContour.Push();
            Visit(addToken.Right);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            return _unit;
        }

        public override Unit Visit(EqualsToken equalsToken)
        {
            _variableContour = _variableContour.Push();
            Visit(equalsToken.Left);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            _variableContour = _variableContour.Push();
            Visit(equalsToken.Right);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            if (_typeContour[equalsToken.Left] != "Boolean")
            {
                Errors.Add("LHS of equal should have a type of Boolean.");
            }
            
            if (_typeContour[equalsToken.Right] != "Boolean")
            {
                Errors.Add("RHS of equal should have a type of Boolean.");
            }
            
            return _unit;
        }

        public override Unit Visit(NotEqualsToken notEqualsToken)
        {
            _variableContour = _variableContour.Push();
            Visit(notEqualsToken.Left);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            _variableContour = _variableContour.Push();
            Visit(notEqualsToken.Right);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            if (_typeContour.Lookup(notEqualsToken.Left) != "Int")
            {
                Errors.Add("LHS of not equal should have a type of Boolean.");
            }
            
            if (_typeContour[notEqualsToken.Right] != "Int")
            {
                Errors.Add("RHS of not equal should have a type of Boolean.");
            }
            
            return _unit;
        }

        public override Unit Visit(LessThanToken lessThanToken)
        {
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();
            
            Visit(lessThanToken.Left);
            
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();
            
            Visit(lessThanToken.Right);
            
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            if (_typeContour[lessThanToken.Left] != "Int")
            {
                Errors.Add("LHS of less than should have a type of Int.");
            }
            
            if (_typeContour[lessThanToken.Right] != "Int")
            {
                Errors.Add("RHS of less than should have a type of Int.");
            }
            
            return _unit;
        }

        public override Unit Visit(LessThanEqualsToken lessThanEqualsToken)
        {
            _variableContour = _variableContour.Push();
            Visit(lessThanEqualsToken.Left);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            _variableContour = _variableContour.Push();
            Visit(lessThanEqualsToken.Right);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            if (_typeContour[lessThanEqualsToken.Left] != "Int")
            {
                Errors.Add("LHS of less than or equal should have a type of Int.");
            }
            
            if (_typeContour[lessThanEqualsToken.Right] != "Int")
            {
                Errors.Add("RHS of less than or equal should have a type of Int.");
            }
            
            return _unit;
        }

        public override Unit Visit(SubtractToken subtractToken)
        {
            _variableContour = _variableContour.Push();
            Visit(subtractToken.Left);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            _variableContour = _variableContour.Push();
            Visit(subtractToken.Right);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            if (_typeContour[subtractToken.Left] != "Int")
            {
                Errors.Add("LHS of subtract should have a type of Int.");
            }
            
            if (_typeContour[subtractToken.Right] != "Int")
            {
                Errors.Add("RHS of subtract should have a type of Int.");
            }
            
            return _unit;
        }

        public override Unit Visit(DivideToken divideToken)
        {
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();
            
            Visit(divideToken.Left);
            
            if (_typeContour.Lookup(divideToken.Left, out var lhsType))
            {
                Errors[divideToken] = "Type of LHS is missing.";
            }

            if (lhsType != "Int")
            {
                Errors[Token] = ("LHS of divide should have a type of Int.");
            }
            
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();
            
            Visit(divideToken.Right);
            
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            if (_typeContour.Lookup(divideToken.Left, out var lhsType))
            {
                Errors[divideToken] = "Type of LHS is missing.";
            }
            
            if (_typeContour.Lookup(divideToken.Right, out var rhsType))
            {
                Errors[divideToken] = "Type of RHS is missing.";
            }
            
            if (lhsType != "Int")
            {
                Errors.Add("LHS of divide should have a type of Int.");
            }
            
            if (rhsType != "Int")
            {
                Errors.Add("RHS of divide should have a type of Int.");
            }
            
            return _unit;
        }

        public override Unit Visit(MultiplyToken multiplyToken)
        {
            _variableContour = _variableContour.Push();
            Visit(multiplyToken.Left);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            _variableContour = _variableContour.Push();
            Visit(multiplyToken.Right);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            if (_typeContour[multiplyToken.Left] != "Int")
            {
                Errors.Add("LHS of multiply should have a type of Int.");
            }
            
            if (_typeContour[multiplyToken.Right] != "Int")
            {
                Errors.Add("RHS of multiply should have a type of Int.");
            }
            
            return _unit;
        }

        public override Unit Visit(AtomicToken atomicToken)
        {
            _typeContour[atomicToken] = atomicToken.Value switch
            {
                string _ => "String",
                int _ => "Int",
                bool _ => "Boolean",
                null => ROOT_TYPE,
                _ => _typeContour[atomicToken]
            };

            return _unit;
        }

        public override Unit Visit(VariableToken variableToken)
        {
            if (_isInsideOfClass && variableToken.Variable != "this" && !_variableContour.Lookup(variableToken.Variable, out _))
            {
                Errors[variableToken] = "Variable does not exist.";
            }
            
            return _unit;
        }

        public override Unit Visit(AccessToken accessToken)
        {
            // Enter LHS
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();
            
            Visit(accessToken.Receiver);
            
            // Exist RHS
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            // Enter RHS
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();
            
            Visit(accessToken.Variable);

            // Exist LHS
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            return _unit;
        }

        public override Unit Visit(InstantiationToken instantiationToken)
        {
            if (!_variableContour.Lookup(instantiationToken.Class, out _))
            {
                Errors[instantiationToken] = "Instantiation of class which does not exist.";
            }
            
            _variableContour = _variableContour.Push();
            Visit(instantiationToken.Actuals);
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            return _unit;
        }

        public override Unit Visit(Formal formal)
        {
            _variableContour.Update(formal.Name, formal);
            
            return _unit;
        }

        public override Unit Visit(ClassToken classToken)
        {
            if (classToken.Name == classToken.Inherits)
            {
                Errors.Add((classToken, "Class extends itself."));
            }

            if (_nativeTypes.Contains(classToken.Inherits))
            {
                Errors.Add((classToken, "Class cannot extend a native type."));
            }
            
            if (classToken.Inherits != "native" && classToken.Inherits != ROOT_TYPE && !_variableContour.Lookup(classToken.Inherits, out _))
            {
                Errors.Add((classToken, "Extended class does not exist."));
            }
            
            _variableContour.Update(classToken.Name, classToken);

            _isInsideOfClass = true;
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(classToken.Formals);
            Visit(classToken.Features);

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            _isInsideOfClass = false;
            
            return _unit;
        }

        public override Unit Visit(TypedArmToken typedArmToken)
        {
            _variableContour.Update(typedArmToken.Name, typedArmToken);
            
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
            // Collect class hierarchy
            foreach (var classToken in classes.Inner)
            {
                _hierarchy.Add(classToken.Name, classToken.Inherits);
            }
            
            // Visit classes
            foreach (var classToken in classes.Inner)
            {
                Visit(classToken);
            }

            return _unit;
        }

        public override Unit Visit(Match match)
        {
            // Enter expression contour
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();
            
            Visit(match.Token);

            if (!_typeContour.Lookup(match.Token, out var exprType))
            {
                Errors.Add((match, "Expression type is not defined"));
            }
            
            // Exist expression contour
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            // Enter arms contour
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();
            
            Visit(match.Arms);
            
            var armsType = match.Arms.Inner.Select(arm =>
            {
                if (!_typeContour.Lookup(arm, out var armType))
                {
                    Errors.Add((arm, "Type of arm did not exist."));
                }

                return armType;
            }).Aggregate(TypeLub);

            // Exist arms contour
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            // Set match type
            _typeContour.Update(match, armsType);
            
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