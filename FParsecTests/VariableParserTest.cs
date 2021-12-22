using FParsec.CSharp;
using FParsecParser;
using Models;
using Xunit;

namespace ParserTests
{
    public class VariableParserTest
    {
        [Theory]
        [InlineData("foo")]
        [InlineData(" foo")]
        [InlineData("foo ")]
        [InlineData(" foo ")]
        public void Test_Variable(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new VariableToken("foo"), reply.Result);
        }
        
        [Theory]
        [InlineData("match")]
        [InlineData("while")]
        [InlineData("with")]
        [InlineData("class")]
        [InlineData("extends")]
        [InlineData("if")]
        [InlineData("else")]
        [InlineData("case")]
        [InlineData("def")]
        [InlineData("var")]
        [InlineData("new")]
        [InlineData("overrides")]
        public void Test_Reserved(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.False(reply.IsOk());
        }
    }
}