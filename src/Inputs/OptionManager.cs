using Microsoft.Extensions.Logging;
using OfficeFinder.Inputs.CommandLine;
using OfficeFinder.Inputs.FilesTools;

namespace OfficeFinder.Inputs;

/// <summary>
/// <inheritdoc cref="IOptionManager"/>
/// </summary>
public class OptionManger : IOptionManager
{
    private readonly IInputErrorManager ErrorManager;
    private readonly IFileFinderManager FileSearcher;
    private readonly IOptionProvider Options;
    private readonly ILogger Logger;
    private readonly IHelper Helper;
    private const string ARGUMENTISMISSING = "Information(s) manquante(s) pour éxécuter le code, nécessite au minimum le path et la regex";
    public OptionManger(IOptionProvider option, IFileFinderManager fileSearcher, ILogger<OptionManger> logger, IInputErrorManager errorManager, IHelper helper)
    {
        this.Options = option;
        this.FileSearcher = fileSearcher;
        this.Logger = logger;
        this.ErrorManager = errorManager;
        this.Helper = helper;
    }
    public (string regex, List<string> listFile) ReadOption()
    {
        // Retrieve fileName and the pattern for the regex
        string? path = Options.Path;
        string? regex = Options.Regex;
        bool help = Options.HelpRequired;

        if(help == true){
            Helper.Help();
            return (null, null);
        }
        // Check if parameters are not null
        if (regex is null || path is null )
        {
            Logger.LogError($"""{ARGUMENTISMISSING} : {(regex is null ? nameof(regex) : String.Empty)} {(path is null ? nameof(path) :  String.Empty)}""");
            if(path is null)
                this.ErrorManager.PathMissing();
            if (regex is null)
                this.ErrorManager.RegexIsMissing();
            Helper.Help();
            throw new ArgumentException(ARGUMENTISMISSING);
        }
        // Find files
        List<string> ListOfFile = FileSearcher.GetFiles(path);

        (string regex, List<string> listFile) result = (regex, ListOfFile);
        return result;
    }
}