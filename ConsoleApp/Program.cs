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
            var logger = loggerFactory.CreateLogger<ToyCompiler>();
            
            var compiler = new ToyCompiler(logger)
                .WithParser(new ToyFparsecParser())
                .WithSemantics(new ToyBasicSemantics())
                .WithCodeGen(new ToyJavaScriptCodeGen())
                .Build();

            compiler(File.ReadAllText("basic.toy"));
        }
    }
}