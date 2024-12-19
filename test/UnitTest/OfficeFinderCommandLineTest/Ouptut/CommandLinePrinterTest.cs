using Moq;
using OfficeFinderCommandLine.Output;
using OfficeFinderCommandLine.src;
using OfficeFinderLibrary.Data;

namespace OfficeFinderCommandLineTest.Output;

public class CommandLinePrinterTest
{
    private Mock<IConsoleWrapper> consoleWrapperMock;
    private IConsoleWrapper consoleWrapperInstance;
    public Mock<IOccurenceStore> StoreMock;
    public CommandLinePrinterTest()
    {
        StoreMock = new Mock<IOccurenceStore>();
        consoleWrapperMock = new Mock<IConsoleWrapper>();
        consoleWrapperInstance = consoleWrapperMock.Object;
    }
    [Theory]
    [MemberData(nameof(GetData))]
    public void PushTest(List<KeyValuePair<string, int>> result, List<string> expectedOutput)
    {
        StoreMock.SetupGet<List<KeyValuePair<string, int>>>(x => x.OccurrencesPerFileList).Returns(result);
        StoreMock.SetupGet<List<FileError>>(x=>x.ListError).Returns(new List<FileError> ());

        ErrorSignification ErrorSignificationObject = new ErrorSignification();
        CommandLinePrinter testedObject = new CommandLinePrinter(StoreMock.Object, consoleWrapperInstance, ErrorSignificationObject);

        testedObject.Push();

        foreach (string element in expectedOutput) {
            this.consoleWrapperMock.Verify(x => x.WriteLine(element), Times.Once);
        }
    }

    public static IEnumerable<object[]> GetData()
    {
        List<KeyValuePair<string, int>> allOccurences = new List<KeyValuePair<string, int>>(){
            new KeyValuePair<string, int>("file1", 100000),
            new KeyValuePair<string, int>("file2", 15000),
            new KeyValuePair<string, int>("file3", 874),
            new KeyValuePair<string, int>("file4", 412),
            new KeyValuePair<string, int>("file5", 50),
            new KeyValuePair<string, int>("file6", 20),
            new KeyValuePair<string, int>("file7", 12),
            new KeyValuePair<string, int>("file8", 10),
            new KeyValuePair<string, int>("file9", 8),
            new KeyValuePair<string, int>("file10", 5),
            new KeyValuePair<string, int>("file11", 2),
            new KeyValuePair<string, int>("file12", 1),
            new KeyValuePair<string, int>("file13", 1),
            new KeyValuePair<string, int>("file14", 1),
            new KeyValuePair<string, int>("file15", 1),
            new KeyValuePair<string, int>("file16", 0),
            new KeyValuePair<string, int>("file17", 0),
            new KeyValuePair<string, int>("file18", 0),
            new KeyValuePair<string, int>("file19", 0),
       };

        List<string> ResultExpected = new List<string>();
        for(int i =0; i < allOccurences.Count; i++){
            if (allOccurences[i].Value > 0)
                ResultExpected.Add($"{allOccurences[i].Key} : {allOccurences[i].Value}");
        }
        yield return new object[] { allOccurences, GetExpectedString(allOccurences, ResultExpected) };
    }

    public static List<string> GetExpectedString(List<KeyValuePair<string, int>> result, List<string> ResultExpected){
        int filesNumber = result.Count;
        int fileWithMoreThanOneOccurence = 0;
        for(int i = 0; i<result.Count; i++){
            if(result[i].Value > 0){
                fileWithMoreThanOneOccurence++;
            }
        }

        ResultExpected.Insert(0, $"nombre de fichier(s) analysé(s) : {filesNumber}");
        ResultExpected.Insert(1, $"nombre de fichier(s) avec au moins une occurence : {fileWithMoreThanOneOccurence}");

        return ResultExpected;
    }
}