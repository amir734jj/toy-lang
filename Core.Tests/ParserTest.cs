using System.IO;
using AntlrParser;
using FParsecParser;
using Microsoft.Extensions.Logging.Abstractions;
using Models;
using Xunit;

namespace Core.Tests
{
    public class ParserTest
    {
        [Fact]
        public void Test_Basic()
        {
            // Arrange
            var p1 = new CompilerPayload { Stream = new MemoryStream(ToyCompiler.BasicFileText) };
            var p2 = new CompilerPayload { Stream = new MemoryStream(ToyCompiler.BasicFileText) };
            
            // Act
            new ToyAntlrParser(NullLogger<ToyAntlrParser>.Instance)
                .Parse(p1);
            new ToyFparsecParser(NullLogger<ToyFparsecParser>.Instance)
                .Parse(p2);
            
            // Assert
            Assert.Equal(p1, p2);
        }
    }
}