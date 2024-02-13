namespace OfficeFinderLibrary.Inputs.FilesTools;

public interface IFileFilter{
    List<FileInfo> FilterValidFiles(List<FileInfo> fileToCheck);
    bool FileIsValid(FileInfo path);
}