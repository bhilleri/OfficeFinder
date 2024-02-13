namespace OfficeFinderLibrary.Data;
/// <summary>
/// In charge of store all results from files which includes :
/// <list type="bullet">
///     <item>Number of occurences per file</item>
///     <item>Errors encountered during the reading of differents files</item>
/// </list>
/// </summary>
public interface IOccurenceStore{
    /// <summary>
    /// Add the number of occurence found for one file
    /// </summary>
    /// <param name="OccurrencePerFile">
    /// <list type="bullet">
    ///     <item>key : the path of the file</item>
    ///     <item>value : the number of occurence for this file</item>
    /// </list>
    /// </param>
    public void Add(KeyValuePair<string, int> OccurrencePerFile);

    /// <summary>
    /// <para>A list of all occurences reccords for all files</para>
    /// <list type="bullet">
    ///     <item>key : the path of the file</item>
    ///     <item>value : the number of occurencefor this file</item>
    /// </list>
    /// </summary>
    public List<KeyValuePair<string, int>> OccurrencesPerFileList{ get; }
    /// <summary>
    /// Add one error encountered during the reading of a specific file
    /// </summary>
    /// <param name="file">The path of the file</param>
    /// <param name="typeOfError">The type of the error</param>
    /// <param name="errorDetails">Contains the details of the exception catched</param>
    public void AddError(string file, FileErrorEnum typeOfError, string errorDetails);
    /// <summary>
    /// Contains the list of errors encountered during the reading of files?
    /// </summary>
    public List<FileError> ListError {get;}
}