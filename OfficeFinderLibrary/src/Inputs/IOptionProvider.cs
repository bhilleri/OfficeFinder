namespace OfficeFinderLibrary.Inputs;
/// <summary>
// Object that contains and manage all options selected by the User
/// </summary>
public interface IOptionProvider
{
    /// <summary>
    /// The path targeted by the User
    /// </summary>
    public string? Path { get; }
    /// <summary>
    /// The regex define by the User
    /// </summary>
    public string? Regex{ get; }
    /// <summary>
    /// If user asks help
    /// </summary>
    public bool HelpRequired{ get; }
}