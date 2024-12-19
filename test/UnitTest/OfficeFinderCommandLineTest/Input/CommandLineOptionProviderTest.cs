using Moq;
using OfficeFinderCommandLine.Inputs;

namespace OfficeFinderCommandLineTest.Inputs;

public class CommandLineOptionProviderTest{
    private readonly string PATHNAME = "path";
    private readonly string REGEXNAME = "Regex";
    private readonly string HELPNAME = "Help";

    private CommandLineOption Option;

    public CommandLineOptionProviderTest(){
        Option = new CommandLineOption(
            optionName: REGEXNAME,
            description: "",
            abbreviationCode: "",
            optionCode: "",
            needed: true,
            NeedArg: false
        );
    }

    [Fact]
    public void PathGetProperty(){
        string OptionValue = "Value";
        Mock<ICommandLineOptionReader> MockReader = new Mock<ICommandLineOptionReader>();
        Mock<ICommandLineOptionStore> MockStore = new Mock<ICommandLineOptionStore>();

        Option.Value = OptionValue;

        MockStore.SetupGet(x => x.PATHOPTIONNAME).Returns(PATHNAME);
        MockStore.Setup(x => x.GetOption(PATHNAME)).Returns(Option);

        CommandLineOptionProvider commandLineProvider = new CommandLineOptionProvider(MockStore.Object, MockReader.Object);
        MockReader.Verify(x => x.Init(), Times.Once());
        Assert.Equal(OptionValue, commandLineProvider.Path);
    }

        [Fact]
    public void RegexGetProperty(){
        string OptionValue = "Value";
        Mock<ICommandLineOptionReader> MockReader = new Mock<ICommandLineOptionReader>();
        Mock<ICommandLineOptionStore> MockStore = new Mock<ICommandLineOptionStore>();


        Option.Value = OptionValue;

        MockStore.SetupGet(x => x.REGEXOPTIONNAME).Returns(REGEXNAME);
        MockStore.Setup(x => x.GetOption(REGEXNAME)).Returns(Option);

        CommandLineOptionProvider commandLineProvider = new CommandLineOptionProvider(MockStore.Object, MockReader.Object);
        MockReader.Verify(x => x.Init(), Times.Once());
        Assert.Equal(OptionValue, commandLineProvider.Regex);
    }
        [Fact]
    public void HelpGetProperty(){
        string OptionValue = "Value";
        Mock<ICommandLineOptionReader> MockReader = new Mock<ICommandLineOptionReader>();
        Mock<ICommandLineOptionStore> MockStore = new Mock<ICommandLineOptionStore>();


        Option.SetPresent();

        MockStore.SetupGet(x => x.HELPOPTIONNAME).Returns(HELPNAME);
        MockStore.Setup(x => x.GetOption(HELPNAME)).Returns(Option);

        CommandLineOptionProvider commandLineProvider = new CommandLineOptionProvider(MockStore.Object, MockReader.Object);
        MockReader.Verify(x => x.Init(), Times.Once());
        Assert.True(commandLineProvider.HelpRequired);
    }
}