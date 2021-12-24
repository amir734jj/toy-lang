using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Models;
using Models.Interfaces;

namespace JavaScriptCodeGen
{
    public class ToyJavaScriptCodeGen : IAstDump
    {
        private readonly ILogger<ToyJavaScriptCodeGen> _logger;

        public ToyJavaScriptCodeGen(ILogger<ToyJavaScriptCodeGen> logger)
        {
            _logger = logger;
        }
        
        public void CodeGen(Classes classes)
        {
            var visitor = new JavaScriptCodeGenVisitor();

            var result = File.ReadAllText("basic.js") + visitor.Visit(classes);
            
            _logger.LogInformation(result);
        }
    }
}