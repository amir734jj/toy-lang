using Models;

namespace Core.Interfaces
{
    public interface IToyParser
    {
        Classes Parse(string text);
    }
}