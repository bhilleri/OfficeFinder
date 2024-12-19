namespace OfficeFinderCommandLine.Inputs;

using OfficeFinderCommandLine.src;
using OfficeFinderLibrary.Inputs;

/// <summary>
/// <inheritdoc cref="IInputErrorManager"/>
/// </summary>
public class CommandLineInputErrorManager : IInputErrorManager
{
    ICommandLineOptionStore _optionStore;
    IConsoleWrapper consoleWrapper;
    public CommandLineInputErrorManager(ICommandLineOptionStore optionStore, IConsoleWrapper consoleWrapper)
    {
        this.consoleWrapper = consoleWrapper;
        _optionStore = optionStore;
    }
    public void FileNotFound(string path)
    {
        PrintError($"Le fichier {path} n'a pas été trouvé");
    }

    public void PathMissing()
    {
        PrintError($"L'option {_optionStore.GetOption(_optionStore.PATHOPTIONNAME).OptionCode} ou son argument est manquant");
    }

    public void RegexIsMissing()
    {
        PrintError($"L'option {_optionStore.GetOption(_optionStore.REGEXOPTIONNAME).OptionCode} ou son argument est manquant");
    }

    private void PrintError(string errorMessage){
        consoleWrapper.ForegroundColor = ConsoleColor.Red;
        consoleWrapper.WriteLine(errorMessage);
        consoleWrapper.ResetColor();
    }
}