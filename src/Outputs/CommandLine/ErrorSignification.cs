using OfficeFinder.Data;

namespace OfficeFinder.Output.CommandLine;

/// <summary>
/// Class used to give a signification for the error code store in the the FileErrorEnum
/// </summary>
public class ErrorSignification{
    /// <summary>
    /// Contains the signification of errors contains in the FileErrorEnum
    /// </summary>
    private readonly Dictionary<FileErrorEnum, string> Signification;
    public ErrorSignification(){
        Signification = new Dictionary<FileErrorEnum, string>{
            {FileErrorEnum.FileIsTemp,"Le fichier est un fichier temporaire ou est corrompu."},
            {FileErrorEnum.FileIsAlreadyOpen, "Le fichier est déjà ouvert ou inexistant."},
            {FileErrorEnum.ArgumentNull, "La lecture du fichier a échoué pour des raisons non connues"}
        };
    }
    public string this[FileErrorEnum key]{
        get => Signification[key];
    }
}