using Core.Interfaces;

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

        public void Build()
        {
            
        }
    }
}