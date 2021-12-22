using FParsec.CSharp;
using FParsecParser;
using Models;
using Xunit;

namespace ParserTests
{
    public class DeclarationParserTest
    {
        [Theory]
        [InlineData("var foo: Bar = baz")]
        [InlineData(" var foo: Bar = baz")]
        [InlineData("var foo: Bar = baz ")]
        [InlineData(" var foo: Bar = baz ")]
        public void Test_Declaration(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new VarDeclToken("foo", "Bar", new VariableToken("baz")), reply.Result);
        }
    }
}