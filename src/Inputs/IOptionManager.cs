namespace OfficeFinder.Inputs;
/// <summary>
// Object that contains and manage all options selected by the User
/// </summary>
public interface IOptionManager
{
    /// <summary>
    /// The directory where the search should begin
    /// </summary>
    public string Directory { get; }
    /// <summary>
    /// The file targeted by the User
    /// </summary>
    public string File { get; }
    /// <summary>
    /// The regex define by the User
    /// </summary>
    public string Regex{ get; }
}