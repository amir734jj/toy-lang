using System;
using System.Linq;
using Models;
using static Models.Constants;

namespace JavaScriptCodeGen
{
    internal class JavaScriptCodeGenVisitor : Visitor<string>
    {
        public override string Visit(NativeToken nativeToken)
        {
            throw new NotImplementedException();
        }

        public override string Visit(AssignToken assignToken)
        {
            return $"{assignToken.Variable} = {Visit(assignToken.Body)}";
        }

        public override string Visit(WhileToken whileToken)
        {
            return $@"while ({whileToken.Condition}) {{
                        {Visit(whileToken.Body)}
                      }}";
        }

        public override string Visit(CondToken condToken)
        {
            return $@"if ({Visit(condToken.Condition)})
                        {Visit(condToken.IfToken)};
                      else
                        {Visit(condToken.ElseToken)}";
        }

        public override string Visit(VarDeclToken varDeclToken)
        {
            return $@"var {varDeclToken.Variable} = {Visit(varDeclToken.Body)};";
        }

        public override string Visit(FunctionDeclToken functionDeclToken)
        {
            throw new NotImplementedException();
        }

        public override string Visit(BlockToken blockToken)
        {
            throw new NotImplementedException();
        }

        public override string Visit(FunctionCallToken functionCallToken)
        {
            //return
            //    $"{Visit(functionCallToken.Receiver)}({String.Join(',', functionCallToken.Actuals.Inner.Select(Visit))})";
            throw new NotImplementedException();
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
                string str => @$"""{str}""",
                int number => number.ToString(),
                bool boolean => boolean.ToString().ToLower(),
                null => null,
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
            return $@"class {classToken.Name} extends {(classToken.Inherits == ROOT_TYPE ? "Object" : classToken.Inherits)} {{
                        constructor {Visit(classToken.Formals)} {{
                            super({string.Join(',', classToken.Actuals.Inner.Select(Visit))});
                        }}
                        {string.Join(";\n", Visit(classToken.Features))}
                      }}";
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
            return $@"({string.Join(',', formals.Inner.Select(Visit))})";
        }

        public override string Visit(Tokens tokens)
        {
            throw new NotImplementedException();
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
    }
}