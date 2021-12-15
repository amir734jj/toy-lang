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
        public IToyCompilerBuild WithCodeGen(IToyCodeGen codeGen);
    }
    
    public interface IToyCompilerBuild
    {
        public void Build();
    }
    
    public interface IToyCompiler : IToyCompilerParser
    {
    }
}