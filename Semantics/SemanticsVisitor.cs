using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Semantics.Models;
using static Models.Constants;

namespace Semantics
{
    internal class SemanticsVisitor : Visitor<Unit>
    {
        private Contour<string, IToken> _variableContour = new();

        private Contour<IToken, string> _typeContour = new();

        private readonly Dictionary<string, string> _hierarchy = new();

        private ClassToken _outerClassToken;

        private readonly string[] _primitiveTypes =
            { STRING_TYPE, INTEGER_TYPE, BOOLEAN_TYPE, UNIT_TYPE, NOTHING_TYPE, ARRAY_ANY_TYPE, SYMBOL_TYPE };

        private readonly Dictionary<string, Dictionary<string, FunctionDeclToken>> _methods = new();

        private readonly Stack<Dictionary<string, FunctionDeclToken>> _availableMethods = new();

        // ReSharper disable once MemberCanBePrivate.Global
        public readonly SemanticErrors<Unit> Semantics = new(Unit.Instance);

        public override Unit Visit(NativeToken nativeToken)
        {
            // Native has a root type
            _typeContour.Update(nativeToken, ANY_TYPE);

            return Unit.Instance;
        }

        public override Unit Visit(AssignToken assignToken)
        {
            // Enter expression body contours
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(assignToken.Body);

            // Make sure variable being assigned actually exist
            if (!_variableContour.Lookup(assignToken.Variable, out var variableToken))
            {
                return Semantics.Error(assignToken, "Cannot assign to a variable that has not be defined yet.");
            }

            // Resolve the type of expression body
            if (!_typeContour.Lookup(assignToken.Body, out var exprType))
            {
                return Semantics.Error(assignToken, "Type of assignment body is not defined.");
            }

            // Resolve static type of variable being assigned
            if (!_typeContour.Lookup(variableToken, out var variableType))
            {
                return Semantics.Error(assignToken, "Type of variable is not defined.");
            }

            // Check if static type is superset of dynamic type
            if (TypeLub(exprType, variableType) != variableType)
            {
                return Semantics.Error(assignToken,
                    "Type of LHS of assignment should be a superset equals of right hand side");
            }

            // Exit expression body contours
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Type of assignment is Unit
            _typeContour.Update(assignToken, UNIT_TYPE);

            return Unit.Instance;
        }

        public override Unit Visit(WhileToken whileToken)
        {
            // Enter loop condition contour
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(whileToken.Condition);

            // Make sure type of condition exist
            if (!_typeContour.Lookup(whileToken.Condition, out var condType))
            {
                return Semantics.Error(whileToken, "Type of conditional expression is not defined.");
            }

            // Make sure type of condition is Boolean
            if (condType != BOOLEAN_TYPE)
            {
                return Semantics.Error(whileToken, "While loop condition should have a type of Boolean.");
            }

            // Exit loop condition contour
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Enter loop body contour
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(whileToken.Body);

            // Exit loop body contour
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Type of while loop is Unit
            _typeContour.Update(whileToken, UNIT_TYPE);

            return Unit.Instance;
        }

        public override Unit Visit(CondToken condToken)
        {
            // Enter if condition contour
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(condToken.Condition);

            // Make sure type of condition expression is defined
            if (!_typeContour.Lookup(condToken.Condition, out var condType))
            {
                return Semantics.Error(condToken, "Type of conditional expression is not defined.");
            }

            // Make sure type of condition expression is boolean
            if (condType != BOOLEAN_TYPE)
            {
                return Semantics.Error(condToken, "If condition should have a type of Boolean.");
            }

            // Exit if condition contour
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Enter if if expression contour
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(condToken.IfToken);

            // Make sure of if expression is defined
            if (!_typeContour.Lookup(condToken.IfToken, out var ifType))
            {
                return Semantics.Error(condToken, "Type of conditional if expression is not defined.");
            }

            // Exit if if expression contour
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Enter if else expression contour
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(condToken.ElseToken);

            // Make sure type of else expression is defined
            if (!_typeContour.Lookup(condToken.ElseToken, out var elseType))
            {
                return Semantics.Error(condToken, "Type of conditional else expression is not defined.");
            }

            // Exit if else expression contour
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Type of If is LUB of if and else expressions
            _typeContour.Update(condToken, TypeLub(ifType, elseType));

            return Unit.Instance;
        }

        public override Unit Visit(VarDeclToken varDeclToken)
        {
            // Enter var decl expression contour
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(varDeclToken.Body);

            // Make sure type of type of variable declaration body expression is defined
            if (!_typeContour.Lookup(varDeclToken.Body, out var exprType))
            {
                return Semantics.Error(varDeclToken, "Type of var decl body is not defined.");
            }

            // Make sure the type of variable declaration body is superset of static type
            if (TypeLub(varDeclToken.Type, exprType) != varDeclToken.Type)
            {
                return Semantics.Error(varDeclToken,
                    "Type of LHS of var decl should be a superset equals of right hand side");
            }

            // Exit var decl expression contour
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Export variable name
            _variableContour.Update(varDeclToken.Variable, varDeclToken);

            // Export variable type
            _typeContour.Update(varDeclToken, varDeclToken.Type);

            return Unit.Instance;
        }

        public override Unit Visit(FunctionDeclToken functionDeclToken)
        {
            // Enter function contour
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(functionDeclToken.Formals);
            Visit(functionDeclToken.Body);

            if (functionDeclToken.Type != NOTHING_TYPE && !_hierarchy.ContainsKey(functionDeclToken.Type))
            {
                return Semantics.Error(functionDeclToken, "Return type of function should be defined.");
            }

            // Exist function contour
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            return Unit.Instance;
        }

        public override Unit Visit(BlockToken blockToken)
        {
            // Enter block contour
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(blockToken.Tokens);

            string blockType;
            if (blockToken.Tokens.Inner.Count == 0)
            {
                blockType = UNIT_TYPE;
            }
            else
            {
                var tokenType = ANY_TYPE;
                foreach (var token in blockToken.Tokens.Inner)
                {
                    if (!_typeContour.Lookup(token, out tokenType))
                    {
                        return Semantics.Error(token, "Token type is not defined while finding block type.");
                    }
                }

                blockType = tokenType;
            }

            // Exist block contour
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Export type of block
            _typeContour.Update(blockToken, blockType);

            return Unit.Instance;
        }

        public override Unit Visit(FunctionCallToken functionCallToken)
        {
            if (_outerClassToken == null)
            {
                return Semantics.Error(functionCallToken, "Cannot have method outside of class.");
            }

            // Make sure function decl is actually defined
            if (!_availableMethods.Peek().TryGetValue(functionCallToken.Name, out var functionDeclToken))
            {
                return Semantics.Error(functionCallToken, "Function is not defined and cannot be invoked.");
            }

            // Make sure count of formals and actuals are equal
            if (functionCallToken.Actuals.Inner.Count != functionDeclToken.Formals.Inner.Count)
            {
                return Semantics.Error(functionCallToken, "Count of formals and actuals do not match");
            }

            for (var index = 0; index < functionCallToken.Actuals.Inner.Count; index++)
            {
                var formal = functionDeclToken.Formals.Inner[index];
                var actual = functionCallToken.Actuals.Inner[index];
                
                // Enter function call actuals contour
                _variableContour = _variableContour.Push();
                _typeContour = _typeContour.Push();

                Visit(actual);
                
                // Make sure type of actual exist
                if (!_typeContour.Lookup(actual, out var actualType))
                {
                    return Semantics.Error(functionCallToken, "Type of actual is not defined in function call.");
                }

                // Make sure type of actual is superset of formal
                if (TypeLub(actualType, formal.Type) != formal.Type)
                {
                    return Semantics.Error(functionCallToken, "Type of actual should be superset of formal in function call.");
                }
                
                // Exist function call actuals contour
                _variableContour = _variableContour.Pop();
                _typeContour = _typeContour.Pop();
            }

            // Type of function call is the static type of function decl
            _typeContour.Update(functionCallToken, functionDeclToken.Type);

            return Unit.Instance;
        }

        public override Unit Visit(NegateToken negateToken)
        {
            // Enter negate expression contour
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(negateToken.Token);

            if (!_typeContour.Lookup(negateToken.Token, out var exprType))
            {
                return Semantics.Error(negateToken, "Cannot find the type of expression being negated.");
            }

            if (exprType != INTEGER_TYPE)
            {
                return Semantics.Error(negateToken, "Expression being negated should have a type of integer.");
            }

            // Exist negate expression contour
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Export type of negate expression as integer
            _typeContour.Update(negateToken, INTEGER_TYPE);

            return Unit.Instance;
        }

        public override Unit Visit(NotToken notToken)
        {
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(notToken.Token);

            if (!_typeContour.Lookup(notToken.Token, out var exprType))
            {
                return Semantics.Error(notToken, "Cannot find the type of expression being noted.");
            }

            if (exprType != BOOLEAN_TYPE)
            {
                return Semantics.Error(notToken, "Expression being noted should have a type of boolean.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Export type of not expression as boolean
            _typeContour.Update(notToken, BOOLEAN_TYPE);

            return Unit.Instance;
        }

        public override Unit Visit(AddToken addToken)
        {
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(addToken.Left);

            // Make sure type of LHS is defined
            if (!_typeContour.Lookup(addToken.Left, out var lhsType))
            {
                return Semantics.Error(addToken, "Type of LHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (lhsType != INTEGER_TYPE)
            {
                return Semantics.Error(addToken, "Type of LHS should be integer.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(addToken.Right);

            // Make sure type of RHS is defined
            if (!_typeContour.Lookup(addToken.Right, out var rhsType))
            {
                return Semantics.Error(addToken, "Type of RHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (rhsType != INTEGER_TYPE)
            {
                return Semantics.Error(addToken, "Type of RHS should be integer.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Export type as integer
            _typeContour.Update(addToken, INTEGER_TYPE);

            return Unit.Instance;
        }

        public override Unit Visit(EqualsToken equalsToken)
        {
            // Enter LHS contours
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(equalsToken.Left);

            // Exist LHS contours
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Enter RHS contours
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(equalsToken.Right);

            // Exist RHS contours
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            _typeContour.Update(equalsToken, BOOLEAN_TYPE);

            return Unit.Instance;
        }

        public override Unit Visit(NotEqualsToken notEqualsToken)
        {
            // Enter LHS contours
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(notEqualsToken.Left);

            // Exist LHS contours
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Enter RHS contours
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(notEqualsToken.Right);

            // Exist RHS contours
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            _typeContour.Update(notEqualsToken, BOOLEAN_TYPE);

            return Unit.Instance;
        }

        public override Unit Visit(LessThanToken lessThanToken)
        {
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(lessThanToken.Left);

            // Make sure type of LHS is defined
            if (!_typeContour.Lookup(lessThanToken.Left, out var lhsType))
            {
                return Semantics.Error(lessThanToken, "Type of LHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (lhsType != INTEGER_TYPE)
            {
                return Semantics.Error(lessThanToken, "Type of LHS should be integer.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(lessThanToken.Right);

            // Make sure type of RHS is defined
            if (!_typeContour.Lookup(lessThanToken.Right, out var rhsType))
            {
                return Semantics.Error(lessThanToken, "Type of RHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (rhsType != INTEGER_TYPE)
            {
                return Semantics.Error(lessThanToken, "Type of RHS should be integer.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Export type as integer
            _typeContour.Update(lessThanToken, BOOLEAN_TYPE);

            return Unit.Instance;
        }

        public override Unit Visit(LessThanEqualsToken lessThanEqualsToken)
        {
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(lessThanEqualsToken.Left);

            // Make sure type of LHS is defined
            if (!_typeContour.Lookup(lessThanEqualsToken.Left, out var lhsType))
            {
                return Semantics.Error(lessThanEqualsToken, "Type of LHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (lhsType != INTEGER_TYPE)
            {
                return Semantics.Error(lessThanEqualsToken, "Type of LHS should be integer.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(lessThanEqualsToken.Right);

            // Make sure type of RHS is defined
            if (!_typeContour.Lookup(lessThanEqualsToken.Right, out var rhsType))
            {
                return Semantics.Error(lessThanEqualsToken, "Type of RHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (rhsType != INTEGER_TYPE)
            {
                return Semantics.Error(lessThanEqualsToken, "Type of RHS should be integer.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Export type as integer
            _typeContour.Update(lessThanEqualsToken, BOOLEAN_TYPE);

            return Unit.Instance;
        }
        
        public override Unit Visit(AndToken andToken)
        {
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(andToken.Left);

            // Make sure type of LHS is defined
            if (!_typeContour.Lookup(andToken.Left, out var lhsType))
            {
                return Semantics.Error(andToken, "Type of LHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (lhsType != BOOLEAN_TYPE)
            {
                return Semantics.Error(andToken, "Type of LHS should be boolean.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(andToken.Right);

            // Make sure type of RHS is defined
            if (!_typeContour.Lookup(andToken.Right, out var rhsType))
            {
                return Semantics.Error(andToken, "Type of RHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (rhsType != BOOLEAN_TYPE)
            {
                return Semantics.Error(andToken, "Type of RHS should be boolean.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Export type as integer
            _typeContour.Update(andToken, BOOLEAN_TYPE);

            return Unit.Instance;
        }
        
        public override Unit Visit(OrToken orToken)
        {
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(orToken.Left);

            // Make sure type of LHS is defined
            if (!_typeContour.Lookup(orToken.Left, out var lhsType))
            {
                return Semantics.Error(orToken, "Type of LHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (lhsType != BOOLEAN_TYPE)
            {
                return Semantics.Error(orToken, "Type of LHS should be boolean.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(orToken.Right);

            // Make sure type of RHS is defined
            if (!_typeContour.Lookup(orToken.Right, out var rhsType))
            {
                return Semantics.Error(orToken, "Type of RHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (rhsType != BOOLEAN_TYPE)
            {
                return Semantics.Error(orToken, "Type of RHS should be boolean.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Export type as integer
            _typeContour.Update(orToken, BOOLEAN_TYPE);

            return Unit.Instance;
        }

        public override Unit Visit(SubtractToken subtractToken)
        {
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(subtractToken.Left);

            // Make sure type of LHS is defined
            if (!_typeContour.Lookup(subtractToken.Left, out var lhsType))
            {
                return Semantics.Error(subtractToken, "Type of LHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (lhsType != INTEGER_TYPE)
            {
                return Semantics.Error(subtractToken, "Type of LHS should be integer.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(subtractToken.Right);

            // Make sure type of RHS is defined
            if (!_typeContour.Lookup(subtractToken.Right, out var rhsType))
            {
                return Semantics.Error(subtractToken, "Type of RHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (rhsType != INTEGER_TYPE)
            {
                return Semantics.Error(subtractToken, "Type of RHS should be integer.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Export type as integer
            _typeContour.Update(subtractToken, INTEGER_TYPE);

            return Unit.Instance;
        }

        public override Unit Visit(DivideToken divideToken)
        {
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(divideToken.Left);

            // Make sure type of LHS is defined
            if (!_typeContour.Lookup(divideToken.Left, out var lhsType))
            {
                return Semantics.Error(divideToken, "Type of LHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (lhsType != INTEGER_TYPE)
            {
                return Semantics.Error(divideToken, "Type of LHS should be integer.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(divideToken.Right);

            // Make sure type of RHS is defined
            if (!_typeContour.Lookup(divideToken.Right, out var rhsType))
            {
                return Semantics.Error(divideToken, "Type of RHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (rhsType != INTEGER_TYPE)
            {
                return Semantics.Error(divideToken, "Type of RHS should be integer.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Export type as integer
            _typeContour.Update(divideToken, INTEGER_TYPE);

            return Unit.Instance;
        }

        public override Unit Visit(MultiplyToken multiplyToken)
        {
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(multiplyToken.Left);

            // Make sure type of LHS is defined
            if (!_typeContour.Lookup(multiplyToken.Left, out var lhsType))
            {
                return Semantics.Error(multiplyToken, "Type of LHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (lhsType != INTEGER_TYPE)
            {
                return Semantics.Error(multiplyToken, "Type of LHS should be integer.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(multiplyToken.Right);

            // Make sure type of RHS is defined
            if (!_typeContour.Lookup(multiplyToken.Right, out var rhsType))
            {
                return Semantics.Error(multiplyToken, "Type of RHS expression is not defined.");
            }

            // Make sure type of LHS is integer
            if (rhsType != INTEGER_TYPE)
            {
                return Semantics.Error(multiplyToken, "Type of RHS should be integer.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Export type as integer
            _typeContour.Update(multiplyToken, INTEGER_TYPE);

            return Unit.Instance;
        }

        public override Unit Visit(AtomicToken atomicToken)
        {
            _typeContour.Update(atomicToken, atomicToken.Value switch
            {
                string => STRING_TYPE,
                int => INTEGER_TYPE,
                bool => BOOLEAN_TYPE,
                null => ANY_TYPE,
                UNIT_SYMBOL_VALUE => UNIT_TYPE,
                _ => ANY_TYPE
            });

            return Unit.Instance;
        }

        public override Unit Visit(VariableToken variableToken)
        {
            if (_outerClassToken != null && variableToken.Variable == THIS_VAR)
            {
                _typeContour.Update(variableToken, _outerClassToken.Name);
                
                return Unit.Instance;
            }
            
            if (!_variableContour.Lookup(variableToken.Variable, out var refToken))
            {
                return Semantics.Error(variableToken, "Variable has no reference.");
            }
            
            if (!_typeContour.Lookup(refToken, out var refTokenType))
            {
                return Semantics.Error(variableToken, "Reference token of variable has no type.");
            }
            
            // Type of variable is the type of decl
            _typeContour.Update(variableToken, refTokenType);

            return Unit.Instance;
        }

        public override Unit Visit(AccessToken accessToken)
        {
            // Enter LHS
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(accessToken.Receiver);

            if (!_typeContour.Lookup(accessToken.Receiver, out var receiverType))
            {
                return Semantics.Error(accessToken, "Type of receiver is undefined.");
            }

            // Exist RHS
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Enter RHS
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();
            
            _availableMethods.Push(_methods[receiverType]);

            Visit(accessToken.FunctionCall);

            if (!_typeContour.Lookup(accessToken.FunctionCall, out var functionCallType))
            {
                return Semantics.Error(accessToken, "Type of variable is not defined.");
            }

            _availableMethods.Pop();

            // Exist LHS
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            // Type of access is the type of variable
            _typeContour.Update(accessToken, functionCallType);

            return Unit.Instance;
        }

        public override Unit Visit(InstantiationToken instantiationToken)
        {
            if (!_variableContour.Lookup(instantiationToken.Class, out var classDeclUntyped))
            {
                return Semantics.Error(instantiationToken, "Instantiation of class which does not exist.");
            }

            if (classDeclUntyped is not ClassToken classDecl)
            {
                return Semantics.Error(instantiationToken, "Instantiation of class is only allowed.");
            }

            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(instantiationToken.Actuals);
            
            // Make sure count of formals and actuals are equal
            if (instantiationToken.Actuals.Inner.Count != classDecl.Formals.Inner.Count)
            {
                return Semantics.Error(instantiationToken, "Count of formals and actuals do not match.");
            }

            for (var index = 0; index < instantiationToken.Actuals.Inner.Count; index++)
            {
                var formal = classDecl.Formals.Inner[index];
                var actual = instantiationToken.Actuals.Inner[index];
                
                // Make sure type of actual exist
                if (!_typeContour.Lookup(actual, out var actualType))
                {
                    return Semantics.Error(instantiationToken, "Type of actual is not defined in new.");
                }

                // Make sure type actual is superset of formal
                if (TypeLub(actualType, formal.Type) != formal.Type)
                {
                    return Semantics.Error(instantiationToken, "Type of actual should be superset of formal in new.");
                }
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            
            // Type of new is the type of class
            _typeContour.Update(instantiationToken, instantiationToken.Class);

            return Unit.Instance;
        }

        public override Unit Visit(Formal formal)
        {
            // Make sure shadowing of variable name is disallowed
            if (_variableContour.Lookup(formal.Name, out _))
            {
                return Semantics.Error(formal, "Formal is shadowing another variable.");
            }

            // Make sure the suggested type of formal actually exist
            if (!_variableContour.Lookup(formal.Type, out var classToken))
            {
                return Semantics.Error(formal, "No class matching the name of formal type.");
            }

            // Make sure the reference class decl associated with the type of formal actually exist
            if (classToken is not ClassToken)
            {
                return Semantics.Error(formal, "Type of formal should be a class declaration.");
            }

            _variableContour.Update(formal.Name, formal);
            _typeContour.Update(formal, formal.Type);

            return Unit.Instance;
        }

        public override Unit Visit(ClassToken classToken)
        {
            // Make sure class does not extend itself
            if (classToken.Name == classToken.Inherits)
            {
                return Semantics.Error(classToken, "Class extends itself.");
            }

            // Make sure class is not extending one of basic types
            if (classToken.Name != ANY_TYPE && _primitiveTypes.Contains(classToken.Inherits))
            {
                return Semantics.Error(classToken, "Class cannot extend a native types.");
            }

            // Make sure class being extended actually exist
            if (classToken.Inherits != "native" && classToken.Inherits != NOTHING_TYPE)
            {
                // Make sure extending class has been declared
                if (!_variableContour.Lookup(classToken.Inherits, out var extendingClassUntyped))
                {
                    return Semantics.Error(classToken, "Extended class does not exist.");
                }

                // Make sure extending class decl
                if (extendingClassUntyped is not ClassToken extendingClass)
                {
                    return Semantics.Error(classToken, "Can only extend a class decl.");
                }
                
                // Make sure count of formals and actuals are equal
                if (classToken.Actuals.Inner.Count != extendingClass.Formals.Inner.Count)
                {
                    return Semantics.Error(classToken, "Count of formals and actuals do not match.");
                }

                for (var index = 0; index < classToken.Actuals.Inner.Count; index++)
                {
                    var formal = extendingClass.Formals.Inner[index];
                    var actual = classToken.Actuals.Inner[index];
                    
                    // Make sure type of actual exist
                    if (!_typeContour.Lookup(actual, out var actualType))
                    {
                        return Semantics.Error(classToken, "Type of actual is not defined in class.");
                    }

                    // Make sure type actual is superset of formal
                    if (TypeLub(actualType, formal.Type) != formal.Type)
                    {
                        return Semantics.Error(classToken,
                            "Type of actual should be superset of formal in class.");
                    }
                }
            }

            if (classToken.Name == "Driver" && classToken.Formals.Inner.Any())
            {
                return Semantics.Error(classToken, "Driver class should not have any formals.");
            }

            _outerClassToken = classToken;
            _availableMethods.Push(_methods[classToken.Name]);
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(classToken.Formals);
            Visit(classToken.Features);

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();
            _outerClassToken = null;
            _availableMethods.Clear();

            return Unit.Instance;
        }

        public override Unit Visit(TypedArmToken typedArmToken)
        {
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();
            
            // Make sure arm variable name is not shadowing any variable
            if (_variableContour.Lookup(typedArmToken.Name, out _))
            {
                return Semantics.Error(typedArmToken, "Shadowing variable name is not allowed.");
            }
            
            _variableContour.Update(typedArmToken.Name, typedArmToken);

            Visit(typedArmToken.Result);
            
            // Make sure type used in typed arm branch actually exist
            if (!_variableContour.Lookup(typedArmToken.Type, out var classToken))
            {
                return Semantics.Error(typedArmToken, "Class used in typed arm is not declared.");
            }

            // Make sure type used in typed arm branch is a class declaration
            if (classToken is not ClassToken)
            {
                return Semantics.Error(typedArmToken, "Type should be a class declaration.");
            }
            
            // Make sure dynamic type of arm exist
            if (!_typeContour.Lookup(typedArmToken.Result, out var typeOfArm))
            {
                return Semantics.Error(typedArmToken, "Type of branch arm did not exist.");
            }

            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Type of typed arm branch is static type
            _typeContour.Update(typedArmToken, typeOfArm);

            return Unit.Instance;
        }

        public override Unit Visit(NullArmToken nullArmToken)
        {
            // Type of null arm of branch is ROOT_TYPE
            _typeContour.Update(nullArmToken, ANY_TYPE);

            return Unit.Instance;
        }

        public override Unit Visit(Formals formals)
        {
            foreach (var token in formals.Inner)
            {
                Visit(token);
            }

            return Unit.Instance;
        }

        public override Unit Visit(Tokens tokens)
        {
            foreach (var token in tokens.Inner)
            {
                Visit(token);
            }

            return Unit.Instance;
        }

        public override Unit Visit(Classes classes)
        {
            // Collect class hierarchy
            foreach (var classToken in classes.Inner)
            {
                _hierarchy.Add(classToken.Name, classToken.Inherits);

                if (_variableContour.Lookup(classToken.Name, out _))
                {
                    return Semantics.Error(classToken, "Duplicate class name is not allowed.");
                }
            }
            
            // Collect methods
            var fixedPointReached = false;
            while (!fixedPointReached)
            {
                fixedPointReached = true;
                foreach (var classToken in classes.Inner) {
                    var parentMethods = new Dictionary<string, FunctionDeclToken>();
                    if (classToken.Inherits != NOTHING_TYPE)
                    {
                        if (!_methods.ContainsKey(classToken.Inherits))
                        {
                            fixedPointReached = false;
                            parentMethods = new Dictionary<string, FunctionDeclToken>();
                        }
                        else
                        {
                            parentMethods = _methods[classToken.Inherits];
                        }
                    }

                    var classDeclMethods = parentMethods.ToDictionary(x => x.Key, x => x.Value);
                    foreach (var functionDeclToken in classToken.Features.Inner
                        .Where(x => x is FunctionDeclToken)
                        .Cast<FunctionDeclToken>())
                    {
                        if (classDeclMethods.ContainsKey(functionDeclToken.Name))
                        {
                            var parentMethod = classDeclMethods[functionDeclToken.Name];
                            if (!functionDeclToken.Override)
                            {
                                return Semantics.Error(classToken, "Should use overrides when overriding a parent method.");
                            }

                            if (functionDeclToken.Formals.Inner.Count != parentMethod.Formals.Inner.Count)
                            {
                                return Semantics.Error(classToken, "Count of formals should match when overriding a method.");
                            }
                        
                            for (var i = 0; i < functionDeclToken.Formals.Inner.Count; i++)
                            {
                                var currentFormal = functionDeclToken.Formals.Inner[i];
                                var parentFormal = parentMethod.Formals.Inner[i];

                                if (TypeLub(currentFormal.Type, parentFormal.Type) != parentFormal.Type)
                                {
                                    return Semantics.Error(classToken,
                                        "Current formal type should be superset of parent formal type when overriding a method.");
                                }
                            }
                        }
                    
                        classDeclMethods[functionDeclToken.Name] = functionDeclToken;
                    }

                    _methods[classToken.Name] = classDeclMethods;

                    _variableContour.Update(classToken.Name, classToken);
                    _typeContour.Update(classToken, NOTHING_TYPE);
                }
            }

            // Visit classes
            foreach (var classToken in classes.Inner)
            {
                Visit(classToken);
            }

            return Unit.Instance;
        }

        public override Unit Visit(Match match)
        {
            // Enter expression contour
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(match.Token);

            if (!_typeContour.Lookup(match.Token, out var expressionType))
            {
                return Semantics.Error(match, "Expression type is not defined.");
            }

            // Exist expression contour
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Enter arms contour
            _variableContour = _variableContour.Push();
            _typeContour = _typeContour.Push();

            Visit(match.Arms);

            var armTypes = new List<string>();
            foreach (var armToken in match.Arms.Inner)
            {
                if (!_typeContour.Lookup(armToken, out var armType))
                {
                    return Semantics.Error(armToken, "Type of arm did not exist.");
                }

                armTypes.Add(armType);
            }
            
            // Type of match is lub of all arms
            var armsType = armTypes.Aggregate(TypeLub);
            
            foreach (var armToken in match.Arms.Inner)
            {
                if (armToken is TypedArmToken typedArmToken && TypeLub(typedArmToken.Type, expressionType) != typedArmToken.Type)
                {
                    return Semantics.Error(armToken, "Typed arm branch should narrow down the expression not broaden it.");
                }
            }

            // Exist arms contour
            _variableContour = _variableContour.Pop();
            _typeContour = _typeContour.Pop();

            // Set match type
            _typeContour.Update(match, armsType);

            return Unit.Instance;
        }

        public override Unit Visit(Arms arms)
        {
            if (arms.Inner
                .Where(x => x is TypedArmToken)
                .Cast<TypedArmToken>()
                .Select(x => x.Name)
                .GroupBy(x => x)
                .Any(x => x.Count() > 1))
            {
                return Semantics.Error(arms, "Variable names in match arm should be unique.");
            }
            
            foreach (var armToken in arms.Inner)
            {
                Visit(armToken);
            }
            
            return Unit.Instance;
        }

        private List<string> TypePathToRoot(string t)
        {
            return _hierarchy.TryGetValue(t, out var parent)
                ? new List<string> { t }.Concat(TypePathToRoot(parent)).ToList()
                : new List<string> { ANY_TYPE };
        }

        private string TypeLub(string t1, string t2)
        {
            if (t1 == t2)
            {
                return t2;
            }

            if (t1 == ANY_TYPE || t2 == ANY_TYPE)
            {
                return ANY_TYPE;
            }

            var p1 = TypePathToRoot(t1);
            var p2 = TypePathToRoot(t2);

            return p1.First(x => p2.Contains(x));
        }
    }
}