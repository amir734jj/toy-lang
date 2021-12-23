using System.Reflection;
using AntlrParser;
using AstJsonDump;
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
                .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Error)
                .Scan(x => x.FromAssemblies(
                    Assembly.Load("Core"),
                    Assembly.Load("FParsecParser"),
                    Assembly.Load("AntlrParser"),
                    Assembly.Load("Semantics"),
                    Assembly.Load("JavaScriptCodeGen"),
                    Assembly.Load("AstJsonDump")))
                .BuildServiceProvider();

            var compiler = serviceProvider.GetRequiredService<ToyCompiler>()
                .WithParser(serviceProvider.GetService<ToyAntlrParser>())
                .WithSemantics(serviceProvider.GetService<ToyBasicSemantics>())
                .WithAstDump(serviceProvider.GetService<ToyJavaScriptCodeGen>())
                .Build();

            compiler("class hello() { }");
        }
    }
}