
namespace OfficeFinder.Inputs.FilesTools;

public class DirectoryExplorer : IDirectoryExplorer
{
    public DirectoryExplorer(IFileFilter filter)
    {
        this.Filter = filter;
    }
    public IFileFilter Filter;

    public List<FileInfo> GetFiles(List<FileInfo> files)
    {
        return Filter.FilterValidFiles(files);
    }
}