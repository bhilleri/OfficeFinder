using OfficeFinder.Inputs;

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

    public ConsoleHelper(ICommandLineOptionStore optionStore){
        _optionStore = optionStore;
        this.Examples = new Dictionary<string, string>{
            {$"""OfficeFinder {_optionStore.GetOption(_optionStore.REGEXOPTIONNAME)!.OptionCode} "regex" """, """Recherche "regex" dans le répertoire courant""" },
            {$"""OfficeFinder {_optionStore.GetOption(_optionStore.REGEXOPTIONNAME)!.OptionCode} "regex" {_optionStore.GetOption(_optionStore.PATHOPTIONNAME)!.OptionCode} "./Directory" """, """Recherche "regex" dans le répertoire directory""" },
            {$"""OfficeFinder {_optionStore.GetOption(_optionStore.REGEXOPTIONNAME)!.OptionCode} "regex" {_optionStore.GetOption(_optionStore.PATHOPTIONNAME)!.OptionCode} "./file.docx" """, """Recherche "regex" dans le fichier file""" }
        };
    }
 
    public void Help()
    {
        Console.WriteLine($"\n{NAMEOFSOFTWARE}");
        Console.WriteLine();
        Console.WriteLine($"{DESCRIPTIONTITLE} : {DESCRIPTION}");
        Console.WriteLine();
        Console.WriteLine($"{OPTIONTITLE} : ");

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
                Console.Write(option.OptionCode?? "");  
                SetPointerPosition(currentMarge += maxSizeOptionCode + this.MARGE);
                Console.Write(option.AbbreviationCode?? "");  
                SetPointerPosition(currentMarge += this.MARGE + maxSizeAbbreviationCode);
                Console.WriteLine(option.Description);
            }
        }
        Console.WriteLine($"{this.EXAMPLETITLE}\n");

        int maxSizeExample = 0;
        foreach(KeyValuePair<string, string> example in this.Examples){
            if(example.Key.Length > maxSizeExample){
                maxSizeExample = example.Key.Length;
            }
        }
        foreach(KeyValuePair<string, string> example in this.Examples){
                int currentMarge = 0;
                SetPointerPosition(currentMarge +=INITIALMARGE);
                Console.Write($""" > {example.Key}""");  
                SetPointerPosition(currentMarge += maxSizeExample + this.MARGE);
                Console.Write(example.Value);
            Console.Write("\n");
        }

    }

    private void SetPointerPosition(int value){
        try{
            Console.SetCursorPosition(value, Console.CursorTop);
        }
        catch(ArgumentOutOfRangeException){
            Console.Write("\t");
        }
        catch(IOException){
            Console.Write("\t");
        }
    }
}