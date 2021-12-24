using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
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
        
        private List<string> _scoped = new();
        private bool beingAccessed = false;

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
            var variableName = _scoped.Contains(varDeclToken.Variable)
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
                actualsVars.Add(MakeVariable());
                actualCode += Visit(actual) + ";\n";
                _returnVariable.Pop();
            }

            var functionName = functionCallToken.Name;
            if (!this.beingAccessed && _scoped.Contains(functionCallToken.Name))
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
            beingAccessed = true;
            var rhs = Visit(accessToken.Variable);
            beingAccessed = false;
            return $"{lhs}.{rhs}";
        }

        public override string Visit(InstantiationToken instantiationToken)
        {
            return
                $"new {instantiationToken.Class}({string.Join(',', instantiationToken.Actuals.Inner.Select(Visit))}))";
        }

        public override string Visit(Formal formal)
        {
            return formal.Name;
        }

        public override string Visit(ClassToken classToken)
        {
            foreach (var formal in classToken.Formals.Inner)
            {
                _scoped.Add(formal.Name);
            }
            
            foreach (var functionDeclToken in classToken.Features.Inner.Where(x => x is FunctionDeclToken).Cast<FunctionDeclToken>())
            {
                _scoped.Add(functionDeclToken.Name);
            }
            
            var parentClass = classToken.Inherits is ANY_TYPE or NOTHING_TYPE ? "Object" : classToken.Inherits;

            _indent = 0;
            _joinTokensWith = $"\n{MakeIndent(1)}";
            var features = Visit(classToken.Features);
            
            _indent = 0;
            _joinTokensWith = ",";
            var actuals = Visit(classToken.Actuals);
            
            var result =
                $"class {classToken.Name} extends {parentClass} {{\n" +
                $"{MakeIndent(1)}constructor{Visit(classToken.Formals)} {{\n" +
                $"{MakeIndent(2)}super({actuals});\n" +
                $"{MakeIndent(1)}}}\n" +
                $"{features}\n" +
                $"}}";

            _scoped = new();

            return result;
        }

        public override string Visit(TypedArmToken typedArmToken)
        {
            return $"if ({_returnVariable} instanceof {typedArmToken.Type}) {{\n" +
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
    }
}