// Template generated code from Antlr4BuildTasks.dotnet-antlr v 1.5

using System.IO;
using Antlr4.Runtime;

namespace AntlrParser
{
    public class ErrorListener<TS> : ConsoleErrorListener<TS>
    {
        public bool HadError;

        public override void SyntaxError(TextWriter output, IRecognizer recognizer, TS offendingSymbol, int line,
            int col, string msg, RecognitionException e)
        {
            HadError = true;
            base.SyntaxError(output, recognizer, offendingSymbol, line, col, msg, e);
        }
    }
}
