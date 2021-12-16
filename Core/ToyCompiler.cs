using System;
using Core.Interfaces;
using Models.Interfaces;

namespace Core
{
    public class ToyCompiler : IToyCompilerParser, IToyCompilerSemantic, IToyCompilerCodeGen, IToyCompilerBuild
    {
        public IToyCompilerSemantic WithParser(IToyParser parser)
        {
            return this;
        }

        public IToyCompilerCodeGen WithSemantics(IToySemantics semantics)
        {
            return this;
        }

        public IToyCompilerBuild WithCodeGen(IToyCodeGen codeGen)
        {
            return this;
        }

        public Action<string> Build()
        {
            return s => { };
        }
    }
}