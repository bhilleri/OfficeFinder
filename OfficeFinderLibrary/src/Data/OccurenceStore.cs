
namespace OfficeFinderLibrary.Data;

/// <summary>
/// <inheritdoc cref="IOccurenceStore"/>
/// </summary>
public class OccurenceStore : IOccurenceStore
{
    public OccurenceStore(){

    }
    public List<KeyValuePair<string, int>> OccurrencesPerFileList => _occurencesPerFIleList;

    /// <summary>
    /// <inheritdoc cref="ListError"/>
    /// </summary>
    private List<FileError> _listError = new List<FileError>();
    public List<FileError> ListError => _listError;

    /// <summary>
    /// <inheritdoc cref="OccurrencesPerFileList"/>
    /// </summary>
    private List<KeyValuePair<string, int>> _occurencesPerFIleList = new List<KeyValuePair<string, int>>();
    public void Add(KeyValuePair<string, int> OccurrencePerFile)
    {
        int min = 0;
        int max = _occurencesPerFIleList.Count;
        while(min != max){
            int i = ((max - min) / 2) + min;
            if (OccurrencePerFile.Value > _occurencesPerFIleList[i].Value)
                max = i;
            else
                min = i +1 ;
        }
        _occurencesPerFIleList.Insert(max, OccurrencePerFile);
    }

    public void AddError(string file, FileErrorEnum typeOfError, string errorDetails)
    {
        this._listError.Add(new FileError(typeOfError, file, errorDetails));
    }
}
