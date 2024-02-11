namespace OfficeFinder.Inputs.CommandLine;

/// <summary>
/// <inheritdoc cref="IOptionProvider"/>
/// </summary>
public class CommandLineOptionProvider : IOptionProvider{
    public string? Path => _store.GetOption(_store.PATHOPTIONNAME).Value;
    public string? Regex => _store.GetOption(_store.REGEXOPTIONNAME).Value;
    public bool HelpRequired => _store.GetOption(_store.HELPOPTIONNAME).IsPresent;
    private ICommandLineOptionStore _store;
    
    public CommandLineOptionProvider(ICommandLineOptionStore store, ICommandLineOptionReader reader){
        _store = store;
        reader.Init();
    }
}