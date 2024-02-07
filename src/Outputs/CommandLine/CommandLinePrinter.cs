using OfficeFinder.Data;

namespace OfficeFinder.Output.CommandLine;

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
        // Print occurences
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