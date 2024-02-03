namespace OfficeFinder;

/// <summary>
/// In charge of research the regex into the file
/// </summary>
public interface IWordSearcher{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="regexPattern">The pattern</param>
    /// <returns>
    /// <list type="bullet">
    /// <item>File Path</item>
    /// <item>Numbers of occurence</item>
    /// </list>
    /// </returns>
    public KeyValuePair<string, int> Search(string regexPattern);
}