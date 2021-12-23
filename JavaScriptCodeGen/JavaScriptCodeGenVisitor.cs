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

        private string _currentVariable = null;

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
                   $"\t{Visit(whileToken.Body)} \n" +
                   $"}}";
        }

        public override string Visit(CondToken condToken)
        {
            return $"if ({Visit(condToken.Condition)})\n" +
                   $"{Visit(condToken.IfToken)}\n " +
                   $"else\n" +
                   $"{Visit(condToken.ElseToken)}";
        }

        public override string Visit(VarDeclToken varDeclToken)
        {
            return $"{GetReturnPrefix(varDeclToken)}var {varDeclToken.Variable} = {Visit(varDeclToken.Body)};";
        }

        public override string Visit(FunctionDeclToken functionDeclToken)
        {

            return $"{functionDeclToken.Name}{Visit(functionDeclToken.Formals)} {{\n" +
                   $"\t{Visit(functionDeclToken.Body)}\n" +
                   $"}}";
        }

        public override string Visit(BlockToken blockToken)
        {
            var prevJoinTokensWith = _joinTokensWith;
            
            _joinTokensWith = $";\n{MakeIndent(_indent)}";

            var result = $"{GetReturnPrefix(blockToken)}{{\n" +
                         $"\t{Visit(blockToken.Tokens)}" +
                         $"}}";

            _joinTokensWith = prevJoinTokensWith;
            
            return result;
        }

        public override string Visit(FunctionCallToken functionCallToken)
        {
            var prevJoinTokensWith = _joinTokensWith;
            
            _joinTokensWith = ",";

            var prevIndent = _indent;
            _indent = 0;

            var result =
                $"{GetReturnPrefix(functionCallToken)}{functionCallToken.Name}({Visit(functionCallToken.Actuals)})";

            _indent = prevIndent;
            _joinTokensWith = prevJoinTokensWith;
            
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
                UNIT_SYMBOL_VALUE => "{}",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public override string Visit(VariableToken variableToken)
        {
            return $"{variableToken.Variable}";
        }

        public override string Visit(AccessToken accessToken)
        {
            return $"{Visit(accessToken.Receiver)}.{Visit(accessToken.Variable)}";
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

            return result;
        }

        public override string Visit(TypedArmToken typedArmToken)
        {
            return $"if ({_currentVariable} instanceof {typedArmToken.Type}) {{\n" +
                   $"var {typedArmToken.Name} = {_currentVariable}\n" +
                   $"{Visit(typedArmToken.Result)}\n" +
                   $"}}";
        }

        public override string Visit(NullArmToken nullArmToken)
        {
            return $"if ({_currentVariable} === null) {{\n" +
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
            return $"var {MakeVariable()} = {Visit(match.Token)};\n" +
                   $"{Visit(match.Arms)}";
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
            _currentVariable = "randomAutoGeneratedVariable" + _randomVariableSeed;
            _randomVariableSeed++;
            return _currentVariable;
        }

        private string GetReturnPrefix(Token token)
        {
            return _allReturnTokens.Contains(token.Id) ? "return " : string.Empty;
        }
    }
}