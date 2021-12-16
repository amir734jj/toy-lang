// ReSharper disable once RedundantUsingDirective

using System.IO;
using Antlr4.Runtime;

// ReSharper disable once CheckNamespace
// ReSharper disable once CheckNamespace
#pragma warning disable CA1050

public abstract class CoolParserBase : Parser
#pragma warning restore CA1050
{
    public CoolParserBase(ITokenStream input)
        : base(input)
    {
    }

    public CoolParserBase(ITokenStream input, TextWriter output, TextWriter errorOutput) : this(input)
    {
    }
}