using System.Collections.Generic;
using FParsec.CSharp;
using FParsecParser;
using Models;
using Models.Extensions;
using Xunit;

namespace ParserTests
{
    public class MatchParserTest
    {
        [Theory]
        [InlineData("match foo with { case null => null }")]
        public void Test_Atomic(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new Match(new VariableToken("foo"), new Arms(new List<ArmToken>
            {
                new NullArmToken(new AtomicToken(null))
            }.AsValueSemantics())), reply.Result);
        }
    }
}