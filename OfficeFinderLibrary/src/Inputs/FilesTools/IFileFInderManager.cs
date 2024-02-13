namespace OfficeFinder.Inputs.FilesTools;

public interface IFileFinderManager {
    public List<string> GetFiles(string path);
}