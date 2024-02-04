namespace OfficeFinder;

/// <summary>
/// In charge of research the regex into the file
/// </summary>
public interface IWordSearcher{
    /// <summary>
    /// Execute the regex request into a file
    /// </summary>
    /// <param name="regexPattern">The pattern</param>
    /// <returns>
    /// <list type="bullet">
    /// <item>File Path</item>
    /// <item>Numbers of occurence</item>
    /// </list>
    /// </returns>
    /// <exception cref="System.IO.IOException">File already open</exception>
    /// <exception cref="System.IO.FileFormatException">Temp file</exception>
    public KeyValuePair<string, int> Search(string regexPattern, string filePath);
}