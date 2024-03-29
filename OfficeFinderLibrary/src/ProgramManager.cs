using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Extensions.DependencyInjection;
using OfficeFinderLibrary.Data;
using OfficeFinderLibrary.Inputs;
using OfficeFinderLibrary.Output;
using OfficeFinderLibrary.Searcher;

namespace OfficeFinderLibrary;

public class ProgramManager : IProgramManager
{
    private readonly IOccurenceStore Occurences;
    private readonly ISearcher WordSearcher;
    private readonly IOptionManager CommandInterpreter;
    private IOutput outputManager;
    public ProgramManager(ISearcher wordSearcher, IOptionManager commandInterpreter, IOccurenceStore occurences, IOutput outputManager){
        this.WordSearcher = wordSearcher;
        this.CommandInterpreter = commandInterpreter;
        this.Occurences = occurences;
        this.outputManager = outputManager;
    }
    public void Start()
    {
        try{
        (string? regex, List<string>? listFile) patternListFile = CommandInterpreter.ReadOption();
        if(patternListFile.regex is null || patternListFile.listFile is null){
            return;
        }
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
        catch(ArgumentException){
            // already be logged
        }

    }
}