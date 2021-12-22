using FParsec.CSharp;
using FParsecParser;
using Models;
using Xunit;

namespace ParserTests
{
    public class AssignmentParserTest
    {
        [Theory]
        [InlineData("foo = null")]
        [InlineData(" foo = null")]
        [InlineData("foo = null ")]
        [InlineData(" foo = null ")]
        public void Test_Assignment(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new AssignToken("foo", new AtomicToken(null)), reply.Result);
        }
    }
}