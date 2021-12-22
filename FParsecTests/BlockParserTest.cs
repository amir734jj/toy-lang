using System.Collections.Generic;
using FParsec.CSharp;
using FParsecParser;
using Models;
using Models.Extensions;
using Xunit;

namespace ParserTests
{
    public class BlockParserTest
    {
        [Theory]
        [InlineData("{}")]
        [InlineData(" {}")]
        [InlineData("{} ")]
        [InlineData(" {} ")]
        [InlineData("{ }")]
        [InlineData(" { }")]
        [InlineData("{ } ")]
        [InlineData(" { } ")]
        public void Test_Block_Empty(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            var blockToken = Assert.IsType<BlockToken>(reply.Result);
            Assert.Empty(blockToken.Tokens.Inner);
        }

        [Theory]
        [InlineData("{foo}")]
        [InlineData(" {foo}")]
        [InlineData("{foo} ")]
        [InlineData(" {foo} ")]
        [InlineData("{ foo }")]
        [InlineData(" { foo }")]
        [InlineData("{ foo } ")]
        [InlineData(" { foo } ")]
        public void Test_Block_One(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            var blockToken = Assert.IsType<BlockToken>(reply.Result);
            Assert.Collection(blockToken.Tokens.Inner, token => { Assert.Equal(new VariableToken("foo"), token); });
        }

        [Theory]
        [InlineData("{foo ; bar}")]
        [InlineData(" {foo ; bar}")]
        [InlineData("{foo ; bar} ")]
        [InlineData(" {foo ; bar} ")]
        [InlineData("{ foo ; bar }")]
        [InlineData(" { foo ; bar }")]
        [InlineData("{ foo ; bar } ")]
        [InlineData(" { foo ; bar } ")]
        public void Test_Block_Many(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            var blockToken = Assert.IsType<BlockToken>(reply.Result);
            Assert.Equal(
                new Tokens(new List<Token> { new VariableToken("foo"), new VariableToken("bar") }.AsValueSemantics()),
                blockToken.Tokens);
        }
    }
}