using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFinderCommandLine.src
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }

        aaa
        public void ResetColor()
        {
            Console.ResetColor();
        }

        public void WriteLine(string text) => Console.WriteLine(text);
        public void Write(string text) => Console.Write(text);
        public void SetCursorPosition(int left, int top) => Console.SetCursorPosition(left, top);

        public void WriteLine() => Console.WriteLine();
    }
}
