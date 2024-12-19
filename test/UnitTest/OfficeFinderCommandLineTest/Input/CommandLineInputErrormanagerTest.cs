using DocumentFormat.OpenXml.Vml;
using Moq;
using OfficeFinderCommandLine.Inputs;
using OfficeFinderCommandLine.src;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace OfficeFinderCommandLineTest.Inputs;

public class CommandLineInputErrorManagerTest
{
    Mock<IConsoleWrapper> consoleWrapperMock;
    IConsoleWrapper consoleWrapper;

    public CommandLineInputErrorManagerTest()
    {
        consoleWrapperMock = new Mock<IConsoleWrapper>();
        consoleWrapper = consoleWrapperMock.Object;
    }

    [Fact]
    public void FileNotFoundTest()
    {
        string path = "c:/test.docx";

        Mock<ICommandLineOptionStore> storeMock = new Mock<ICommandLineOptionStore>();
        ICommandLineOptionStore store = storeMock.Object;

        CommandLineInputErrorManager testedObject = new CommandLineInputErrorManager(store, consoleWrapper);

        testedObject.FileNotFound(path);

        this.consoleWrapperMock.VerifySet(x => x.ForegroundColor = ConsoleColor.Red, Times.Once);
        this.consoleWrapperMock.Verify(x => x.WriteLine($"Le fichier {path} n'a pas été trouvé"), Times.Once);
        this.consoleWrapperMock.Verify(x => x.ResetColor(), Times.Once);
    }

    [Fact]
    public void PathMissing()
    {
        string optionCode = "code";
        CommandLineOption option = new CommandLineOption(
            optionName: "name",
            description: "",
            abbreviationCode: "",
            optionCode: optionCode,
            needed: true,
            NeedArg: false
        );
        string PATHOPTIONNAME = "Path";
        Mock<ICommandLineOptionStore> storeMock = new Mock<ICommandLineOptionStore>();
        storeMock.SetupGet(x => x.PATHOPTIONNAME).Returns(PATHOPTIONNAME);
        storeMock.Setup(x => x.GetOption(PATHOPTIONNAME)).Returns(option);
        ICommandLineOptionStore store = storeMock.Object;

        CommandLineInputErrorManager testedObject = new CommandLineInputErrorManager(store, consoleWrapper);

        testedObject.PathMissing();

        this.consoleWrapperMock.VerifySet(x=> x.ForegroundColor = ConsoleColor.Red, Times.Once);
        this.consoleWrapperMock.Verify(x=> x.WriteLine($"L'option {optionCode} ou son argument est manquant"), Times.Once);
        this.consoleWrapperMock.Verify(x=> x.ResetColor(), Times.Once);
    }


    [Fact]
    public void RegexIsMissing()
    {
        string optionCode = "code";
        CommandLineOption option = new CommandLineOption(
            optionName: "name",
            description: "",
            abbreviationCode: "",
            optionCode: optionCode,
            needed: true,
            NeedArg: false
        );
        string REGEXOPTIONNAME = "Regex";
        Mock<ICommandLineOptionStore> storeMock = new Mock<ICommandLineOptionStore>();
        storeMock.SetupGet(x => x.REGEXOPTIONNAME).Returns(REGEXOPTIONNAME);
        storeMock.Setup(x => x.GetOption(REGEXOPTIONNAME)).Returns(option);
        ICommandLineOptionStore store = storeMock.Object;

        CommandLineInputErrorManager testedObject = new CommandLineInputErrorManager(store, consoleWrapper);

        testedObject.RegexIsMissing();

        this.consoleWrapperMock.VerifySet(x=> x.ForegroundColor = ConsoleColor.Red, Times.Once);
        this.consoleWrapperMock.Verify(x => x.WriteLine($"L'option {optionCode} ou son argument est manquant"), Times.Once);
        this.consoleWrapperMock.Verify(x=> x.ResetColor(), Times.Once);
    }
}