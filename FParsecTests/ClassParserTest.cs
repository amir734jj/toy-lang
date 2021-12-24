using System.Collections.Generic;
using FParsec.CSharp;
using FParsecParser;
using Models;
using Models.Extensions;
using Xunit;
using static Models.Constants;

namespace ParserTests
{
    public class ClassParserTest
    {
        [Theory]
        [InlineData("class Foo() extends native { }")]
        public void Test_ExtendsNative( string text)
        {
            // Act
            var reply = Parser.Class().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(
                new ClassToken("Foo", new Formals(new List<Formal>().AsValueSemantics()), NOTHING_TYPE,
                    new Tokens(new List<IToken>().AsValueSemantics()), new Tokens(new List<IToken>().AsValueSemantics()))
                , reply.Result);
        }
        
        [Theory]
        [InlineData("class Foo() extends Bar() { }")]
        public void Test_Extends_EmptyFormals_EmptyActuals( string text)
        {
            // Act
            var reply = Parser.Class().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(
                new ClassToken("Foo", new Formals(new List<Formal>().AsValueSemantics()), "Bar",
                    new Tokens(new List<IToken>().AsValueSemantics()), new Tokens(new List<IToken>().AsValueSemantics()))
                , reply.Result);
        }

        [Theory]
        [InlineData("class Foo() { }")]
        public void Test_ExtendsNone_EmptyFormals( string text)
        {
            // Act
            var reply = Parser.Class().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(
                new ClassToken("Foo", new Formals(new List<Formal>().AsValueSemantics()), ANY_TYPE,
                    new Tokens(new List<IToken>().AsValueSemantics()), new Tokens(new List<IToken>().AsValueSemantics()))
                , reply.Result);
        }
        
        [Theory]
        [InlineData("class Foo(baz: Int) extends Bar(baz) { }")]
        public void Test_One_Formal( string text)
        {
            // Act
            var reply = Parser.Class().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(
                new ClassToken("Foo", new Formals(new List<Formal>{new("baz", "Int")}.AsValueSemantics()), "Bar",
                    new Tokens(new List<IToken>{new VariableToken("baz")}.AsValueSemantics()), new Tokens(new List<IToken>().AsValueSemantics()))
                , reply.Result);
        }
    }
}