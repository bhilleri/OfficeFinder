using OfficeFinderCommandLine.Inputs;

namespace OfficeFinderCommandLineTest.Inputs;

public class CommandLineOptionTest{

    string _optionName = "name";
    string _description = "description";
    string _abbreviation = "abbreviation";
    bool _needed = false;
    string _optionCode = "optionCode";
    bool _needArg = true;
    string _defaultValue = "defaultValue";
    string _value = "value";
    [Fact]
    public void ConstructorWithoutDefaultValue(){
        CommandLineOption option = new CommandLineOption(
            optionName: _optionName,
            description: _description,
            abbreviationCode: _abbreviation,
            needed: _needed,
            optionCode: _optionCode,
            NeedArg: _needArg
        );

        Assert.Equal(_optionName, option.OptionName);
        Assert.Equal(_description, option.Description);
        Assert.Equal(_abbreviation, option.AbbreviationCode);
        Assert.Equal(_needed, option.Needed);
        Assert.Equal(_optionCode, option.OptionCode);
        Assert.Equal(_needArg, option.NeedArg);
        Assert.Null(option.Value);
        Assert.Null(option.DefaultValue);
        Assert.False(option.IsPresent);
    }

    [Fact]
    public void ConstructorWithDefaultValue(){
        CommandLineOption option = new CommandLineOption(
            optionName: _optionName,
            description: _description,
            abbreviationCode: _abbreviation,
            needed: _needed,
            optionCode: _optionCode,
            NeedArg: _needArg,
            defaultValue: _defaultValue
        );

        Assert.Equal(_optionName, option.OptionName);
        Assert.Equal(_description, option.Description);
        Assert.Equal(_abbreviation, option.AbbreviationCode);
        Assert.Equal(_needed, option.Needed);
        Assert.Equal(_optionCode, option.OptionCode);
        Assert.Equal(_needArg, option.NeedArg);
        Assert.Equal(_defaultValue ,option.Value);
        Assert.Equal(_defaultValue, option.DefaultValue);
        Assert.True(option.IsPresent);
    }

    [Fact]
    public void GetOptionForCommandLine(){
        CommandLineOption option = new CommandLineOption(
            optionName: _optionName,
            description: _description,
            abbreviationCode: _abbreviation,
            needed: _needed,
            optionCode: _optionCode,
            NeedArg: _needArg
        );

        Dictionary<string, string> result = option.GetOptionForCommandLine();

        Assert.Equal(result[_abbreviation], _optionName);
        Assert.Equal(result[_optionCode], _optionName);
    }

    [Fact]
    public void SetPresent(){
        CommandLineOption option = new CommandLineOption(
            optionName: _optionName,
            description: _description,
            abbreviationCode: _abbreviation,
            needed: _needed,
            optionCode: _optionCode,
            NeedArg: false
        );

        Assert.False(option.IsPresent);
        Assert.False(option.NeedArg);

        option.SetPresent();

        Assert.True(option.IsPresent);
    }

    [Fact]
    public void SetValue(){
        CommandLineOption option = new CommandLineOption(
            optionName: _optionName,
            description: _description,
            abbreviationCode: _abbreviation,
            needed: _needed,
            optionCode: _optionCode,
            NeedArg: true
        );

        Assert.False(option.IsPresent);
        Assert.True(option.NeedArg);
        Assert.Null(option.Value);

        option.Value = _value;

        Assert.True(option.IsPresent);
        Assert.Equal(_value, option.Value);
    }
}