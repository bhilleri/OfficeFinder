
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Extensions.FileProviders;

namespace OfficeFinder.Inputs.FilesTools;

public class FileFinderManager : IFileFinderManager
{
    private IDirectoryExplorer Explorer;
    public FileFinderManager(IDirectoryExplorer explorer){
        Explorer = explorer;
    }
    public List<string> GetFiles(string path)
    {
        List<FileInfo> ListOfFilesFileInfo = new List<FileInfo>();
        if (!(File.Exists(path) || Directory.Exists(path))){
            return new List<string>();
        }

        FileAttributes attributes = File.GetAttributes(path);
        if (((attributes & FileAttributes.Directory) == FileAttributes.Directory) == false){
            ListOfFilesFileInfo = Explorer.GetFiles(new List<FileInfo>() { new FileInfo(path) });
        }
        else
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            List<FileInfo> files = dir.GetFiles("*.docx", SearchOption.AllDirectories).ToList();
            ListOfFilesFileInfo = Explorer.GetFiles(files);
        }
        return ListOfFilesFileInfo.Select(x => x.FullName).ToList();
    }
}