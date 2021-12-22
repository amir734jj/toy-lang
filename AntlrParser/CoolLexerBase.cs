using System.IO;
using Antlr4.Runtime;
// ReSharper disable PublicConstructorInAbstractClass
// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable once CheckNamespace
#pragma warning disable CA1050

public abstract class CoolLexerBase : Lexer
#pragma warning restore CA1050
{
    public CoolLexerBase(ICharStream input)
        : base(input)
    {
    }

    public CoolLexerBase(ICharStream input, TextWriter output, TextWriter errorOutput) : this(input)
    {
    }
}