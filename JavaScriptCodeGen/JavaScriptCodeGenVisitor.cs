﻿using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using static Models.Constants;
// ReSharper disable RedundantStringInterpolation

namespace JavaScriptCodeGen
{
    internal class JavaScriptCodeGenVisitor : Visitor<string>
    {
        private string _joinTokensWith = ";";

        private int _indent;

        private int _randomVariableSeed = 100;

        private HashSet<Guid> _allReturnTokens = new();

        private readonly Stack<string> _returnVariable = new();

        private bool _beingAccessed;

        private readonly string[] _basicTypes =
            { ANY_TYPE, STRING_TYPE, INTEGER_TYPE, BOOLEAN_TYPE, UNIT_TYPE, NOTHING_TYPE, ARRAY_ANY_TYPE, SYMBOL_TYPE, IO_TYPE };

        private readonly Dictionary<string, List<string>> _scoped = new();

        private string _currentClassName = "";
        
        public override string Visit(AndToken andToken)
        {
            return $"{GetReturnPrefix(andToken)}{andToken.Left} = {Visit(andToken.Right)}";
        }

        public override string Visit(OrToken orToken)
        {
            return $"{GetReturnPrefix(orToken)}{orToken.Left} = {Visit(orToken.Right)}";
        }

        public override string Visit(NativeToken nativeToken)
        {
            return $"{GetReturnPrefix(nativeToken)}new Error()";
        }

        public override string Visit(AssignToken assignToken)
        {
            return $"{GetReturnPrefix(assignToken)}{assignToken.Variable} = {Visit(assignToken.Body)}";
        }

        public override string Visit(WhileToken whileToken)
        {
            return $"while ({Visit(whileToken.Condition)}) {{\n" +
                   $"\t {Visit(whileToken.Body)} \n" +
                   $"}}";
        }

        public override string Visit(CondToken condToken)
        {
            var returnVar = MakeVariable();
            var result = $"var {returnVar}; \n" +
                         $"if ({Visit(condToken.Condition)}) \n" +
                         $"\t {Visit(condToken.IfToken)} \n" +
                         $"else \n" +
                         $"\t {Visit(condToken.ElseToken)}";
            _returnVariable.Pop();
            return result;
        }

        public override string Visit(VarDeclToken varDeclToken)
        {
            var variableName = _scoped[_currentClassName].Contains(varDeclToken.Variable)
                ? $"this.{varDeclToken.Variable}"
                : varDeclToken.Variable;
            
            return $"{GetReturnPrefix(varDeclToken)}var {variableName} = {Visit(varDeclToken.Body)};";
        }

        public override string Visit(FunctionDeclToken functionDeclToken)
        {
            var returnVar = MakeVariable();
            var body = Visit(functionDeclToken.Body);
            
            // Native stuff should be dumped manually
            if (functionDeclToken.Body is NativeToken)
            {
                throw new NotImplementedException();
            }
            
            var result = $"{functionDeclToken.Name}{Visit(functionDeclToken.Formals)} {{ \n" +
                         $"\t {body} \n" +
                         $"\t return {returnVar}; \n" +
                         $"}}";
            _returnVariable.Pop();
            return result;
        }

        public override string Visit(BlockToken blockToken)
        {
            var prevJoinTokensWith = _joinTokensWith;
            
            _joinTokensWith = $";\n{MakeIndent(_indent)}";

            var result = $"{GetReturnPrefix(blockToken)}{{ \n" +
                         $"\t{Visit(blockToken.Tokens)} \n" +
                         $"}}";

            _joinTokensWith = prevJoinTokensWith;
            
            return result;
        }

        public override string Visit(FunctionCallToken functionCallToken)
        {
            _joinTokensWith = ",";

            var actualCode = string.Empty;
            var actualsVars = new List<string>();
            
            foreach (var actual in functionCallToken.Actuals.Inner)
            {
                var returnVar = MakeVariable();
                actualsVars.Add(returnVar);
                actualCode += $"var {returnVar} = {Visit(actual)};\n";
                _returnVariable.Pop();
            }

            var functionName = functionCallToken.Name;
            if (!_beingAccessed && _scoped[_currentClassName].Contains(functionCallToken.Name))
            {
                functionName = "this." + functionName;
            }

            var result = $"{actualCode}\n" +
                         $"{GetReturnPrefix(functionCallToken)}{functionName}({string.Join(',', actualsVars)})";

            return result;
        }

        public override string Visit(NegateToken negateToken)
        {
            return $"{GetReturnPrefix(negateToken)}-{Visit(negateToken.Token)}";
        }

        public override string Visit(NotToken notToken)
        {
            return $"{GetReturnPrefix(notToken)}-{Visit(notToken.Token)}";
        }

        public override string Visit(AddToken addToken)
        {
            return $"{GetReturnPrefix(addToken)}{Visit(addToken.Left)} + {Visit(addToken.Right)}";
        }

        public override string Visit(EqualsToken equalsToken)
        {
            return $"{GetReturnPrefix(equalsToken)}{Visit(equalsToken.Left)} === {Visit(equalsToken.Right)}";
        }

        public override string Visit(NotEqualsToken notEqualsToken)
        {
            return $"{GetReturnPrefix(notEqualsToken)}{Visit(notEqualsToken.Left)} !== {Visit(notEqualsToken.Right)}";
        }

        public override string Visit(LessThanToken lessThanToken)
        {
            return $"{GetReturnPrefix(lessThanToken)}{Visit(lessThanToken.Left)} < {Visit(lessThanToken.Right)}";
        }

        public override string Visit(LessThanEqualsToken lessThanEqualsToken)
        {
            return $"{GetReturnPrefix(lessThanEqualsToken)}{Visit(lessThanEqualsToken.Left)} <= {Visit(lessThanEqualsToken.Right)}";
        }

        public override string Visit(SubtractToken subtractToken)
        {
            return $"{GetReturnPrefix(subtractToken)}{Visit(subtractToken.Left)} - {Visit(subtractToken.Right)}";
        }

        public override string Visit(DivideToken divideToken)
        {
            return $"{GetReturnPrefix(divideToken)}{Visit(divideToken.Left)} + {Visit(divideToken.Right)}";
        }

        public override string Visit(MultiplyToken multiplyToken)
        {
            return $"{GetReturnPrefix(multiplyToken)}{Visit(multiplyToken.Left)} / {Visit(multiplyToken.Right)}";
        }

        public override string Visit(AtomicToken atomicToken)
        {
            return GetReturnPrefix(atomicToken) + atomicToken.Value switch
            {
                string str => @$"""{str}""",
                int number => number.ToString(),
                bool boolean => boolean.ToString().ToLower(),
                null => null,
                UNIT_SYMBOL_VALUE => "new Unit()",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public override string Visit(VariableToken variableToken)
        {
            return $"{variableToken.Variable}";
        }

        public override string Visit(AccessToken accessToken)
        {
            var lhs = Visit(accessToken.Receiver);
            _beingAccessed = true;
            var rhs = Visit(accessToken.FunctionCall);
            _beingAccessed = false;
            return $"{lhs}.{rhs}";
        }

        public override string Visit(InstantiationToken instantiationToken)
        {
            return
                $"new {TypeRename(instantiationToken.Class)}({string.Join(',', instantiationToken.Actuals.Inner.Select(Visit))}))";
        }

        public override string Visit(Formal formal)
        {
            return formal.Name;
        }

        public override string Visit(ClassToken classToken)
        {
            if (_basicTypes.Contains(classToken.Name))
            {
                return "";
            }

            _currentClassName = classToken.Name;
            var extendsPrefix = classToken.Inherits != ANY_TYPE ? $"extends {TypeRename(classToken.Inherits)}" : "";

            _indent = 0;
            _joinTokensWith = $"\n{MakeIndent(1)}";
            var methods = string.Join('\n', classToken.Features.Inner.Where(x => x is FunctionDeclToken));
            var insideConstructor = string.Join('\n', classToken.Features.Inner.Where(x => x is not FunctionDeclToken).Select(Visit));
            
            _indent = 0;
            _joinTokensWith = ",";
            var actuals = Visit(classToken.Actuals);

            var result =
                $"class {classToken.Name} {extendsPrefix} {{\n" +
                $"{MakeIndent(1)}constructor{Visit(classToken.Formals)} {{\n" +
                $"{MakeIndent(2)}super({actuals});\n" +
                $"{insideConstructor}\n" +
                $"{MakeIndent(1)}}}\n" +
                $"{methods}\n" +
                $"}}";

            _currentClassName = "";

            return result;
        }

        public override string Visit(TypedArmToken typedArmToken)
        {
            return $"if ({_returnVariable} instanceof {TypeRename(typedArmToken.Type)}) {{\n" +
                   $"var {typedArmToken.Name} = {_returnVariable}\n" +
                   $"{Visit(typedArmToken.Result)}\n" +
                   $"}}";
        }

        public override string Visit(NullArmToken nullArmToken)
        {
            return $"if ({_returnVariable.Peek()} === null) {{\n" +
                   $"{Visit(nullArmToken.Result)}\n" +
                   $"}}";
        }

        public override string Visit(Formals formals)
        {
            return $"({string.Join(',', formals.Inner.Select(Visit))})";
        }

        public override string Visit(Tokens tokens)
        {
            return string.Join(_joinTokensWith, tokens.Inner.Select(Visit));
        }

        public override string Visit(Classes classes)
        {
            // Collect methods
            foreach (var classToken in classes.Inner)
            {
                var parentMethods = new List<string>();
                if (classToken.Inherits != NOTHING_TYPE)
                {
                    parentMethods = _scoped[classToken.Inherits];
                }

                var classDeclMethods = parentMethods.Select(x => x).ToList();
                foreach (var functionDeclToken in classToken.Features.Inner
                    .Where(x => x is FunctionDeclToken)
                    .Cast<FunctionDeclToken>())
                {
                    classDeclMethods.Add(functionDeclToken.Name);
                }
                
                foreach (var formal in classToken.Formals.Inner)
                {
                    classDeclMethods.Add(formal.Name);
                }

                _scoped[classToken.Name] = classDeclMethods;
            }
            
            var returnFinder = new ReturnExpressionVisitor();
            _allReturnTokens = returnFinder.Visit(classes).ToHashSet();
            
            return string.Join('\n', classes.Inner.Select(Visit));
        }

        public override string Visit(Match match)
        {
            var returnVar = MakeVariable();
            var result = $"var {returnVar} = {Visit(match.Token)};\n" +
                         $"{Visit(match.Arms)}";

            _returnVariable.Pop();

            return result;
        }

        public override string Visit(Arms arms)
        {
            return string.Join(" ", arms.Inner.Select(Visit));
        }

        private string MakeIndent(int indent)
        {
            return new string(Enumerable.Range(0, indent).Select(_ => '\t').ToArray());
        }

        private string MakeVariable()
        {
            _returnVariable.Push("randomAutoGeneratedVariable" + _randomVariableSeed);
            _randomVariableSeed++;
            return _returnVariable.Peek();
        }

        private string GetReturnPrefix(IToken token)
        {
            return _allReturnTokens.Contains(token.Id) ? $"{_returnVariable.Peek()} = " : string.Empty;
        }

        private string TypeRename(string type)
        {
            return type == "String" ? "StringC" : type;
        }
    }
}