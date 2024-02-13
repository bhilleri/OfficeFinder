namespace OfficeFinderLibrary.Inputs.FilesTools;

public interface IDirectoryExplorer {
    public List<FileInfo> GetFiles(List<FileInfo> path);
}