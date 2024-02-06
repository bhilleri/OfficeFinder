namespace OfficeFinder.Inputs;

public interface IOptionManager
{
    public string Directory { get; }
    public string File { get; }
    public string Regex{ get; }
}