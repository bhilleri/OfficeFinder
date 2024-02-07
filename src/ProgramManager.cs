using Microsoft.Extensions.DependencyInjection;
using OfficeFinder.Data;
using OfficeFinder.Inputs;
using OfficeFinder.Output;
using OfficeFinder.Searcher;

namespace OfficeFinder;

public class ProgramManager : IProgramManager
{
    private readonly IOccurenceStore Occurences;
    private readonly ISearcher WordSearcher;
    private readonly ICommandLineOPtionReader CommandInterpreter;
    private IOutput outputManager;
    public ProgramManager(ISearcher wordSearcher, ICommandLineOPtionReader commandInterpreter, IOccurenceStore occurences, IOutput outputManager){
        this.WordSearcher = wordSearcher;
        this.CommandInterpreter = commandInterpreter;
        this.Occurences = occurences;
        this.outputManager = outputManager;
    }
    public void Start()
    {

        (string regex, List<string> listFile) patternListFile = CommandInterpreter.ReadOption();
        foreach(string filePath in patternListFile.listFile){
            try{
                Occurences.Add(WordSearcher.Search(patternListFile.regex, filePath));
            }catch(System.IO.IOException ex){
                this.Occurences.AddError(filePath, FileErrorEnum.FileIsAlreadyOpen, ex.Message);
            }
            catch(System.IO.FileFormatException ex){
                this.Occurences.AddError(filePath, FileErrorEnum.FileIsTemp, ex.Message);
            }
            catch (ArgumentNullException ex){
                this.Occurences.AddError(file: filePath, typeOfError: FileErrorEnum.ArgumentNull, ex.Message);
            }
        }
        this.outputManager.Push();
    }
}