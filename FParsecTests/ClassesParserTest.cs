using System.Collections.Generic;
using FParsec.CSharp;
using FParsecParser;
using Models;
using Models.Extensions;
using Xunit;
using static Models.Constants;

namespace ParserTests
{
    public class ClassesParserTest
    {
        [Theory]
        [InlineData("class Foo() extends native { } class Bar() extends native { }")]
        public void Test_Natives( string text)
        {
            // Act
            var reply = Parser.Classes().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new Classes(new List<ClassToken>
            {
                new("Foo", new Formals(new List<Formal>().AsValueSemantics()), NOTHING_TYPE,
                    new Tokens(new List<IToken>().AsValueSemantics()), new Tokens(new List<IToken>().AsValueSemantics())),
                new("Bar", new Formals(new List<Formal>().AsValueSemantics()), NOTHING_TYPE,
                    new Tokens(new List<IToken>().AsValueSemantics()), new Tokens(new List<IToken>().AsValueSemantics()))
            }.AsValueSemantics()), reply.Result);
        }
    }
}