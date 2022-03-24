using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Models;
using Models.Interfaces;

namespace JavaScriptCodeGen
{
    public class ToyJavaScriptCodeGen : IAstDump
    {
        private readonly ILogger<ToyJavaScriptCodeGen> _logger;

        private static readonly string BasicFileText = File.ReadAllText(Path.Join(Directory.GetParent(Assembly.GetAssembly(typeof(ToyJavaScriptCodeGen)).Location).FullName, "basic.js"));

        public ToyJavaScriptCodeGen(ILogger<ToyJavaScriptCodeGen> logger)
        {
            _logger = logger;
        }
        
        public void CodeGen(CompilerPayload compilerPayload)
        {
            var visitor = new JavaScriptCodeGenVisitor();

            var result = BasicFileText + visitor.AsVisitor().Visit(compilerPayload.Ast);

            _logger.LogInformation(result);

            compilerPayload.Result = result;
        }
    }
}