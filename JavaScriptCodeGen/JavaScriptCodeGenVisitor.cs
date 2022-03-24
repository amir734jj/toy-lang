using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using static Models.Constants;
// ReSharper disable RedundantStringInterpolation

namespace JavaScriptCodeGen
{
    internal class JavaScriptCodeGenVisitor : IVisitor<string>
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

        public IVisitor<string> AsVisitor()
        {
            return this;
        }

        public string Visit(AndToken andToken)
        {
            return $"{GetReturnPrefix(andToken)}{andToken.Left} = {AsVisitor().Visit(andToken.Right)}";
        }

        public string Visit(OrToken orToken)
        {
            return $"{GetReturnPrefix(orToken)}{orToken.Left} = {AsVisitor().Visit(orToken.Right)}";
        }

        public string Visit(NativeToken nativeToken)
        {
            return $"{GetReturnPrefix(nativeToken)}new Error()";
        }

        public string Visit(AssignToken assignToken)
        {
            return $"{GetReturnPrefix(assignToken)}{assignToken.Variable} = {AsVisitor().Visit(assignToken.Body)}";
        }

        public string Visit(WhileToken whileToken)
        {
            return $"while ({AsVisitor().Visit(whileToken.Condition)}) {{\n" +
                   $"\t {AsVisitor().Visit(whileToken.Body)} \n" +
                   $"}}";
        }

        public string Visit(CondToken condToken)
        {
            var result = $"{GetReturnPrefix(condToken)}(({AsVisitor().Visit(condToken.Condition)}) ? ({AsVisitor().Visit(condToken.IfToken)}) : ({AsVisitor().Visit(condToken.ElseToken)}))";
            return result;
        }

        public string Visit(VarDeclToken varDeclToken)
        {
            string variableName;
            string prefix;
            if (_scoped[_currentClassName].Contains(varDeclToken.Variable))
            {
                variableName = $"this.{varDeclToken.Variable}";
                prefix = "";
            }
            else
            {
                variableName = $"{varDeclToken.Variable}";
                prefix = "var ";
            }

            return $"{GetReturnPrefix(varDeclToken)}{prefix}{variableName} = {AsVisitor().Visit(varDeclToken.Body)}; {variableName}";
        }

        public string Visit(FunctionDeclToken functionDeclToken)
        {
            var body = AsVisitor().Visit(functionDeclToken.Body);
            
            // Native stuff should be dumped manually
            if (functionDeclToken.Body is NativeToken)
            {
                throw new NotImplementedException();
            }
            
            var result = $"{functionDeclToken.Name}{AsVisitor().Visit(functionDeclToken.Formals)} {{ \n" +
                         $"\t {body} \n" +
                         $"}}";

            return result;
        }

        public string Visit(BlockToken blockToken)
        {
            if (blockToken.Tokens.Inner.Any())
            {
                _joinTokensWith.Push(";\n");
                var result = $"{GetReturnPrefix(blockToken)}(() => {{ \n" +
                       $"\t{AsVisitor().Visit(blockToken.Tokens)} \n" +
                       $"}}).bind(this)()";
                _joinTokensWith.Pop();
                return result;
            }

            return "new Unit()";
        }

        public string Visit(FunctionCallToken functionCallToken)
        {
            var functionName = functionCallToken.Name;
            if (!_beingAccessed && _scoped[_currentClassName].Contains(functionCallToken.Name))
            {
                functionName = "this." + functionName;
            }
            
            if (_beingAccessed)
            {
                _beingAccessed = false;
            }

            _joinTokensWith.Push(",");
            var actualCode = functionCallToken.Actuals.Inner.Select(AsVisitor().Visit).ToList();
            _joinTokensWith.Pop();
            
            var result = $"{GetReturnPrefix(functionCallToken)}{functionName}({string.Join(',', actualCode)})";

            return result;
        }

        public string Visit(NegateToken negateToken)
        {
            return $"{GetReturnPrefix(negateToken)}-{AsVisitor().Visit(negateToken.Token)}";
        }

        public string Visit(NotToken notToken)
        {
            return $"{GetReturnPrefix(notToken)}-{AsVisitor().Visit(notToken.Token)}";
        }

        public string Visit(AddToken addToken)
        {
            return $"{GetReturnPrefix(addToken)}{AsVisitor().Visit(addToken.Left)} + {AsVisitor().Visit(addToken.Right)}";
        }

        public string Visit(EqualsToken equalsToken)
        {
            return $"{GetReturnPrefix(equalsToken)}{AsVisitor().Visit(equalsToken.Left)} === {AsVisitor().Visit(equalsToken.Right)}";
        }

        public string Visit(NotEqualsToken notEqualsToken)
        {
            return $"{GetReturnPrefix(notEqualsToken)}{AsVisitor().Visit(notEqualsToken.Left)} !== {AsVisitor().Visit(notEqualsToken.Right)}";
        }

        public string Visit(LessThanToken lessThanToken)
        {
            return $"{GetReturnPrefix(lessThanToken)}{AsVisitor().Visit(lessThanToken.Left)} < {AsVisitor().Visit(lessThanToken.Right)}";
        }

        public string Visit(LessThanEqualsToken lessThanEqualsToken)
        {
            return $"{GetReturnPrefix(lessThanEqualsToken)}{AsVisitor().Visit(lessThanEqualsToken.Left)} <= {AsVisitor().Visit(lessThanEqualsToken.Right)}";
        }

        public string Visit(SubtractToken subtractToken)
        {
            return $"{GetReturnPrefix(subtractToken)}{AsVisitor().Visit(subtractToken.Left)} - {AsVisitor().Visit(subtractToken.Right)}";
        }

        public string Visit(DivideToken divideToken)
        {
            return $"{GetReturnPrefix(divideToken)}{AsVisitor().Visit(divideToken.Left)} / {AsVisitor().Visit(divideToken.Right)}";
        }

        public string Visit(MultiplyToken multiplyToken)
        {
            return $"{GetReturnPrefix(multiplyToken)}{AsVisitor().Visit(multiplyToken.Left)} * {AsVisitor().Visit(multiplyToken.Right)}";
        }

        public string Visit(AtomicToken atomicToken)
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

        public string Visit(VariableToken variableToken)
        {
            return _scoped.ContainsKey(variableToken.Variable)
                ? $"this.{variableToken.Variable}"
                : $"{variableToken.Variable}";
        }

        public string Visit(AccessToken accessToken)
        {
            var lhs = AsVisitor().Visit(accessToken.Receiver);
            _beingAccessed = true;
            var rhs = Visit(accessToken.FunctionCall);
            _beingAccessed = false;
            
            return $"{lhs}.{rhs}";
        }

        public string Visit(InstantiationToken instantiationToken)
        {
            return
                $"new {TypeRename(instantiationToken.Class)}({string.Join(',', instantiationToken.Actuals.Inner.Select(AsVisitor().Visit))})";
        }

        public string Visit(Formal formal)
        {
            return formal.Name;
        }

        public string Visit(ClassToken classToken)
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
            var insideConstructor = string.Join(";\n", new[]{$"super({actuals})"}.Concat(classToken.Features.Inner.Where(x => x is not FunctionDeclToken).Select(AsVisitor().Visit)));
            _joinTokensWith.Pop();
            
            var methods = string.Join('\n', classToken.Features.Inner.Where(x => x is FunctionDeclToken).Select(AsVisitor().Visit));
            
            var result =
                $"class {TypeRename(classToken.Name)} {extendsPrefix} {{\n" +
                $"{MakeIndent(1)}constructor{AsVisitor().Visit(classToken.Formals)} {{\n" +
                $"{insideConstructor}\n" +
                $"{MakeIndent(1)}}}\n" +
                $"{methods}\n" +
                $"}}";

            _currentClassName = "";

            return result;
        }

        public string Visit(TypedArmToken typedArmToken)
        {
            return $"({_returnVariable.Peek()} instanceof {TypeRename(typedArmToken.Type)} && " +
                   $"({typedArmToken.Name} = {_returnVariable.Peek()})) ? " +
                   $"{AsVisitor().Visit(typedArmToken.Result)}";
        }

        public string Visit(NullArmToken nullArmToken)
        {
            return $"({_returnVariable.Peek()} === null) ? {AsVisitor().Visit(nullArmToken.Result)}";
        }

        public string Visit(Formals formals)
        {
            return $"({string.Join(',', formals.Inner.Select(Visit))})";
        }

        public string Visit(Tokens tokens)
        {
            return string.Join(_joinTokensWith.Peek(), tokens.Inner.Select(AsVisitor().Visit));
        }

        public string Visit(Classes classes)
        {
            bool dumpMainCall = false;
            
            // Collect methods
            var fixedPointReached = false;
            while (!fixedPointReached)
            {
                fixedPointReached = true;
                foreach (var classToken in classes.Inner)
                {
                    dumpMainCall |= classToken.Name == "Driver";
                    var parentMethods = new List<string>();
                    
                    if (classToken.Inherits != NOTHING_TYPE)
                    {
                        if (!_scoped.ContainsKey(classToken.Inherits))
                        {
                            fixedPointReached = false;
                            parentMethods = new List<string>();
                        }
                        else
                        {
                            parentMethods = _scoped[classToken.Inherits];
                        }
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
            }
            
            var returnExpressionVisitor = new ReturnExpressionVisitor();
            var returnFinder = returnExpressionVisitor.Visit(classes);
            
            _allReturnTokens = returnFinder.ToHashSet();
            
            var result = string.Join('\n', classes.Inner.Select(Visit));

            if (dumpMainCall)
            {
                result += "\n" +
                          "new Driver()";
            }

            return result;
        }

        public string Visit(Match match)
        {
            var returnVar = MakeVariable();
            
            var variables = match.Arms.Inner.Where(x => x is TypedArmToken).Cast<TypedArmToken>().Select(x => x.Name);
            var decls = "var " + string.Join(',', variables);

            return $"{GetReturnPrefix(match)}" +
                   $"(({returnVar}) => {{ {decls};\n" +
                   $"return {AsVisitor().Visit(match.Arms)} }}).bind(this)({AsVisitor().Visit(match.Token)})";
        }

        public string Visit(Arms arms)
        {
            var result = "";
            var closing = "";
            foreach (var armToken in arms.Inner)
            {
                result += "(" + AsVisitor().Visit(armToken) + ": ";
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

        private static string TypeRename(string type)
        {
            return type switch
            {
                "String" => "StringC",
                "Boolean" => "BooleanC",
                "Int" => "IntC",
                _ => type
            };
        }
    }
}
