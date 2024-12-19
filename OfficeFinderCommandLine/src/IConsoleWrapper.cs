
namespace OfficeFinderCommandLine.src
{
    public interface IConsoleWrapper
    {
        ConsoleColor ForegroundColor { get; set; }

        void ResetColor();
        void SetCursorPosition(int left, int top);
        void Write(string text);
        void WriteLine(string text);
        void WriteLine();
    }
}