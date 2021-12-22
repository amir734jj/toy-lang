using System.IO;
using System.Reflection;
using AntlrParser;
using Core;
using JavaScriptCodeGen;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Semantics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(cfg => cfg.AddConsole())
                .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Trace)
                .Scan(x => x.FromAssemblies(
                    Assembly.Load("FParsec"),
                    Assembly.Load("AntlrParser"),
                    Assembly.Load("Semantics"),
                    Assembly.Load("JavaScriptCodeGen")))
                .BuildServiceProvider();

            var compiler = new ToyCompiler()
                .WithParser(serviceProvider.GetService<ToyAntlrParser>())
                .WithSemantics(serviceProvider.GetService<ToyBasicSemantics>())
                .WithCodeGen(serviceProvider.GetService<ToyJavaScriptCodeGen>())
                .Build();

            compiler(File.ReadAllText("basic.toy"));
        }
    }
}