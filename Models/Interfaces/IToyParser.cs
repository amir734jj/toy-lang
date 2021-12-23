using System.IO;

namespace Models.Interfaces
{
    public interface IToyParser
    {
        Classes Parse(Stream text);
    }
}