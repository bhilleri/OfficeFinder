namespace OfficeFinderCommandLine.Inputs;

/// <summary>
/// <inheritdoc cref="ICommandLineArgs"/>
/// </summary>
public class CommandLineArgs : ICommandLineArgs
{
    public CommandLineArgs(string[] args){
        this._args = args;
    }
    private string[] _args;
    public string[] Args => _args;
}