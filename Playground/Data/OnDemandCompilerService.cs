using System;
using Core;
using FParsecParser;
using JavaScriptCodeGen;
using Models;
using Semantics;

namespace Playground.Data
{
    public class OnDemandCompilerService
    {
        private readonly Action<CompilerPayload> _compiler;

        public OnDemandCompilerService(
            ToyCompiler toyCompiler,
            ToyJavaScriptCodeGen javaScriptCodeGen,
            ToyBasicSemantics basicSemantics,
            ToyFparsecParser fparsecParser)
        {
            _compiler = toyCompiler.WithParser(fparsecParser).WithSemantics(basicSemantics)
                .WithAstDump(javaScriptCodeGen).Build();
        }
        
        public void Compile(CompilerPayload compilerPayload)
        {
            try
            {
                _compiler(compilerPayload);
            }
            catch (Exception e)
            {
                // ignored
                Console.WriteLine(e);
            }
        } 
    }
}
