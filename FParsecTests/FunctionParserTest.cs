using System.Collections.Generic;
using FParsec.CSharp;
using FParsecParser;
using Models;
using Models.Extensions;
using Xunit;

namespace ParserTests
{
    public class FunctionParserTest
    {
        [Theory]
        [InlineData("def foo():Unit=bar")]
        [InlineData(" def foo():Unit=bar")]
        [InlineData("def foo():Unit=bar ")]
        [InlineData(" def foo():Unit=bar ")]
        [InlineData("def foo() : Unit = bar")]
        [InlineData(" def foo() : Unit = bar")]
        [InlineData("def foo() : Unit = bar ")]
        [InlineData(" def foo() : Unit = bar ")]
        public void Test_Function_Empty(string text)
        {
            // Act
            var reply = Parser.Function().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(
                new FunctionDeclToken(false, "foo", new Formals(new List<Formal>().AsValueSemantics()), "Unit",
                    new VariableToken("bar")), reply.Result);
        }

        [Theory]
        [InlineData("def foo(baz:Qux):Unit=bar")]
        [InlineData(" def foo(baz:Qux):Unit=bar")]
        [InlineData("def foo(baz:Qux):Unit=bar ")]
        [InlineData(" def foo(baz:Qux):Unit=bar ")]
        [InlineData("def foo(baz : Qux) : Unit = bar")]
        [InlineData(" def foo(baz : Qux): Unit = bar")]
        [InlineData("def foo(baz : Qux) : Unit= bar ")]
        [InlineData(" def foo(baz : Qux): Unit = bar ")]
        public void Test_Function_One(string text)
        {
            // Act
            var reply = Parser.Function().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(
                new FunctionDeclToken(false, "foo",
                    new Formals(new List<Formal> { new("baz", "Qux") }.AsValueSemantics()), "Unit",
                    new VariableToken("bar")), reply.Result);
        }

        [Theory]
        [InlineData("def foo(baz:Qux,taz:Sux):Unit=bar")]
        [InlineData(" def foo(baz:Qux,taz:Sux):Unit=bar")]
        [InlineData("def foo(baz:Qux,taz:Sux):Unit=bar ")]
        [InlineData(" def foo(baz:Qux,taz:Sux):Unit=bar ")]
        [InlineData("def foo(baz : Qux , taz : Sux) : Unit = bar")]
        [InlineData(" def foo(baz : Qux , taz : Sux) : Unit = bar")]
        [InlineData("def foo(baz : Qux , taz : Sux) : Unit = bar ")]
        [InlineData(" def foo(baz : Qux , taz : Sux) : Unit = bar ")]
        public void Test_Function_Many(string text)
        {
            // Act
            var reply = Parser.Function().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(
                new FunctionDeclToken(false, "foo",
                    new Formals(
                        new List<Formal> { new("baz", "Qux"), new("taz", "Sux") }.AsValueSemantics()),
                    "Unit",
                    new VariableToken("bar")), reply.Result);
        }
    }
}