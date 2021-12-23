using System;
using Models.Interfaces;

namespace Models
{
    public class Token
    {
    }

    #region Misc

    public class CommentToken : Token
    {
        public CommentToken(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }

    public class CommentsToken : Token
    {
        public CommentsToken(IValueCollection<CommentToken> inner)
        {
            Inner = inner;
        }

        public IValueCollection<CommentToken> Inner { get;  }
    }

    #endregion

    #region Singular

    public class NativeToken : Token
    {
    }

    public class AssignToken : Token
    {
        public AssignToken(string variable, Token body)
        {
            Variable = variable;
            Body = body;
        }

        public string Variable { get;  }
        public Token Body { get;  }
    }

    public class WhileToken : Token
    {
        public WhileToken(Token condition, Token body)
        {
            Condition = condition;
            Body = body;
        }

        public Token Condition { get;  }
        public Token Body { get;  }
    }

    public class CondToken : Token
    {
        public CondToken(Token condition, Token ifToken, Token elseToken)
        {
            Condition = condition;
            IfToken = ifToken;
            ElseToken = elseToken;
        }

        public Token Condition { get;  }
        public Token IfToken { get;  }
        public Token ElseToken { get;  }
    }

    public class VarDeclToken : Token
    {
        public VarDeclToken(string variable, string type, Token body)
        {
            Variable = variable;
            Type = type;
            Body = body;
        }

        public string Variable { get;  }
        public string Type { get;  }
        public Token Body { get;  }
    }

    public class FunctionDeclToken : Token
    {
        public FunctionDeclToken(bool @override, string name, Formals formals, string type, Token body)
        {
            Override = @override;
            Name = name;
            Formals = formals;
            Type = type;
            Body = body;
        }

        public bool Override { get;  }
        public string Name { get;  }
        public Formals Formals { get;  }
        public string Type { get;  }
        public Token Body { get;  }
    }

    public class BlockToken : Token
    {
        public BlockToken(Tokens tokens)
        {
            Tokens = tokens;
        }

        public Tokens Tokens { get;  }
    }

    public class FunctionCallToken : Token
    {
        public FunctionCallToken(string name, Tokens actuals)
        {
            Name = name;
            Actuals = actuals;
        }

        public string Name { get;  }
        public Tokens Actuals { get;  }
    }

    public class NegateToken : Token
    {
        public NegateToken(Token token)
        {
            Token = token;
        }

        public Token Token { get;  }
    }

    public class NotToken : Token
    {
        public NotToken(Token token)
        {
            Token = token;
        }

        public Token Token { get;  }
    }

    public class AddToken : Token
    {
        public AddToken(Token left, Token right)
        {
            Left = left;
            Right = right;
        }

        public Token Left { get;  }
        public Token Right { get;  }
    }

    public class EqualsToken : Token
    {
        public EqualsToken(Token left, Token right)
        {
            Left = left;
            Right = right;
        }

        public Token Left { get;  }
        public Token Right { get;  }
    }

    public class NotEqualsToken : Token
    {
        public NotEqualsToken(Token left, Token right)
        {
            Left = left;
            Right = right;
        }

        public Token Left { get;  }
        public Token Right { get;  }
    }

    public class AndToken : Token
    {
        public AndToken(Token left, Token right)
        {
            Left = left;
            Right = right;
        }

        public Token Left { get;  }
        public Token Right { get;  }
    }

    public class OrToken : Token
    {
        public OrToken(Token left, Token right)
        {
            Left = left;
            Right = right;
        }

        public Token Left { get;  }
        public Token Right { get;  }
    }

    public class LessThanToken : Token
    {
        public LessThanToken(Token left, Token right)
        {
            Left = left;
            Right = right;
        }

        public Token Left { get;  }
        public Token Right { get;  }
    }

    public class LessThanEqualsToken : Token
    {
        public LessThanEqualsToken(Token left, Token right)
        {
            Left = left;
            Right = right;
        }

        public Token Left { get;  }
        public Token Right { get;  }
    }

    public class SubtractToken : Token
    {
        public SubtractToken(Token left, Token right)
        {
            Left = left;
            Right = right;
        }

        public Token Left { get;  }
        public Token Right { get;  }
    }

    public class DivideToken : Token
    {
        public DivideToken(Token left, Token right)
        {
            Left = left;
            Right = right;
        }

        public Token Left { get;  }
        public Token Right { get;  }
    }

    public class MultiplyToken : Token
    {
        public MultiplyToken(Token left, Token right)
        {
            Left = left;
            Right = right;
        }

        public Token Left { get;  }
        public Token Right { get;  }
    }

    public class AtomicToken : Token
    {
        public AtomicToken(IConvertible value)
        {
            Value = value;
        }

        public IConvertible Value { get;  }
    }

    public class VariableToken : Token
    {
        public VariableToken(string variable)
        {
            Variable = variable;
        }

        public string Variable { get;  }
    }

    public class AccessToken : Token
    {
        public AccessToken(Token receiver, Token variable)
        {
            Receiver = receiver;
            Variable = variable;
        }

        public Token Receiver { get;  }
        public Token Variable { get;  }
    }

    public class InstantiationToken : Token
    {
        public InstantiationToken(string @class, Tokens actuals)
        {
            Class = @class;
            Actuals = actuals;
        }

        public string Class { get;  }
        public Tokens Actuals { get;  }
    }

    public class Formal : Token
    {
        public Formal(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get;  }
        public string Type { get;  }
    }

    public class ClassToken : Token
    {
        public ClassToken(string name, Formals formals, string inherits, Tokens actuals, Tokens features)
        {
            Name = name;
            Formals = formals;
            Inherits = inherits;
            Actuals = actuals;
            Features = features;
        }

        public string Name { get;  }
        public Formals Formals { get;  }
        public string Inherits { get;  }
        public Tokens Actuals { get;  }
        public Tokens Features { get;  }
    }

    public class ArmToken : Token
    {
    }

    public class TypedArmToken : ArmToken
    {
        public TypedArmToken(string name, string type, Token result)
        {
            Name = name;
            Type = type;
            Result = result;
        }

        public string Name { get;  }
        public string Type { get;  }
        public Token Result { get;  }
    }

    public class NullArmToken : ArmToken
    {
        public NullArmToken(Token result)
        {
            Result = result;
        }

        public Token Result { get;  }
    }

    public class Match : Token
    {
        public Match(Token token, Arms arms)
        {
            Token = token;
            Arms = arms;
        }

        public Token Token { get;  }
        public Arms Arms { get;  }
    }

    #endregion
    
    #region SequenceToken
    
    public class Formals : Token
    {
        public Formals(IValueCollection<Formal> inner)
        {
            Inner = inner;
        }

        public IValueCollection<Formal> Inner { get;  }
    }

    public class Tokens : Token
    {
        public Tokens(IValueCollection<Token> inner)
        {
            Inner = inner;
        }

        public IValueCollection<Token> Inner { get;  }
    }

    public class Classes : Token
    {
        public Classes(IValueCollection<ClassToken> inner)
        {
            Inner = inner;
        }

        public IValueCollection<ClassToken> Inner { get;  }
    }

    public class Arms : Token
    {
        public Arms(IValueCollection<ArmToken> inner)
        {
            Inner = inner;
        }

        public IValueCollection<ArmToken> Inner { get;  }
    }

    #endregion
}