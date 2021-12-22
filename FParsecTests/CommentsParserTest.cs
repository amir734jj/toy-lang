using System.Collections.Generic;
using FParsec.CSharp;
using FParsecParser;
using Models;
using Models.Extensions;
using Xunit;

namespace ParserTests
{
    public class CommentsParserTest
    {
        [Theory]
        [InlineData("/** hello \n world! **/ //hello world!")]
        [InlineData("/** hello \n world! **/ //hello world! ")]
        [InlineData(" /** hello \n world! **/ //hello world!")]
        [InlineData(" /** hello \n world! **/ //hello world! ")]
        public void Test_Multiple( string text)
        {
            // Act
            var reply = Parser.Comments().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new CommentsToken(new List<CommentToken>
            {
                new("* hello \n world! *"),
                new("hello world!")
            }.AsValueSemantics()), reply.Result);
        }
    }
}