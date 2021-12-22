using System.Collections.Generic;
using FParsec.CSharp;
using FParsecParser;
using Models;
using Models.Extensions;
using Xunit;

namespace ParserTests
{
    public class NewParserTest
    {
        [Theory]
        [InlineData("new Foo()")]
        [InlineData(" new Foo()")]
        [InlineData("new Foo() ")]
        [InlineData(" new Foo() ")]
        [InlineData("new Foo ( )")]
        [InlineData(" new Foo ( )")]
        [InlineData("new Foo ( ) ")]
        [InlineData(" new Foo ( ) ")]
        public void Test_Instantiation_Empty(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new InstantiationToken(
                    "Foo",
                    new Tokens(new List<Token>().AsValueSemantics())),
                reply.Result);
        }
        
        [Theory]
        [InlineData("new Foo(bar)")]
        [InlineData(" new Foo(bar)")]
        [InlineData("new Foo(bar) ")]
        [InlineData(" new Foo(bar) ")]
        [InlineData("new Foo ( bar )")]
        [InlineData(" new Foo ( bar )")]
        [InlineData("new Foo ( bar ) ")]
        [InlineData(" new Foo ( bar ) ")]
        public void Test_Instantiation_One(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new InstantiationToken(
                    "Foo",
                    new Tokens(new List<Token>{ new VariableToken("bar")}.AsValueSemantics())),
                reply.Result);
        }
        
        [Theory]
        [InlineData("new Foo(bar,baz)")]
        [InlineData(" new Foo(bar,baz)")]
        [InlineData("new Foo(bar,baz) ")]
        [InlineData(" new Foo(bar,baz) ")]
        [InlineData("new Foo ( bar , baz )")]
        [InlineData(" new Foo( bar , baz )")]
        [InlineData("new Foo ( bar , baz ) ")]
        [InlineData(" new Foo ( bar , baz ) ")]
        public void Test_Instantiation_Many(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new InstantiationToken(
                    "Foo",
                    new Tokens(
                        new List<Token> { new VariableToken("bar"), new VariableToken("baz") }.AsValueSemantics())),
                reply.Result);
        }
    }
}