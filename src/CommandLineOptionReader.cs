
using Microsoft.Extensions.Configuration;

namespace OfficeFinder;

/// <summary>
/// <inheritdoc cref="ICommandLineOPtionReader"/>
/// </summary>
public class CommandLineOPtionReader : ICommandLineOPtionReader
{
    public (string regex, List<string> listFile) ReadOption(string[] args)
    {
        var switchMappings = new Dictionary<string, string>()
    {
        { "--regex", "regex" },
        { "-r", "regexAbbreviation"},
        { "--file", "file" },
        { "-f", "fileAbbreviation"},
        { "--directory", "Directory" },
        { "-d", "DirectoryAbbreviation"},
    };
        var builder = new ConfigurationBuilder();
        builder.AddCommandLine(args, switchMappings);

        var config = builder.Build();

        // Retrieve fileName and the pattern for the regex
        string? fileName = config["file"] ?? config["fileAbbreviation"];
        string? pattern = config["regex"] ?? config["regexAbbreviation"];
        string? directory = config["directory"] ?? config["directoryAbbreviation"];

        // Check if parameters are not null
        if (pattern is null || !(fileName is null ^ directory is null))
        {
            Console.WriteLine("Pour utiliser la commande taper : OfficeFinder <pattern> <file>");
            throw new ArgumentException("Les arguments de la commande ne sont pas correctement saisies");
        }

        List<string> ListOfFile = new List<string>();

        if (fileName is not null)
        {
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
        {
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
            else{
                Console.WriteLine($"Le dossier {directory} n'a pas été trouvé");
                throw new DirectoryNotFoundException($"Dossier {directory} inexistant");
            }
        }

        // Check if file exist

        (string regex, List<string> listFile) result = (pattern, ListOfFile);

        return result;
    }
}