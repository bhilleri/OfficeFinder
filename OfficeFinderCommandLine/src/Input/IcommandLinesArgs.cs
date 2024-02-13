namespace OfficeFinderCommandLine.Inputs;

/// <summary>
/// Contains all arguments given by the user to the program.
/// </summary>
public interface ICommandLineArgs{
    /// <summary>
    /// args given by the user
    /// </summary>
    public string[]Args { get; }
}