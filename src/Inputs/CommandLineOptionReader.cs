
namespace OfficeFinder.Inputs;

/// <summary>
/// <inheritdoc cref="ICommandLineOPtionReader"/>
/// </summary>
public class CommandLineOPtionReader : ICommandLineOPtionReader
{
    private string []Args;
    private IOptionManager Options;
    public CommandLineOPtionReader(IOptionManager option)
    {
        Options = option;
    }
    public (string regex, List<string> listFile) ReadOption()
    {
        // Retrieve fileName and the pattern for the regex
        string? fileName = Options.File;
        string? pattern = Options.Regex;
        string? directory = Options.Directory;

        // Check if parameters are not null
        if (pattern is null || !(fileName is null ^ directory is null))
        {
            Console.WriteLine("Pour utiliser la commande taper : OfficeFinder <pattern> <file>");
            throw new ArgumentException("Les arguments de la commande ne sont pas correctement saisies");
        }

        //Initialization of list which will contain all the files in which to search
        List<string> ListOfFile = new List<string>();

        if (fileName is not null)
        { // If the command precise to search in one file
            if (File.Exists(fileName) == false)
            {
                Console.WriteLine($"Le fichier {fileName} n'a pas été trouvé");
                throw new FileNotFoundException($"Fichier {fileName} inexistant");
            }
            else
            {
                ListOfFile.Add(fileName);
            }
        }
        else
        { // If the command precise to search all files of one directory
            if (Directory.Exists(directory))
            {
                DirectoryInfo dir = new DirectoryInfo(directory);
                IEnumerable<FileInfo> files = dir.GetFiles("*.docx", SearchOption.AllDirectories);
                foreach (FileInfo file in files)
                {
                    if(file.Name[0] != '~') // Temp file => not readable
                        ListOfFile.Add(file.FullName);
                }
            }
            else{ // If directory doesn't exist
                Console.WriteLine($"Le dossier {directory} n'a pas été trouvé");
                throw new DirectoryNotFoundException($"Dossier {directory} inexistant");
            }
        }

        (string regex, List<string> listFile) result = (pattern, ListOfFile);
        return result;
    }
}