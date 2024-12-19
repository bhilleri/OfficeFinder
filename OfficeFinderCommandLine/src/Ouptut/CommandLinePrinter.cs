using OfficeFinderCommandLine.src;
using OfficeFinderLibrary.Data;
using OfficeFinderLibrary.Output;

namespace OfficeFinderCommandLine.Output;

/// <summary>
/// Class used to print the result on the console
/// </summary>
public class CommandLinePrinter : IOutput{
    /// <summary>
    /// <inheritdoc cref="IOccurenceStore"/>
    /// </summary>
    private IOccurenceStore _store;
    /// <summary>
    /// <inheritdoc cref="ErrorSignification"/>
    /// </summary>
    private ErrorSignification Signification;
    private IConsoleWrapper consoleWrapper;

    public CommandLinePrinter(IOccurenceStore store, IConsoleWrapper consoleWrapper, ErrorSignification signification){
        this._store = store;
        this.Signification = signification;
        this.consoleWrapper = consoleWrapper;
    }

    public void Push()
    {
        int nbResultWithMoreThan0Occurence = 0;
        foreach (KeyValuePair<string, int> occurence in _store.OccurrencesPerFileList) {
            if (occurence.Value == 0)
                break;
            nbResultWithMoreThan0Occurence++;
        }
        // Print occurences
        consoleWrapper.WriteLine($"nombre de fichier(s) analysé(s) : {_store.OccurrencesPerFileList.Count}");
        consoleWrapper.WriteLine($"nombre de fichier(s) avec au moins une occurence : {nbResultWithMoreThan0Occurence}");
        foreach(KeyValuePair<string, int> occurence in _store.OccurrencesPerFileList){
            if (occurence.Value == 0)
                break;
            consoleWrapper.WriteLine($"{occurence.Key} : {occurence.Value}");
        }

        // print errors

        ConsoleColor defaultColor = consoleWrapper.ForegroundColor;
        
        foreach(FileError error in _store.ListError){
            consoleWrapper.ForegroundColor = ConsoleColor.Yellow;
            consoleWrapper.WriteLine($"Erreur dans la lecture de {error.File} : {this.Signification[error.TypeOfErreur]} : {error.ErrorDetails}");
        }
        consoleWrapper.ForegroundColor = defaultColor;
    }
}