using System;
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
        
        public override string Visit(NativeToken nativeToken)
        {
            return "new Error()";
        }

        public override string Visit(AssignToken assignToken)
        {
            return $"{assignToken.Variable} = {Visit(assignToken.Body)}";
        }

        public override string Visit(WhileToken whileToken)
        {
            return $"{MakeIndent(_indent)}while ({Visit(whileToken.Condition)}) {{\n" +
                   $"{MakeIndent(_indent + 1)}{Visit(whileToken.Body)} \n" +
                   $"{MakeIndent(_indent)}}}";
        }

        public override string Visit(CondToken condToken)
        {
            return $"({Visit(condToken.Condition)}) ? " +
                   $"{Visit(condToken.IfToken)} : " +
                   $"{Visit(condToken.ElseToken)}";
        }

        public override string Visit(VarDeclToken varDeclToken)
        {
            return $"var {varDeclToken.Variable} = {Visit(varDeclToken.Body)};";
        }

        public override string Visit(FunctionDeclToken functionDeclToken)
        {
            return $"{MakeIndent(_indent)}{functionDeclToken.Name}{Visit(functionDeclToken.Formals)} {{\n" +
                   $"{MakeIndent(_indent + 1)}{Visit(functionDeclToken.Body)}\n" +
                   $"{MakeIndent(_indent)}}}";
        }

        public override string Visit(BlockToken blockToken)
        {
            var prevJoinTokensWith = _joinTokensWith;
            
            _joinTokensWith = ";\n";

            var result = $"(() => {{ {MakeIndent(_indent + 1)}{Visit(blockToken.Tokens)} }})()";

            _joinTokensWith = prevJoinTokensWith;
            
            return result;
        }

        public override string Visit(FunctionCallToken functionCallToken)
        {
            var prevJoinTokensWith = _joinTokensWith;
            
            _joinTokensWith = ",";

            var prevIndent = _indent;
            _indent = 0;
            
            var result = $"{functionCallToken.Name}({Visit(functionCallToken.Actuals)})";

            _indent = prevIndent;
            _joinTokensWith = prevJoinTokensWith;
            
            return result;
        }

        public override string Visit(NegateToken negateToken)
        {
            return $"-{Visit(negateToken.Token)}";
        }

        public override string Visit(NotToken notToken)
        {
            return $"-{Visit(notToken.Token)}";
        }

        public override string Visit(AddToken addToken)
        {
            return $"{Visit(addToken.Left)} + {Visit(addToken.Right)}";
        }

        public override string Visit(EqualsToken equalsToken)
        {
            return $"{Visit(equalsToken.Left)} === {Visit(equalsToken.Right)}";
        }

        public override string Visit(NotEqualsToken notEqualsToken)
        {
            return $"{Visit(notEqualsToken.Left)} !== {Visit(notEqualsToken.Right)}";
        }

        public override string Visit(LessThanToken lessThanToken)
        {
            return $"{Visit(lessThanToken.Left)} < {Visit(lessThanToken.Right)}";
        }

        public override string Visit(LessThanEqualsToken lessThanEqualsToken)
        {
            return $"{Visit(lessThanEqualsToken.Left)} <= {Visit(lessThanEqualsToken.Right)}";
        }

        public override string Visit(SubtractToken subtractToken)
        {
            return $"{Visit(subtractToken.Left)} - {Visit(subtractToken.Right)}";
        }

        public override string Visit(DivideToken divideToken)
        {
            return $"{Visit(divideToken.Left)} + {Visit(divideToken.Right)}";
        }

        public override string Visit(MultiplyToken multiplyToken)
        {
            return $"{Visit(multiplyToken.Left)} / {Visit(multiplyToken.Right)}";
        }

        public override string Visit(AtomicToken atomicToken)
        {
            return atomicToken.Value switch
            {
                string str => $"{str}",
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

            var prevJoinTokensWith = _joinTokensWith;
            
            _joinTokensWith = ";\n    ";

            _indent++;
            var features = Visit(classToken.Features);
            _indent--;
            
            var result =
                $"class {classToken.Name} extends {parentClass} {{\n" +
                $"{MakeIndent(1)}constructor {Visit(classToken.Formals)} {{\n" +
                $"{MakeIndent(2)}super({string.Join(',', classToken.Actuals.Inner.Select(Visit))});\n" +
                $"{MakeIndent(1)}}}\n" +
                $"{features}\n" +
                $"}}";

            _joinTokensWith = prevJoinTokensWith;
            
            return result;
        }

        public override string Visit(TypedArmToken typedArmToken)
        {
            return $"({typedArmToken.Name} instanceof {typedArmToken.Type}) {{ {Visit(typedArmToken.Result)} }}";
        }

        public override string Visit(NullArmToken nullArmToken)
        {
            throw new NotImplementedException();
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
            return string.Join('\n', classes.Inner.Select(Visit));
        }

        public override string Visit(Match match)
        {
            return "";
        }

        public override string Visit(Arms arms)
        {
            return string.Join(" ", arms.Inner.Select(Visit));
        }

        private string MakeIndent(int indent)
        {
            return new string(Enumerable.Range(0, indent).Select(_ => '\t').ToArray());
        }
    }
}