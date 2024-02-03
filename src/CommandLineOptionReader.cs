
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
        { "--path", "path" },
        { "-p", "pathAbbreviation"},
    };
        var builder = new ConfigurationBuilder();
        builder.AddCommandLine(args, switchMappings);

        var config = builder.Build();

        // Retrieve fileName and the pattern for the regex
        string? fileName = config["path"] ?? config["pathAbbreviation"];
        string? pattern = config["regex"] ?? config["regexAbbreviation"];

        // Check if parameters are not null
        if (pattern is null || fileName is null)
        {
            Console.WriteLine("Pour utiliser la commande taper : OfficeFinder <pattern> <file>");
            throw new ArgumentException("Les arguments de la commande ne sont pas correctement saisies");
        }


        // Check if file exist
        if (File.Exists(fileName) == false)
        {
            Console.WriteLine($"Le fichier {fileName} n'a pas été trouvé");
            throw new FileNotFoundException($"Fichier {fileName} inexistant");
        }
        (string regex, List<string> listFile) result= (pattern, new List<string>(){
            fileName
        });

        return result;
    }
}