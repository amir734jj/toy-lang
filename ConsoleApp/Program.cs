using System.IO;
using AntlrParser;
using Core;
using FParsecParser;
using JavaScriptCodeGen;
using Microsoft.Extensions.Logging;
using Semantics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(x => x.AddConsole());

            var compiler = new ToyCompiler(loggerFactory.CreateLogger<ToyCompiler>())
                .WithParser(new ToyFparsecParser())
                .WithSemantics(new ToyBasicSemantics(loggerFactory.CreateLogger<ToyBasicSemantics>()))
                .WithCodeGen(new ToyJavaScriptCodeGen())
                .Build();

            compiler(File.ReadAllText("basic.toy"));
        }
    }
}