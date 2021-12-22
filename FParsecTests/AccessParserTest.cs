using System.Collections.Generic;
using FParsec.CSharp;
using FParsecParser;
using Models;
using Models.Extensions;
using Xunit;

namespace ParserTests
{
    public class AccessParserTest
    {
        [Theory]
        [InlineData("foo.bar")]
        [InlineData(" foo.bar")]
        [InlineData("foo.bar ")]
        [InlineData(" foo.bar ")]
        public void Test_AccessName(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new AccessToken(new VariableToken("foo"), new VariableToken("bar")), reply.Result);
        }
        
        [Theory]
        [InlineData("foo.bar()")]
        [InlineData(" foo.bar()")]
        [InlineData("foo.bar() ")]
        [InlineData(" foo.bar() ")]
        public void Test_AccessFunction(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(
                new AccessToken(new VariableToken("foo"),
                    new FunctionCallToken("bar", new Tokens(new List<Token>().AsValueSemantics()))),
                reply.Result);
        }
    }
}