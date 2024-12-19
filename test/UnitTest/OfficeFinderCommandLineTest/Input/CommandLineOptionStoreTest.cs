using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Moq;
using OfficeFinderCommandLine.Inputs;

namespace OfficeFinderCommandLineTest.Inputs;

public class CommandLineOptionStoreTest{
    [Fact]
    public void GetNames(){
        Mock<ILogger<CommandLineOptionStore>> mockLogger = new Mock<ILogger<CommandLineOptionStore>>();
        CommandLineOptionStore store = new CommandLineOptionStore(mockLogger.Object);

        string[] names = store.GetNames();
        Assert.Contains(store.PATHOPTIONNAME, names);
        Assert.Contains(store.REGEXOPTIONNAME, names);
        Assert.Contains(store.REGEXOPTIONNAME, names);
        Assert.Equal(3 , names.Length);
    }

    [Fact]
    public void GeOptionPath(){
        Mock<ILogger<CommandLineOptionStore>> mockLogger = new Mock<ILogger<CommandLineOptionStore>>();
        CommandLineOptionStore store = new CommandLineOptionStore(mockLogger.Object);

        CommandLineOption option = store.GetOption(store.PATHOPTIONNAME);

        Assert.Equal(store.PATHOPTIONNAME, option.OptionName);
        Assert.Equal("permet de définir le chemin vers le fichier ou le dossier. valeur par défaut => répertoire courant", option.Description);
        Assert.Equal("-p", option.AbbreviationCode);
        Assert.Equal("--path", option.OptionCode);
        Assert.False(option.Needed);
        Assert.Equal("./", option.DefaultValue);
        Assert.True(option.NeedArg);
        Assert.True(option.IsPresent);
        Assert.Equal("./", option.Value);
    }

        [Fact]
    public void GeOptionRegex(){
        Mock<ILogger<CommandLineOptionStore>> mockLogger = new Mock<ILogger<CommandLineOptionStore>>();
        CommandLineOptionStore store = new CommandLineOptionStore(mockLogger.Object);

        CommandLineOption option = store.GetOption(store.REGEXOPTIONNAME);

        Assert.Equal(store.REGEXOPTIONNAME, option.OptionName);
        Assert.Equal("Définition de l'expression régulière à rechercher", option.Description);
        Assert.Equal("-r", option.AbbreviationCode);
        Assert.Equal("--regex", option.OptionCode);
        Assert.True(option.Needed);
        Assert.Null(option.DefaultValue);
        Assert.True(option.NeedArg);
        Assert.False(option.IsPresent);
        Assert.Null(option.Value);
    }

    [Fact]
    public void GeOptionHelper(){
        Mock<ILogger<CommandLineOptionStore>> mockLogger = new Mock<ILogger<CommandLineOptionStore>>();
        CommandLineOptionStore store = new CommandLineOptionStore(mockLogger.Object);

        CommandLineOption option = store.GetOption(store.HELPOPTIONNAME);

        Assert.Equal(store.HELPOPTIONNAME, option.OptionName);
        Assert.Equal("Affiche l'aide", option.Description);
        Assert.Equal("-h", option.AbbreviationCode);
        Assert.Equal("--help", option.OptionCode);
        Assert.False(option.Needed);
        Assert.Null(option.DefaultValue);
        Assert.False(option.NeedArg);
        Assert.False(option.IsPresent);
        Assert.Null(option.Value);
    }

    [Fact]
    public void GeOptionError(){
        Mock<ILogger<CommandLineOptionStore>> mockLogger = new Mock<ILogger<CommandLineOptionStore>>();
        CommandLineOptionStore store = new CommandLineOptionStore(mockLogger.Object);
        string optionName = "gsngoisgidsj";
        Assert.Throws<ArgumentNullException>(() =>  store.GetOption(optionName));
    }

    [Fact]
    public void GetOptionForConsole(){
        Mock<ILogger<CommandLineOptionStore>> mockLogger = new Mock<ILogger<CommandLineOptionStore>>();
        CommandLineOptionStore store = new CommandLineOptionStore(mockLogger.Object);

        Dictionary<string, string> allOptionForConsole = store.GetOptionForConsole();

        Assert.Equal(store.PATHOPTIONNAME, allOptionForConsole["-p"]);
        Assert.Equal(store.PATHOPTIONNAME, allOptionForConsole["--path"]);

        Assert.Equal(store.REGEXOPTIONNAME, allOptionForConsole["-r"]);
        Assert.Equal(store.REGEXOPTIONNAME, allOptionForConsole["--regex"]);

        Assert.Equal(store.HELPOPTIONNAME, allOptionForConsole["-h"]);
        Assert.Equal(store.HELPOPTIONNAME, allOptionForConsole["--help"]);

        Assert.Equal(6, allOptionForConsole.Count);

    }
}