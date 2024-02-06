using System.ComponentModel;
using Microsoft.Extensions.Configuration;

namespace OfficeFinder.Inputs;

public class OptionManager :IOptionManager
{
    private const string REGEXKEY = "regex";
    private const string REGEXKEYABBREVIATION = "regexAbbreviation";
    private const string FILE = "file";
    private const string FILEABBREVIATION = "fileAbbreviation";
    private const string DIRECTORY = "Directory";
    private const string DIRECTORYABBREVIATION = "DirectoryAbbreviation";
    private Dictionary<string, string> OptionsDefinition = new Dictionary<string, string>(){
        {"--regex", REGEXKEY},
        {"-r",REGEXKEYABBREVIATION},
        {"--file",FILE},
        {"-f",FILEABBREVIATION},
        {"--directory", DIRECTORY},
        {"-d",DIRECTORYABBREVIATION},
    };
    
    private IConfigurationRoot UserOption;
    public OptionManager(string[] args) {
        var builder = new ConfigurationBuilder();
        builder.AddCommandLine(args, OptionsDefinition);
        UserOption = builder.Build();
    }

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
    public string? Regex => this.UserOption.GetValue<string>(REGEXKEY) ?? this.UserOption.GetValue<string>(REGEXKEYABBREVIATION);

    public string? File => this.UserOption.GetValue<string?>(FILE)?? this.UserOption.GetValue<string?>(FILEABBREVIATION);
    public string? Directory => this.UserOption.GetValue<string?>(DIRECTORY)?? this.UserOption.GetValue<string?>(DIRECTORYABBREVIATION);
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
};

