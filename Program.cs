namespace OfficeFinder;

// Permet de rechercher une regex dans un fichier
// Taper OfficeFinder --regex <pattern> --path <File>
public class Program
{
    public static void Main(string[] args)
    {
        ICommandLineOPtionReader CommandInterpreter = new CommandLineOPtionReader();
        (string regex, List<string> listFile) patternListFile = CommandInterpreter.ReadOption(args);
        foreach(string filePath in patternListFile.listFile){
            IWordSearcher wordSearcher = new WordSearcher(filePath);
            try{
                KeyValuePair<string, int> NumberOfOccurence = wordSearcher.Search(patternListFile.regex);
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


