using System;
using Models.Interfaces;

namespace Models
{
    internal static class IsExternalInit {}

    public record Token
    {
        // ReSharper disable once UnusedMember.Global
        public Guid Id => Guid.NewGuid();
    }

    #region Misc
    
    public record CommentToken(string Text) : Token;
    
    public record CommentsToken(IValueCollection<CommentToken> Inner) : Token;

    #endregion

    #region Singular

    public record NativeToken : Token;
    
    public record AssignToken(string Variable, Token Body) : Token;

    public record WhileToken(Token Condition, Token Body) : Token;
    
    public record CondToken(Token Condition, Token IfToken, Token ElseToken) : Token;

    public record VarDeclToken(string Variable, string Type, Token Body) : Token;

    public record FunctionDeclToken(bool Override, string Name, Formals Formals, string Type, Token Body) : Token;
    
    public record BlockToken(Tokens Tokens) : Token;

    public record FunctionCallToken(string Receiver, Tokens Actuals) : Token;

    public record NegateToken(Token Token) : Token;

    public record NotToken(Token Token) : Token;
    
    public record AddToken(Token Left, Token Right) : Token;

    public record EqualsToken(Token Left, Token Right) : Token;
    
    public record NotEqualsToken(Token Left, Token Right) : Token;

    public record AndToken(Token Left, Token Right) : Token;
    
    public record OrToken(Token Left, Token Right) : Token;
    
    public record LessThanToken(Token Left, Token Right) : Token;

    public record LessThanEqualsToken(Token Left, Token Right) : Token;

    public record SubtractToken(Token Left, Token Right) : Token;
    
    public record DivideToken(Token Left, Token Right) : Token;
    
    public record MultiplyToken(Token Left, Token Right) : Token;

    public record AtomicToken(IConvertible Value) : Token;

    public record VariableToken(string Variable) : Token;
    
    public record AccessToken(Token Receiver, Token Variable) : Token;
    
    public record InstantiationToken(string Class, Tokens Actuals) : Token;

    public record Formal(string Name, string Type) : Token;

    public record ClassToken(string Name, Formals Formals, string Inherits, Tokens Actuals, Tokens Features) : Token;

    public record ArmToken : Token;
    
    public record TypedArmToken(string Name, string Type, Token Result) : ArmToken;
    
    public record NullArmToken(Token Result) : ArmToken;

    public record Match(Token Token, Arms Arms) : Token;
    
    #endregion
    
    #region SequenceToken
    
    public record Formals(IValueCollection<Formal> Inner) : Token;

    public record Tokens(IValueCollection<Token> Inner) : Token;
    
    public record Classes(IValueCollection<ClassToken> Inner) : Token;

    public record Arms(IValueCollection<ArmToken> Inner) : Token;

    #endregion
}