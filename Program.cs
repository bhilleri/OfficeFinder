using System.IO.Enumeration;
using System.IO.Packaging;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;

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
            KeyValuePair<string, int> NumberOfOccurence = wordSearcher.Search(patternListFile.regex);
            Console.WriteLine($"{NumberOfOccurence.Key} : {NumberOfOccurence.Value}");
        }

    }
}


