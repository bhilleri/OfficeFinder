using System.IO.Enumeration;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;

// Permet de rechercher une regex dans un fichier
// Taper OfficeFinder --regex <pattern> --path <File>
public class Program
{
    public static void Main(string[] args)
    {
        // Get arguments
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

        // Search on the word file
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(fileName, false))
        {

            if (wordDoc is null)
            {
                throw new ArgumentNullException(nameof(wordDoc));
            }

            MainDocumentPart mainDocument = wordDoc.MainDocumentPart ?? wordDoc.AddMainDocumentPart();
            Body body = mainDocument.Document.Body ?? new Body();

            RegexOptions options = RegexOptions.Multiline;
            MatchCollection AllMatchs = Regex.Matches(body.InnerText, pattern, options);
            if (AllMatchs.Count > 0)
                Console.WriteLine($"{fileName} : {AllMatchs.Count}");
            else
                Console.WriteLine($"Aucune occurence trouvé dans le fichier {fileName} avec le pattern {pattern}");
        }

    }
}


