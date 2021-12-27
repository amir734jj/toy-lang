using System.IO;

namespace Models.Interfaces
{
    public interface IToyParser
    {
        void Parse(CompilerPayload compilerPayload);
    }
}