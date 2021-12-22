using FParsec.CSharp;
using FParsecParser;
using Models;
using Xunit;

namespace ParserTests
{
    public class FormalParserTest
    {
        [Theory]
        [InlineData("foo:Bar")]
        [InlineData(" foo:Bar")]
        [InlineData("foo:Bar ")]
        [InlineData(" foo:Bar ")]
        [InlineData("foo : Bar")]
        [InlineData(" foo : Bar")]
        [InlineData("foo : Bar ")]
        [InlineData(" foo : Bar ")]
        public void Test_Formal(string text)
        {
            // Act
            var reply = Parser.Formal().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new Formal("foo", "Bar"), reply.Result);
        }
    }
}