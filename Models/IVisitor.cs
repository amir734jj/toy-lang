using Models.Interfaces;

namespace Models
{
    public interface IVisitor<out T>
    {
        public IVisitor<T> AsVisitor();
        
        public T Visit(IToken token)
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
        
        public T Visit(AndToken andToken);
        public T Visit(OrToken orToken);
        public T Visit(NativeToken nativeToken);
        public T Visit(AssignToken assignToken);
        public T Visit(WhileToken whileToken);
        public T Visit(CondToken condToken);
        public T Visit(VarDeclToken varDeclToken);
        public T Visit(FunctionDeclToken functionDeclToken);
        public T Visit(BlockToken blockToken);
        public T Visit(FunctionCallToken functionCallToken);
        public T Visit(NegateToken negateToken);
        public T Visit(NotToken notToken);
        public T Visit(AddToken addToken);
        public T Visit(EqualsToken equalsToken);
        public T Visit(NotEqualsToken notEqualsToken);
        public T Visit(LessThanToken lessThanToken);
        public T Visit(LessThanEqualsToken lessThanEqualsToken);
        public T Visit(SubtractToken subtractToken);
        public T Visit(DivideToken divideToken);
        public T Visit(MultiplyToken multiplyToken);
        public T Visit(AtomicToken atomicToken);
        public T Visit(VariableToken variableToken);
        public T Visit(AccessToken accessToken);
        public T Visit(InstantiationToken instantiationToken);
        public T Visit(Formal formal);
        public T Visit(ClassToken classToken);
        public T Visit(TypedArmToken typedArmToken);
        public T Visit(NullArmToken nullArmToken);
        public T Visit(Formals formals);
        public T Visit(Tokens tokens);
        public T Visit(Classes classes);
        public T Visit(Match match);
        public T Visit(Arms arms);
    }
}
