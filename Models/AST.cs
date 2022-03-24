using System;
using System.Linq;
using Equ;
using Models.Interfaces;
using Newtonsoft.Json;

namespace Models
{
    public interface IToken
    {
        [JsonIgnore]
        [MemberwiseEqualityIgnore]
        public Guid Id { get; }
    }

    public interface IExpression
    {
        public ClassToken Type { get; set; }
    }

    public abstract class Token<T> : MemberwiseEquatable<T>, IToken 
    {
        public Token()
        {
            Id = Guid.NewGuid();
            AstNode = GetType().Name;
        }

        public override string ToString()
        {
            var fields = GetType().GetProperties()
                .Where(x => x.Name != "Type" && x.Name != "Id")
                .Select(x => x.GetValue(this)?.ToString());
            var result = string.Join(",", fields);
            return $"{AstNode} ({result})";
        }

        public Guid Id { get; }
        
        public string AstNode { get; }
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

    public class NativeToken : Token<NativeToken>, IExpression
    {
        public ClassToken Type { get; set; }
    }

    public class AssignToken : Token<AssignToken>, IExpression
    {
        public AssignToken(string variable, IToken body)
        {
            Variable = variable;
            Body = body;
        }

        public string Variable { get; }
        public IToken Body { get; }
        
        public ClassToken Type { get; set; }
    }

    public class WhileToken : Token<WhileToken>, IExpression
    {
        public WhileToken(IToken condition, IToken body)
        {
            Condition = condition;
            Body = body;
        }

        public IToken Condition { get; }
        public IToken Body { get; }
        
        public ClassToken Type { get; set; }
    }

    public class CondToken : Token<CondToken>, IExpression
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
        
        public ClassToken Type { get; set; }
    }

    public class VarDeclToken : Token<VarDeclToken>, IExpression
    {
        public VarDeclToken(string variable, string localType, IToken body)
        {
            Variable = variable;
            LocalType = localType;
            Body = body;
        }

        public string Variable { get; }
        
        public string LocalType { get; }
        public IToken Body { get; }
        
        public ClassToken Type { get; set; }
    }

    public class FunctionDeclToken : Token<FunctionDeclToken>
    {
        public FunctionDeclToken(bool @override, string name, Formals formals, string returnType, IToken body)
        {
            Override = @override;
            Name = name;
            Formals = formals;
            ReturnType = returnType;
            Body = body;
        }

        public bool Override { get; }
        public string Name { get; }
        public Formals Formals { get; }
        public string ReturnType { get; }
        public IToken Body { get; }
    }

    public class BlockToken : Token<BlockToken>, IExpression
    {
        public BlockToken(Tokens tokens)
        {
            Tokens = tokens;
        }

        public Tokens Tokens { get; }
        
        public ClassToken Type { get; set; }
    }

    public class FunctionCallToken : Token<FunctionCallToken>, IExpression
    {
        public FunctionCallToken(string name, Tokens actuals)
        {
            Name = name;
            Actuals = actuals;
        }

        public string Name { get; }
        public Tokens Actuals { get; }
        
        public ClassToken Type { get; set; }
    }

    public class NegateToken : Token<NegateToken>, IExpression
    {
        public NegateToken(IToken token)
        {
            Token = token;
        }

        public IToken Token { get; }
        
        public ClassToken Type { get; set; }
    }

    public class NotToken : Token<NotToken>, IExpression
    {
        public NotToken(IToken token)
        {
            Token = token;
        }

        public IToken Token { get; }
        
        public ClassToken Type { get; set; }
    }

    public class AddToken : Token<AddToken>, IExpression
    {
        public AddToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
        
        public ClassToken Type { get; set; }
    }

    public class EqualsToken : Token<EqualsToken>, IExpression
    {
        public EqualsToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
        
        public ClassToken Type { get; set; }
    }

    public class NotEqualsToken : Token<NotEqualsToken>, IExpression
    {
        public NotEqualsToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
        
        public ClassToken Type { get; set; }
    }

    public class AndToken : Token<AndToken>, IExpression
    {
        public AndToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
        
        public ClassToken Type { get; set; }
    }

    public class OrToken : Token<OrToken>, IExpression
    {
        public OrToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
        
        public ClassToken Type { get; set; }
    }

    public class LessThanToken : Token<LessThanToken>, IExpression
    {
        public LessThanToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
        
        public ClassToken Type { get; set; }
    }

    public class LessThanEqualsToken : Token<LessThanEqualsToken>, IExpression
    {
        public LessThanEqualsToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
        
        public ClassToken Type { get; set; }
    }

    public class SubtractToken : Token<SubtractToken>, IExpression
    {
        public SubtractToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
        
        public ClassToken Type { get; set; }
    }

    public class DivideToken : Token<DivideToken>, IExpression
    {
        public DivideToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
        
        public ClassToken Type { get; set; }
    }

    public class MultiplyToken : Token<MultiplyToken>, IExpression
    {
        public MultiplyToken(IToken left, IToken right)
        {
            Left = left;
            Right = right;
        }

        public IToken Left { get; }
        public IToken Right { get; }
        
        public ClassToken Type { get; set; }
    }

    public class AtomicToken : Token<AtomicToken>, IExpression
    {
        public AtomicToken(IConvertible value)
        {
            Value = value;
        }

        public IConvertible Value { get; }
        
        public ClassToken Type { get; set; }
    }

    public class VariableToken : Token<VariableToken>, IExpression
    {
        public VariableToken(string variable)
        {
            Variable = variable;
        }

        public string Variable { get; }
        
        public ClassToken Type { get; set; }
    }

    public class AccessToken : Token<AccessToken>, IExpression
    {
        public AccessToken(IToken receiver, FunctionCallToken functionCall)
        {
            Receiver = receiver;
            FunctionCall = functionCall;
        }

        public IToken Receiver { get; }
        public FunctionCallToken FunctionCall { get; }
        
        public ClassToken Type { get; set; }
    }

    public class InstantiationToken : Token<InstantiationToken>, IExpression
    {
        public InstantiationToken(string @class, Tokens actuals)
        {
            Class = @class;
            Actuals = actuals;
        }

        public string Class { get; }
        public Tokens Actuals { get; }
        
        public ClassToken Type { get; set; }
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

    public class Match : Token<Match>, IExpression
    {
        public Match(IToken token, Arms arms)
        {
            Token = token;
            Arms = arms;
        }

        public IToken Token { get; }
        public Arms Arms { get; }
        
        public ClassToken Type { get; set; }
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