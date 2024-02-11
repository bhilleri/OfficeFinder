
using Castle.Core.Logging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Logging;

namespace OfficeFinder.Inputs.CommandLine;

/// <summary>
/// <inheritdoc cref="ICommandLineOptionStore"/>
/// </summary>
public class CommandLineOptionStore : ICommandLineOptionStore
{
    private const string _PATHOPTIONNAME = "path";
    public string PATHOPTIONNAME => _PATHOPTIONNAME;
    private const string _REGEXOPTIONNAME = "Regex";
    public string REGEXOPTIONNAME => _REGEXOPTIONNAME;
    public const string _HELPOPTIONNAME = "Help";
    public string HELPOPTIONNAME => "Help";
    private ILogger<CommandLineOptionStore> _logger;
    public CommandLineOptionStore(ILogger<CommandLineOptionStore> logger){
        _logger = logger;
        this._listOptions = new List<CommandLineOption>();
        this._listOptions!.Add(new CommandLineOption(
            optionName: _PATHOPTIONNAME,
            description: "permet de définir le chemin vers le fichier ou le dossier. valeur par défaut => répertoire courant",
            abbreviationCode: "-p",
            optionCode: "--path",
            needed: false,
            defaultValue: "./",
            NeedArg: true
        ));
        this._listOptions!.Add(new CommandLineOption(
            optionName: _REGEXOPTIONNAME,
            description: "Définition de l'expression régulière à rechercher",
            abbreviationCode: "-r",
            optionCode: "--regex",
            needed: true,
            NeedArg:true
        ));
        this._listOptions!.Add(new CommandLineOption(
            optionName: _HELPOPTIONNAME,
            description: "Affiche l'aide",
            abbreviationCode: "-h",
            optionCode: "--help",
            needed: false,
            NeedArg: false
        ));
    }
    private List<CommandLineOption> _listOptions;
    public string[] GetNames()
    {
        string[] names = new string[_listOptions.Count];
        for(int i = 0; i < _listOptions.Count; i++){
            names[i] = _listOptions[i].OptionName;
        }
        return names;
    }

    public CommandLineOption GetOption(string name)
    {
        CommandLineOption result = null;
        foreach(CommandLineOption option in _listOptions){
            if(name == option.OptionName){
                result = option;
                break;
            }
        }
        if (result == null){
            string errorMessage = $"L'option {name} n'est pas définie dans la liste des options disponibles";
            _logger.LogError(errorMessage);
            throw new ArgumentNullException(errorMessage);
        }
        return result;
    }

    public Dictionary<string, string> GetOptionForConsole()
    {
        Dictionary<string, string> allOptionForConsole = new Dictionary<string, string>();

        foreach(CommandLineOption option in this._listOptions){

            allOptionForConsole.Add(option.AbbreviationCode, option.OptionName);
            allOptionForConsole.Add(option.OptionCode, option.OptionName);
        }
        return allOptionForConsole;
    }
}