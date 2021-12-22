using FParsec.CSharp;
using FParsecParser;
using Models;
using Xunit;

namespace ParserTests
{
    public class CommentParserTest
    {
        [Theory]
        [InlineData("// hello world!")]
        [InlineData(" // hello world!")]
        [InlineData("// hello world! ")]
        [InlineData(" // hello world! ")]
        public void Test_SingleLine( string text)
        {
            // Act
            var reply = Parser.Comment().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new CommentToken("hello world!"), reply.Result);
        }
        
        [Theory]
        [InlineData("/** hello \n world! **/")]
        [InlineData(" /** hello \n world! **/")]
        [InlineData("/** hello \n world! **/ ")]
        [InlineData(" /** hello \n world! **/ ")]
        public void Test_MultiLine( string text)
        {
            // Act
            var reply = Parser.Comment().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new CommentToken("* hello \n world! *"), reply.Result);
        }
    }
}