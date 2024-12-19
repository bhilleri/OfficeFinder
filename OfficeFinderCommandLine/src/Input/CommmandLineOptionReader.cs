using System.Linq.Expressions;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Math;
using Microsoft.Extensions.Logging;
using OfficeFinderCommandLine;

namespace OfficeFinderCommandLine.Inputs;

/// <summary>
/// <inheritdoc cref="IOptionProvider"/>
/// </summary>
public class CommandLineOptionReader : ICommandLineOptionReader
{
    private string[]  _args;
    private ICommandLineOptionStore _optionStore;
    private ILogger<ICommandLineOptionReader> _logger;
    private IEventIDOfficeFinderCommandLIne _eventIDFinder;
    public CommandLineOptionReader(ICommandLineArgs argStore, ICommandLineOptionStore optionStore, ILogger<ICommandLineOptionReader> logger, IEventIDOfficeFinderCommandLIne evenIDStore)
    {

        this._args = argStore.Args;
        this._optionStore = optionStore;
        this._logger = logger;
        _eventIDFinder = evenIDStore;
    }
    public void Init(){
        Dictionary<string, string> OptionsDefinition = _optionStore.GetOptionForConsole();
        
        for(int i = 0; i < _args.Length; i++){
            if (_args[i][0] == '-')
            {
                bool IsIdentified = OptionsDefinition.TryGetValue(_args[i], out string? nameOfProperty);  // try to identify the option present on args
                if (IsIdentified)
                {   //if the option present on arg exist
                    try
                    {

                        CommandLineOption option = _optionStore.GetOption(nameOfProperty!); //retrieve the option inside the store
                        if (option.NeedArg)
                        {
                            if (i + 1 < _args.Length && _args[i + 1][0] != '-')
                            {
                                option.Value = _args[i + 1];
                            }
                            else
                            {
                                _logger.LogWarning(_eventIDFinder.OptionHasNotArgument, _eventIDFinder.MessageOfOptionHasNotArgument(_args[i]));
                            }
                        }
                        else
                        {
                            option.SetPresent();    // the option hasn't argument and doesn't need argument
                        }
                    }
                    catch (ArgumentNullException ex)
                    {
                        this._logger.LogWarning(_eventIDFinder.OPtionIsNotRecongnized, _eventIDFinder.MessageOfOptionIsNotRecongnized(_args[i]), ex);
                        // The option is not recongize
                        // This line can't be reach in normal situation because wee already known that the option is known.
                    }
                }
                else
                {
                    this._logger.LogWarning(_eventIDFinder.OPtionIsNotRecongnized, _eventIDFinder.MessageOfOptionIsNotRecongnized(_args[i]));
                    // Element is not an option
                }
            }
        }
    }

}