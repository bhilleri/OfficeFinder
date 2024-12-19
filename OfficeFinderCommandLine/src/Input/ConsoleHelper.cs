using OfficeFinderCommandLine.src;
using OfficeFinderLibrary.Inputs;

namespace OfficeFinderCommandLine.Inputs;

/// <summary>
/// <inheritdoc cref="IHelper"/>
/// </summary>
public class ConsoleHelper : IHelper
{
    private const string NAMEOFSOFTWARE = "OfficeFinder";
    private string DESCRIPTIONTITLE = "Description";
    private string DESCRIPTION = $"{NAMEOFSOFTWARE} est un logiciel qui permet de reperer les occurences d'un texte sans avoir à les ouvrir";
    private string OPTIONTITLE = "Options";
    private string EXAMPLETITLE = "Exemples";
    private int MARGE = 5;
    private int INITIALMARGE = 10;
    private Dictionary<string,string> Examples;
    private ICommandLineOptionStore _optionStore;
    private IConsoleWrapper consoleWrapper;

    public ConsoleHelper(ICommandLineOptionStore optionStore, IConsoleWrapper consoleWrapper){
        this.consoleWrapper = consoleWrapper;
        _optionStore = optionStore;
        this.Examples = new Dictionary<string, string>{
            {$"""OfficeFinder {_optionStore.GetOption(_optionStore.REGEXOPTIONNAME)!.OptionCode} "regex" """, """Recherche "regex" dans le répertoire courant""" },
            {$"""OfficeFinder {_optionStore.GetOption(_optionStore.REGEXOPTIONNAME)!.OptionCode} "regex" {_optionStore.GetOption(_optionStore.PATHOPTIONNAME)!.OptionCode} "./Directory" """, """Recherche "regex" dans le répertoire directory""" },
            {$"""OfficeFinder {_optionStore.GetOption(_optionStore.REGEXOPTIONNAME)!.OptionCode} "regex" {_optionStore.GetOption(_optionStore.PATHOPTIONNAME)!.OptionCode} "./file.docx" """, """Recherche "regex" dans le fichier file""" }
        };
    }
 
    public void Help()
    {
        consoleWrapper.WriteLine($"\n{NAMEOFSOFTWARE}");
        consoleWrapper.WriteLine();
        consoleWrapper.WriteLine($"{DESCRIPTIONTITLE} : {DESCRIPTION}");
        consoleWrapper.WriteLine();
        consoleWrapper.WriteLine($"{OPTIONTITLE} : ");

        int maxSizeOptionCode = 0;
        int maxSizeAbbreviationCode = 0;
        foreach(string name in _optionStore.GetNames()){
            CommandLineOption? option = _optionStore.GetOption(name);
            if (option is not null && option.OptionCode.Length > maxSizeOptionCode)
                maxSizeOptionCode = option.OptionCode.Length;
            if (option is not null && option.AbbreviationCode.Length > maxSizeAbbreviationCode)
                maxSizeAbbreviationCode = option.AbbreviationCode.Length;
        }
        foreach(string name in _optionStore.GetNames()){
            CommandLineOption? option = _optionStore.GetOption(name);
            if(option != null)
            {
                int currentMarge = 0;
                SetPointerPosition(currentMarge = this.INITIALMARGE);
                consoleWrapper.Write(option.OptionCode?? "");  
                SetPointerPosition(currentMarge += maxSizeOptionCode + this.MARGE);
                consoleWrapper.Write(option.AbbreviationCode?? "");  
                SetPointerPosition(currentMarge += this.MARGE + maxSizeAbbreviationCode);
                consoleWrapper.WriteLine(option.Description);
            }
        }
        consoleWrapper.WriteLine($"{this.EXAMPLETITLE}\n");

        int maxSizeExample = 0;
        foreach(KeyValuePair<string, string> example in this.Examples){
            if(example.Key.Length > maxSizeExample){
                maxSizeExample = example.Key.Length;
            }
        }
        foreach(KeyValuePair<string, string> example in this.Examples){
                int currentMarge = 0;
                SetPointerPosition(currentMarge +=INITIALMARGE);
                consoleWrapper.Write($""" > {example.Key}""");  
                SetPointerPosition(currentMarge += maxSizeExample + this.MARGE);
                consoleWrapper.Write(example.Value);
            consoleWrapper.Write("\n");
        }

    }

    private void SetPointerPosition(int value){
        try{
            consoleWrapper.SetCursorPosition(value, Console.CursorTop);
        }
        catch(ArgumentOutOfRangeException){
            consoleWrapper.Write("\t");
        }
        catch(IOException){
            consoleWrapper.Write("\t");
        }
    }
}