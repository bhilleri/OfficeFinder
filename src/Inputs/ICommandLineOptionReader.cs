namespace OfficeFinder.Inputs;

/// <summary>
/// In charge of retrieve options when the users enter the command
/// </summary>
public interface ICommandLineOPtionReader{
    /// <summary>
    /// Retrieve options when the user enter the command
    /// </summary>
    /// <param name="args">List of args provided to the program</param>
    /// <returns></returns>
    (string regex, List<string> listFile) ReadOption();
}