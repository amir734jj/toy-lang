using System.IO;
using Core;
using FParsecParser;
using JavaScriptCodeGen;
using Semantics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var compiler = new ToyCompiler()
                .WithParser(new ToyFparsecParser())
                .WithSemantics(new ToyBasicSemantics())
                .WithCodeGen(new ToyJavaScriptCodeGen())
                .Build();

            compiler(File.ReadAllText("basic.toy"));
        }
    }
}