namespace OfficeFinderLibrary.Data;

/// <summary>
/// Structure used to store data about one error encoutered during the reading phase of the program
/// </summary>
public struct FileError{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="typeOfErreur">The type of the error</param>
    /// <param name="file">the path to the file</param>
    /// <param name="errorDetails">The message of the exception catched</param>
    public FileError(FileErrorEnum typeOfErreur, string file, string errorDetails){
        this.TypeOfErreur = typeOfErreur;
        this.File = file;
        this.ErrorDetails = errorDetails;
    }
    /// <summary>
    /// The type of the error
    /// </summary>
    public readonly FileErrorEnum TypeOfErreur;
    /// <summary>
    /// the path to the file
    /// </summary>
    public readonly string File;
    /// <summary>
    /// The message of the exception catched
    /// </summary>
    public readonly string ErrorDetails;
}