using FParsec.CSharp;
using FParsecParser;
using Models;
using Xunit;
using static Models.Constants;

namespace ParserTests
{
    public class AtomicParserTest
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
        public void Test_Unit(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new AtomicToken(UNIT_SYMBOL_VALUE), reply.Result);
        }
        
        [Theory]
        [InlineData("null")]
        [InlineData(" null")]
        [InlineData("null ")]
        [InlineData(" null ")]
        public void Test_Null(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new AtomicToken(null), reply.Result);
        }

        [Theory]
        [InlineData("true")]
        [InlineData(" true")]
        [InlineData("true ")]
        [InlineData(" true ")]
        public void Test_Boolean_True(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new AtomicToken(true), reply.Result);
        }

        [Theory]
        [InlineData("false")]
        [InlineData(" false")]
        [InlineData("false ")]
        [InlineData(" false ")]
        public void Test_Boolean_False(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new AtomicToken(false), reply.Result);
        }

        [Theory]
        [InlineData("123456789")]
        [InlineData(" 123456789")]
        [InlineData("123456789 ")]
        [InlineData(" 123456789 ")]
        public void Test_Number(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new AtomicToken(123456789), reply.Result);
        }

        [Theory]
        [InlineData(@""" Hello world! \r\n\"" """)]
        [InlineData(@" "" Hello world! \r\n\"" """)]
        [InlineData(@""" Hello world! \r\n\"" "" ")]
        [InlineData(@" "" Hello world! \r\n\"" "" ")]
        public void Test_String(string text)
        {
            // ActFunctionTest
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new AtomicToken(@" Hello world! \r\n\"" "), reply.Result);
        }
    }
}
