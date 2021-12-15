using FParsec.CSharp;
using Models;
using Models.Interfaces;

namespace FParsecParser
{
    public class ToyFparsecParser : IToyParser
    {
        public Classes Parse(string text)
        {
            return Parser.Classes().ParseString(text).Result;
        }
    }
}