using Microsoft.Extensions.DependencyInjection;

namespace OfficeFinder;

public class ProgramManager : IProgramManager
{
    private readonly IWordSearcher WordSearcher;
    private readonly ICommandLineOPtionReader CommandInterpreter;
    public ProgramManager(IServiceProvider hostProvider){
        this.WordSearcher = hostProvider.GetRequiredService<IWordSearcher>()!;
        this.CommandInterpreter = hostProvider.GetRequiredService<ICommandLineOPtionReader>()!;
    }
    public void Start(string[] args)
    {
        (string regex, List<string> listFile) patternListFile = CommandInterpreter.ReadOption(args);
        foreach(string filePath in patternListFile.listFile){
            try{
                KeyValuePair<string, int> NumberOfOccurence = WordSearcher.Search(patternListFile.regex, filePath);
                Console.WriteLine($"{NumberOfOccurence.Key} : {NumberOfOccurence.Value}");
            }catch(System.IO.IOException ex){
                ConsoleColor defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erreur sur la lecture du fichier {filePath}, assurez vous que le fichier n'est pas ouvert. exception : {ex.Message}");
                Console.ForegroundColor = defaultColor;
            }
            catch(System.IO.FileFormatException ex){
                ConsoleColor defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erreur sur la lecture du fichier {filePath}, le fichier semble corrompue (Le fichier est-il correctement enregistré ?). exception : {ex.Message}");
                Console.ForegroundColor = defaultColor;
            }
        }
    }
}