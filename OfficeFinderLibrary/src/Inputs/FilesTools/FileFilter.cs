
namespace OfficeFinderLibrary.Inputs.FilesTools;

public class FileFilter : IFileFilter
{
    public FileFilter(){
        UsedFilesConfig = new Dictionary<string, bool>(){
            {WORDEXTENSION, true}
        };
    }
    private string WORDEXTENSION = "docx";

    private Dictionary<string, bool> UsedFilesConfig;

    public bool FileIsValid(FileInfo file)
    {
        if (!file.Exists || file.Name[0] == '~')
            return false;
        bool valid = false;
        foreach(KeyValuePair<string, bool> UsedFilesConfig in UsedFilesConfig){
            if (UsedFilesConfig.Value == true && file.Extension ==$".{UsedFilesConfig.Key}"){
                valid = true;
                break;
            }
        }
        return valid;
    }

    public List<FileInfo> FilterValidFiles(List<FileInfo> fileToCheck)
    {
        return fileToCheck.Where(x => FileIsValid(x)).ToList();
    }
}