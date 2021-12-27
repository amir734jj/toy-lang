using System;
using Models;
using Models.Interfaces;

namespace Core.Interfaces
{
    public interface IToyCompilerParser
    {
        public IToyCompilerSemantic WithParser(IToyParser parser);
    }

    public interface IToyCompilerSemantic
    {
        public IToyCompilerCodeGen WithSemantics(IToySemantics semantics);
    }
    
    public interface IToyCompilerCodeGen
    {
        public IToyCompilerBuild WithAstDump(IAstDump codeGen);
    }
    
    public interface IToyCompilerBuild
    {
        public Action<CompilerPayload> Build();
    }
}