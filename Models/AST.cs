using System;
using Equ;
using Models.Interfaces;

namespace Models
{
    public interface IToken
    {
        [MemberwiseEqualityIgnore]
        public Guid Id { get; }
    }

    public abstract class Token<T> : MemberwiseEquatable<T>, IToken 
    {
        public Token()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
    }

    #region Misc

    public class CommentToken : Token<CommentToken>
    {
        public CommentToken(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }

    public class CommentsToken : Token<CommentsToken>
    {
        public CommentsToken(IValueCollection<CommentToken> inner)
        {
            Inner = inner;
        }

        public IValueCollection<CommentToken> Inner { get; }
    }

    #endregion

    #region Singular

    public class NativeToken : Token<NativeToken>
    {
    }

    public class AssignToken : Token<AssignToken>
    {
        public AssignToken(string variable, IToken body)
        {
            Variable = variable;
            Body = body;
        }

        public string Variable { get; }
        public IToken Body { get; }
    }

    public class WhileToken : Token<WhileToken>
    {
        public WhileToken(IToken condition, IToken body)
        {
            Condition = condition;
            Body = body;
        }

        public IToken Condition { get; }
        public IToken Body { get; }
    }

    public class CondToken : Token<CondToken>
    {
        public CondToken(IToken condition, IToken ifToken, IToken elseToken)
        {
            Condition = condition;
            IfToken = ifToken;
            ElseToken = elseToken;
        }

        public IToken Condition { get; }
        public IToken IfToken { get; }
        public IToken ElseToken { get; }
    }

    public class VarDeclToken : Token<VarDeclToken>
    {
        public VarDeclToken(string variable, string type, IToken body)
        {
            Variable = variable;
            Type = type;
            Body = body;
        }

        public string Variable { get; }
        public string Type { get; }
        public IToken Body { get; }
    }

    public class FunctionDeclToken : Token<FunctionDeclToken>
    {
        public FunctionDeclToken(bool @override, string name, Formals formals, string type, IToken body)
        {
            Override = @override;
            Name = name;
            Formals = formals;
            Type = type;
            Body = body;
        }

        public bool Override { get; }
        public string Name { get; }
        public Formals Formals { get; }
        public string Type { get; }
        public IToken Body { get; }
    }

    public class BlockToken : Token<BlockToken>
    {
        public BlockToken(Tokens tokens)
        {
            Tokens = tokens;
        }

        public Tokens Tokens { get; }
    }

    public class FunctionCallToken : Token<FunctionCallToken>
    {
        public FunctionCallToken(string name, Tokens actuals)
        {
            Name = name;
            Actuals = actuals;
        }

        public string Name { get; }
        public Tokens Actuals { get; }
    }

    public class NegateToken : Token<NegateToken>
    {
        public NegateToken(IToken token)
        {
            Token = token;
        }

        public IToken Token { get; }
    }

    public class NotToken : Token<NotToken>
    {
        public NotToken(IToken token)
        {
            Token = token;
        }

        public IToken Token { get; }
    }

    public class AddToken : Token<AddToken>
    {
        public AddToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
    }

    public class EqualsToken : Token<EqualsToken>
    {
        public EqualsToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
    }

    public class NotEqualsToken : Token<NotEqualsToken>
    {
        public NotEqualsToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
    }

    public class AndToken : Token<AndToken>
    {
        public AndToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
    }

    public class OrToken : Token<OrToken>
    {
        public OrToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
    }

    public class LessThanToken : Token<LessThanToken>
    {
        public LessThanToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
    }

    public class LessThanEqualsToken : Token<LessThanEqualsToken>
    {
        public LessThanEqualsToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
    }

    public class SubtractToken : Token<SubtractToken>
    {
        public SubtractToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
    }

    public class DivideToken : Token<DivideToken>
    {
        public DivideToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
    }

    public class MultiplyToken : Token<MultiplyToken>
    {
        public MultiplyToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
    }

    public class AtomicToken : Token<AtomicToken>
    {
        public AtomicToken(IConvertible value)
        {
            Value = value;
        }

        public IConvertible Value { get; }
    }

    public class VariableToken : Token<VariableToken>
    {
        public VariableToken(string variable)
        {
            Variable = variable;
        }

        public string Variable { get; }
    }

    public class AccessToken : Token<AccessToken>
    {
        public AccessToken(IToken receiver, IToken variable)
        {
            Receiver = receiver;
            Variable = variable;
        }

        public IToken Receiver { get; }
        public IToken Variable { get; }
    }

    public class InstantiationToken : Token<InstantiationToken>
    {
        public InstantiationToken(string @class, Tokens actuals)
        {
            Class = @class;
            Actuals = actuals;
        }

        public string Class { get; }
        public Tokens Actuals { get; }
    }

    public class Formal : Token<Formal>
    {
        public Formal(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public string Type { get; }
    }

    public class ClassToken : Token<ClassToken>
    {
        public ClassToken(string name, Formals formals, string inherits, Tokens actuals, Tokens features)
        {
            Name = name;
            Formals = formals;
            Inherits = inherits;
            Actuals = actuals;
            Features = features;
        }

        public string Name { get; }
        public Formals Formals { get; }
        public string Inherits { get; }
        public Tokens Actuals { get; }
        public Tokens Features { get; }
    }

    public interface IArmToken : IToken
    {
    }

    public class TypedArmToken : Token<TypedArmToken>, IArmToken
    {
        public TypedArmToken(string name, string type, IToken result)
        {
            Name = name;
            Type = type;
            Result = result;
        }

        public string Name { get; }
        public string Type { get; }
        public IToken Result { get; }
    }

    public class NullArmToken : Token<NullArmToken>, IArmToken
    {
        public NullArmToken(IToken result)
        {
            Result = result;
        }

        public IToken Result { get; }
    }

    public class Match : Token<Match>
    {
        public Match(IToken token, Arms arms)
        {
            Token = token;
            Arms = arms;
        }

        public IToken Token { get; }
        public Arms Arms { get; }
    }

    #endregion

    #region SequenceToken

    public class Formals : Token<Formals>
    {
        public Formals(IValueCollection<Formal> inner)
        {
            Inner = inner;
        }

        public IValueCollection<Formal> Inner { get; }
    }

    public class Tokens : Token<Tokens>
    {
        public Tokens(IValueCollection<IToken> inner)
        {
            Inner = inner;
        }

        public IValueCollection<IToken> Inner { get; }
    }

    public class Classes : Token<Classes>
    {
        public Classes(IValueCollection<ClassToken> inner)
        {
            Inner = inner;
        }

        public IValueCollection<ClassToken> Inner { get; }
    }

    public class Arms : Token<Arms>
    {
        public Arms(IValueCollection<IArmToken> inner)
        {
            Inner = inner;
        }

        public IValueCollection<IArmToken> Inner { get; }
    }

    #endregion
}