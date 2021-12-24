namespace Models.Interfaces
{
    public interface IVisitor<out T>
    {
        public T Visit(IToken token);
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
        public T Visit(TypedArmToken armToken);
        public T Visit(NullArmToken nullArmToken);
        public T Visit(Formals formals);
        public T Visit(Tokens tokens);
        public T Visit(Classes classes);
        public T Visit(Match match);
    }
}