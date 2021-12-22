using System.Collections.Generic;
using FParsec.CSharp;
using FParsecParser;
using Models;
using Models.Extensions;
using Xunit;

namespace ParserTests
{
    public class FormalsParserTest
    {
        [Theory]
        [InlineData("()")]
        [InlineData(" ()")]
        [InlineData("() ")]
        [InlineData(" () ")]
        [InlineData("( )")]
        [InlineData(" ( )")]
        [InlineData("( ) ")]
        [InlineData(" ( ) ")]
        public void Test_Empty(string text)
        {
            // Act
            var reply = Parser.Formals().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new Formals(new List<Formal>().AsValueSemantics()), reply.Result);
        }
        
        [Theory]
        [InlineData("(foo:Bar)")]
        [InlineData(" (foo:Bar)")]
        [InlineData("(foo:Bar) ")]
        [InlineData(" (foo:Bar) ")]
        [InlineData("(foo : Bar)")]
        [InlineData(" (foo : Bar)")]
        [InlineData("(foo : Bar) ")]
        [InlineData(" (foo : Bar) ")]
        public void Test_One(string text)
        {
            // Act
            var reply = Parser.Formals().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(
                new Formals(new List<Formal> { new("foo", "Bar") }.AsValueSemantics()),
                reply.Result);
        }
        
        [Theory]
        [InlineData("(foo:Bar,baz:Qux)")]
        [InlineData(" (foo:Bar,baz:Qux)")]
        [InlineData("(foo:Bar,baz:Qux) ")]
        [InlineData(" (foo:Bar,baz:Qux) ")]
        [InlineData("(foo : Bar , baz : Qux)")]
        [InlineData(" (foo : Bar,baz : Qux)")]
        [InlineData("(foo : Bar,baz : Qux) ")]
        [InlineData(" (foo : Bar,baz : Qux) ")]
        public void Test_NoVar_Many(string text)
        {
            // Act
            var reply = Parser.Formals().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(
                new Formals(new List<Formal> { new("foo", "Bar"), new("baz", "Qux") }
                    .AsValueSemantics()), reply.Result);
        }
    }
}
