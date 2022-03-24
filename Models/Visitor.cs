using Models.Interfaces;

namespace Models
{
    public abstract class Visitor<T> : IVisitor<T>
    {
        public virtual T Visit(IToken token)
        {
            return token switch
            {
                NativeToken nativeToken => Visit(nativeToken),
                AssignToken assignToken => Visit(assignToken),
                WhileToken whileToken => Visit(whileToken),
                CondToken condToken => Visit(condToken),
                VarDeclToken varDeclToken => Visit(varDeclToken),
                FunctionDeclToken functionDeclToken => Visit(functionDeclToken),
                BlockToken blockToken => Visit(blockToken),
                FunctionCallToken functionCallToken => Visit(functionCallToken),
                NegateToken negateToken => Visit(negateToken),
                NotToken notToken => Visit(notToken),
                AddToken addToken => Visit(addToken),
                AndToken andToken => Visit(andToken),
                OrToken orToken => Visit(orToken),
                EqualsToken equalsToken => Visit(equalsToken),
                NotEqualsToken notEqualsToken => Visit(notEqualsToken),
                LessThanToken lessThanToken => Visit(lessThanToken),
                LessThanEqualsToken lessThanEqualsToken => Visit(lessThanEqualsToken),
                SubtractToken subtractToken => Visit(subtractToken),
                DivideToken divideToken => Visit(divideToken),
                MultiplyToken multiplyToken => Visit(multiplyToken),
                AtomicToken atomicToken => Visit(atomicToken),
                VariableToken variableToken => Visit(variableToken),
                AccessToken accessToken => Visit(accessToken),
                InstantiationToken instantiationToken => Visit(instantiationToken),
                Formal formal => Visit(formal),
                ClassToken classToken => Visit(classToken),
                Arms arms => Visit(arms),
                TypedArmToken typedArmToken => Visit(typedArmToken),
                NullArmToken nullArmToken => Visit(nullArmToken),
                Formals formals => Visit(formals),
                Tokens tokens => Visit(tokens),
                Classes classes => Visit(classes),
                Match match => Visit(match),
                _ => default
            };
        }
        
        public abstract T Visit(AndToken andToken);
        public abstract T Visit(OrToken orToken);
        public abstract T Visit(NativeToken nativeToken);
        public abstract T Visit(AssignToken assignToken);
        public abstract T Visit(WhileToken whileToken);
        public abstract T Visit(CondToken condToken);
        public abstract T Visit(VarDeclToken varDeclToken);
        public abstract T Visit(FunctionDeclToken functionDeclToken);
        public abstract T Visit(BlockToken blockToken);
        public abstract T Visit(FunctionCallToken functionCallToken);
        public abstract T Visit(NegateToken negateToken);
        public abstract T Visit(NotToken notToken);
        public abstract T Visit(AddToken addToken);
        public abstract T Visit(EqualsToken equalsToken);
        public abstract T Visit(NotEqualsToken notEqualsToken);
        public abstract T Visit(LessThanToken lessThanToken);
        public abstract T Visit(LessThanEqualsToken lessThanEqualsToken);
        public abstract T Visit(SubtractToken subtractToken);
        public abstract T Visit(DivideToken divideToken);
        public abstract T Visit(MultiplyToken multiplyToken);
        public abstract T Visit(AtomicToken atomicToken);
        public abstract T Visit(VariableToken variableToken);
        public abstract T Visit(AccessToken accessToken);
        public abstract T Visit(InstantiationToken instantiationToken);
        public abstract T Visit(Formal formal);
        public abstract T Visit(ClassToken classToken);
        public abstract T Visit(TypedArmToken typedArmToken);
        public abstract T Visit(NullArmToken nullArmToken);
        public abstract T Visit(Formals formals);
        public abstract T Visit(Tokens tokens);
        public abstract T Visit(Classes classes);
        public abstract T Visit(Match match);
        public abstract T Visit(Arms arms);
    }
}