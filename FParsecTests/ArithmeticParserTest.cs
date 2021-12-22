using FParsec.CSharp;
using FParsecParser;
using Models;
using Xunit;

namespace ParserTests
{
    public class ArithmeticParserTest
    {
        [Theory]
        [InlineData("1+2*3-1/-3")]
        [InlineData(" 1+2*3-1/-3")]
        [InlineData("1+2*3-1/-3 ")]
        [InlineData(" 1+2*3-1/-3 ")]
        [InlineData(" 1 + 2 * 3 - 1 / - 3 ")]
        [InlineData(" ((1 + (2 * 3)) - (1 / (- 3))) ")]
        public void Test_Operation(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(
                new SubtractToken(
                    new AddToken(
                        new AtomicToken(1),
                        new MultiplyToken(new AtomicToken(2), new AtomicToken(3))),
                    new DivideToken(new AtomicToken(1), new NegateToken(new AtomicToken(3)))), reply.Result);
        }
    }
}