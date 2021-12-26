using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using static Models.Constants;
// ReSharper disable RedundantStringInterpolation

namespace JavaScriptCodeGen
{
    internal class JavaScriptCodeGenVisitor : Visitor<string>
    {
        private readonly Stack<string> _joinTokensWith = new();

        private int _indent;

        private int _randomVariableSeed = 100;

        private HashSet<(Guid, IToken)> _allReturnTokens = new();

        private readonly Stack<string> _returnVariable = new();

        private bool _beingAccessed;

        private readonly string[] _basicTypes =
            { ANY_TYPE, STRING_TYPE, INTEGER_TYPE, BOOLEAN_TYPE, UNIT_TYPE, NOTHING_TYPE, ARRAY_ANY_TYPE, SYMBOL_TYPE, IO_TYPE };

        private readonly Dictionary<string, List<string>> _scoped = new();

        private string _currentClassName = "";
        private readonly ReturnExpressionVisitor _returnFinder = new();

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
            var result = $"{GetReturnPrefix(condToken)}(({Visit(condToken.Condition)}) ? ({Visit(condToken.IfToken)}) : ({Visit(condToken.ElseToken)}))";
            return result;
        }

        public override string Visit(VarDeclToken varDeclToken)
        {
            var variableName = _scoped[_currentClassName].Contains(varDeclToken.Variable)
                ? $"this.{varDeclToken.Variable}"
                : $"var {varDeclToken.Variable}";
            
            return $"{GetReturnPrefix(varDeclToken)}{variableName} = {Visit(varDeclToken.Body)};";
        }

        public override string Visit(FunctionDeclToken functionDeclToken)
        {
            var body = Visit(functionDeclToken.Body);
            
            // Native stuff should be dumped manually
            if (functionDeclToken.Body is NativeToken)
            {
                throw new NotImplementedException();
            }
            
            var result = $"{functionDeclToken.Name}{Visit(functionDeclToken.Formals)} {{ \n" +
                         $"\t {body} \n" +
                         $"}}";

            return result;
        }

        public override string Visit(BlockToken blockToken)
        {
            if (blockToken.Tokens.Inner.Any())
            {
                _joinTokensWith.Push(";\n");
                var result = $"{GetReturnPrefix(blockToken)}(() => {{ \n" +
                       $"\t{Visit(blockToken.Tokens)} \n" +
                       $"}}).bind(this)()";
                _joinTokensWith.Pop();
                return result;
            }

            return "new Unit()";
        }

        public override string Visit(FunctionCallToken functionCallToken)
        {
            _joinTokensWith.Push(",");
            var actualCode = functionCallToken.Actuals.Inner.Select(Visit).ToList();
            _joinTokensWith.Pop();

            var functionName = functionCallToken.Name;
            if (!_beingAccessed && _scoped[_currentClassName].Contains(functionCallToken.Name))
            {
                functionName = "this." + functionName;
            }

            var result = $"{GetReturnPrefix(functionCallToken)}{functionName}({string.Join(',', actualCode)})";

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
            return _scoped.ContainsKey(variableToken.Variable)
                ? $"this.{variableToken.Variable}"
                : $"{variableToken.Variable}";
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
                $"new {TypeRename(instantiationToken.Class)}({string.Join(',', instantiationToken.Actuals.Inner.Select(Visit))})";
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
            var extendsPrefix = classToken.Inherits != NOTHING_TYPE ? $"extends {TypeRename(classToken.Inherits)}" : "";

            _indent = 0;
            _joinTokensWith.Push(",");
            var actuals = Visit(classToken.Actuals);
            _joinTokensWith.Pop();
            
            _joinTokensWith.Push(";\n");
            var insideConstructor = string.Join(";\n", new[]{$"super({actuals})"}.Concat(classToken.Features.Inner.Where(x => x is not FunctionDeclToken).Select(Visit)));
            _joinTokensWith.Pop();
            
            var methods = string.Join('\n', classToken.Features.Inner.Where(x => x is FunctionDeclToken).Select(Visit));
            
            var result =
                $"class {classToken.Name} {extendsPrefix} {{\n" +
                $"{MakeIndent(1)}constructor{Visit(classToken.Formals)} {{\n" +
                $"{insideConstructor}\n" +
                $"{MakeIndent(1)}}}\n" +
                $"{methods}\n" +
                $"}}";

            _currentClassName = "";

            return result;
        }

        public override string Visit(TypedArmToken typedArmToken)
        {
            return $"({_returnVariable.Peek()} instanceof {TypeRename(typedArmToken.Type)} && " +
                   $"({typedArmToken.Name} = {_returnVariable.Peek()})) ? " +
                   $"{Visit(typedArmToken.Result)}";
        }

        public override string Visit(NullArmToken nullArmToken)
        {
            return $"({_returnVariable.Peek()} === null) ? {Visit(nullArmToken.Result)}";
        }

        public override string Visit(Formals formals)
        {
            return $"({string.Join(',', formals.Inner.Select(Visit))})";
        }

        public override string Visit(Tokens tokens)
        {
            return string.Join(_joinTokensWith.Peek(), tokens.Inner.Select(Visit));
        }

        public override string Visit(Classes classes)
        {
            bool dumpMainCall = false;
            // Collect methods
            foreach (var classToken in classes.Inner)
            {
                dumpMainCall |= classToken.Name == "Driver";
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
            
            _allReturnTokens = _returnFinder.Visit(classes).ToHashSet();
            
            var result = string.Join('\n', classes.Inner.Select(Visit));

            if (dumpMainCall)
            {
                result += "\n" +
                          "new Driver()";
            }

            return result;
        }

        public override string Visit(Match match)
        {
            var returnVar = MakeVariable();
            
            var variables = match.Arms.Inner.Where(x => x is TypedArmToken).Cast<TypedArmToken>().Select(x => x.Name);
            var decls = "var " + string.Join(',', variables);

            return $"{GetReturnPrefix(match)}" +
                   $"(({returnVar}) => {{ {decls};\n" +
                   $"return {Visit(match.Arms)} }}).bind(this)({Visit(match.Token)})";
        }

        public override string Visit(Arms arms)
        {
            var result = "";
            var closing = "";
            foreach (var armToken in arms.Inner)
            {
                result += "(" + Visit(armToken) + ": ";
                closing += ")";
            }

            result += @"new Error(""pattern matching failed."")" + closing;

            return result;
        }

        private string MakeIndent(int indent)
        {
            return new string(Enumerable.Range(0, indent).Select(_ => '\t').ToArray());
        }

        private string MakeVariable()
        {
            _returnVariable.Push("rndVar" + _randomVariableSeed);
            _randomVariableSeed++;
            return _returnVariable.Peek();
        }

        private string GetReturnPrefix(IToken token)
        {
            return _allReturnTokens.Any(x => x.Item1 == token.Id) ? $"return " : string.Empty;
        }

        private string TypeRename(string type)
        {
            return type == "String" ? "StringC" : type;
        }
    }
}
