using System.ComponentModel;
using Microsoft.Extensions.Configuration;

namespace OfficeFinder.Inputs.CommandLine;

/// <summary>
/// <inheritdoc cref="IOptionProvider"/>
/// </summary>
public class CommandLineOptionReader : ICommandLineOptionReader
{
    private string[]  _args;
    private ICommandLineOptionStore _optionStore;
    public CommandLineOptionReader(ICommandLineArgs argStore, ICommandLineOptionStore optionStore)
    {

        this._args = argStore.Args;
        this._optionStore = optionStore;
    }
    public void Init(){
        Dictionary<string, string> OptionsDefinition = _optionStore.GetOptionForConsole();
        
        for(int i = 0; i < _args.Length; i++){
            if(_args[i][0] == '-'){
                bool IsIdentified = OptionsDefinition.TryGetValue(_args[i], out string? nameOfProperty);  // try to identify the option present on args
                if(IsIdentified){   //if the option present on arg exist
                    CommandLineOption option = _optionStore.GetOption(nameOfProperty!); //retrieve the option inside the store
                    if(option.NeedArg){
                        if(i+1 < _args.Length && _args[i+1][0] != '-'){
                            option.Value = _args[i+1];
                        }
                        else{
                            // for the moment the option is just ignored if it's arg is missing
                        }
                    }
                    else{
                        option.SetPresent();
                    }
                }
            }
        }
    }

}