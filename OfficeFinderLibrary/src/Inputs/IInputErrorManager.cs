namespace OfficeFinder.Inputs;
/// <summary>
/// Called to notify the user that a prolem has occured
/// </summary>
public interface IInputErrorManager{
    /// <summary>
    /// Notify the user that the option path is missing
    /// </summary>
    public void PathMissing();
    /// <summary>
    /// Notifiy the user that the file or directory is not found
    /// </summary>
    /// <param name="path"></param>
    public void FileNotFound(string path);
    /// <summary>
    /// Notify to the user that the regex is missing
    /// </summary>
    public void RegexIsMissing();
}