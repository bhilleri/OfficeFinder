using OfficeFinder.Data;
using OfficeFinder.Output;

namespace OfficeFinderCommandLine.Output;

/// <summary>
/// Class used to print the result on the console
/// </summary>
public class CommandLinePrinter : IOutput{
    /// <summary>
    /// <inheritdoc cref="IOccurenceStore"/>
    /// </summary>
    private IOccurenceStore Occurences;
    /// <summary>
    /// <inheritdoc cref="ErrorSignification"/>
    /// </summary>
    private ErrorSignification Signification;

    public CommandLinePrinter(IOccurenceStore occurence, ErrorSignification signification){
        this.Occurences = occurence;
        this.Signification = signification;
    }

    public void Push()
    {
        int nbResultWithMoreThan0Occurence = 0;
        foreach (KeyValuePair<string, int> occurence in Occurences.OccurrencesPerFileList) {
            if (occurence.Value == 0)
                break;
            nbResultWithMoreThan0Occurence++;
        }
        // Print occurences
        Console.WriteLine($"nomnre de fichier(s) analysé(s) : {Occurences.OccurrencesPerFileList.Count}");
        Console.WriteLine($"nombre de fichier(s) avec au moins une occurence : {nbResultWithMoreThan0Occurence}");
        foreach(KeyValuePair<string, int> occurence in Occurences.OccurrencesPerFileList){
            if (occurence.Value == 0)
                break;
            Console.WriteLine($"{occurence.Key} : {occurence.Value}");
        }

        // print errors

        ConsoleColor defaultColor = Console.ForegroundColor;
        
        foreach(FileError error in Occurences.ListError){
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Erreur dans la lecture de {error.File} : {this.Signification[error.TypeOfErreur]} : {error.ErrorDetails}");
        }
        Console.ForegroundColor = defaultColor;
    }
}