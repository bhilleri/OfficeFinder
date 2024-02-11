using System.Collections.Generic;

namespace OfficeFinder.Inputs.CommandLine;

/// <summary>
/// Define one option for the command line application
/// </summary>
public class CommandLineOption{
    public CommandLineOption(string optionName, string description, string abbreviationCode, string optionCode, bool needed, bool NeedArg){
        this._optionName = optionName;
        this._description = description;
        this._abbreviationCode = abbreviationCode;
        this._needed = needed;
        this._optionCode = optionCode;
        this._isPresent = false;
        this._needArg = NeedArg;
    }
    public CommandLineOption(string optionName, string description, string abbreviationCode, string optionCode, bool needed, string defaultValue, bool NeedArg) : this(optionName, description, abbreviationCode, optionCode, needed, NeedArg){
        this._defaultValue = defaultValue;
        this._value = defaultValue;
        this._isPresent = true;
    }
    private string _optionName;
    public string OptionName => _optionName;
    private string _description;
    public string Description => _description;
    private string _abbreviationCode;
    /// <summary>
    /// Code to enter in the command line to define the option with one letter (ex: -o)
    /// </summary>
    public string AbbreviationCode => _abbreviationCode;
    /// <summary>
    /// Code to enter in the command line to define the option (ex : --option)
    /// </summary>
    private string _optionCode;
    public string OptionCode => _optionCode;
    private bool _needed;
    public bool Needed => _needed;
    public string? _defaultValue;
    private string? _value;
    public string? Value{
        get => this._value;
        set{
            this._value = value;
            _isPresent = true;
        }
    }
    private bool _isPresent;
    /// <summary>
    /// Define if the option has been defined by the user.
    /// This information is used when the option has'nt argument
    /// </summary>
    public bool IsPresent => _isPresent;
    private bool _needArg;
    /// <summary>
    /// Define if the option need argument
    /// </summary>
    public bool NeedArg => _needArg;
    public Dictionary<string, string> GetOptionForCommandLine(){
        return new Dictionary<string, string>{
            { _abbreviationCode, _optionName },
            {_optionCode, _optionName }
        };
    }

    /// <summary>
    /// Called to set the present statut to true.
    /// used only if the option hasn't got arguement. Otherwise the <see cref="GetOptionForCommandLine"/> should be used
    /// </summary>
    public void SetPresent(){
        this._isPresent = true;
    }
}