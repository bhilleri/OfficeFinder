using Microsoft.Extensions.Logging;
using Moq;
using OfficeFinderCommandLine;
using OfficeFinderCommandLine.Inputs;

namespace OfficeFinderCommandLineTest.Inputs;

public class CommandLineOptionReaderTest{
    /*
        3 option
        |-> path -> need arg
        |-> regex -> need arg
        |-> help -> not need arg
        2 code pour chaque
        6 possibilité
        2 erreurs


    */

    EventIDOfficeFInderCommandLine EventStore = new EventIDOfficeFInderCommandLine();  

    [Fact]
    public void InitIsNotANOption(){
        Mock<ICommandLineArgs> mockArgs = new Mock<ICommandLineArgs>();
        string[] args = new string[]{
            "Not an option"
        };
        mockArgs.SetupGet<string[]>(x => x.Args).Returns(args);
        Mock<ILogger<ICommandLineOptionReader>> mockLogger = new Mock<ILogger<ICommandLineOptionReader>>();
        Mock<ICommandLineOptionStore> mockStore = new Mock<ICommandLineOptionStore>();
        mockStore.Setup(x => x.GetOption(It.IsAny<string>()));


        Dictionary<string, string> AllOptions = new Dictionary<string, string>(){
            {"--optionA", "OptionA"},
            {"-A", "OptionA"},
            {"--OptionB", "OptionB"},
            {"-B", "OptionB"},
            {"--OptionC", "OptionC"},
            {"-c", "OptionC"}
        };
        mockStore.Setup(x => x.GetOptionForConsole()).Returns(AllOptions);

        
        CommandLineOptionReader reader = new CommandLineOptionReader(mockArgs.Object, mockStore.Object, mockLogger.Object, EventStore);
        reader.Init();
        mockLogger.Verify(x => x.Log(
        It.IsAny<LogLevel>(),
        It.IsAny<EventId>(),
        It.IsAny<It.IsAnyType>(),
        It.IsAny<Exception>(),
        (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()), Times.Never);
        mockStore.Verify(x => x.GetOption(It.IsAny<string>()), Times.Never);
    }


    [Fact]
    public void InitInexistantOption(){
        Mock<ICommandLineArgs> mockArgs = new Mock<ICommandLineArgs>();
        string[] args = new string[]{
            "--IMAJOCK"
        };
        mockArgs.SetupGet<string[]>(x => x.Args).Returns(args);
        Mock<ILogger<ICommandLineOptionReader>> mockLogger = new Mock<ILogger<ICommandLineOptionReader>>();
        Mock<ICommandLineOptionStore> mockStore = new Mock<ICommandLineOptionStore>();
        mockStore.Setup(x => x.GetOption(It.IsAny<string>()));


        Dictionary<string, string> AllOptions = new Dictionary<string, string>(){
            {"--optionA", "OptionA"},
            {"-A", "OptionA"},
            {"--OptionB", "OptionB"},
            {"-B", "OptionB"},
            {"--OptionC", "OptionC"},
            {"-c", "OptionC"}
        };
        mockStore.Setup(x => x.GetOptionForConsole()).Returns(AllOptions);

        CommandLineOptionReader reader = new CommandLineOptionReader(mockArgs.Object, mockStore.Object, mockLogger.Object, EventStore);

        reader.Init();
        mockLogger.Verify(x => x.Log(
        It.IsAny<LogLevel>(),
        It.IsAny<EventId>(),
        It.Is<It.IsAnyType>((x, v)=> x.ToString() == this.EventStore.MessageOfOptionIsNotRecongnized(args[0])),
        It.IsAny<Exception>(),
        (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()), Times.Once);
        mockStore.Verify(x => x.GetOption(It.IsAny<string>()), Times.Never);
    }

        [Fact]
    public void InitInexistantOptionButTheOPtionISInTheDictionaryProvideByGetOptionForConsole(){
        Mock<ICommandLineArgs> mockArgs = new Mock<ICommandLineArgs>();
        string[] args = new string[]{
            "--optionA"
        };
        mockArgs.SetupGet<string[]>(x => x.Args).Returns(args);
        Mock<ILogger<ICommandLineOptionReader>> mockLogger = new Mock<ILogger<ICommandLineOptionReader>>();
        Mock<ICommandLineOptionStore> mockStore = new Mock<ICommandLineOptionStore>();
        mockStore.Setup(x => x.GetOption(It.IsAny<string>()));


        Dictionary<string, string> AllOptions = new Dictionary<string, string>(){
            {"--optionA", "OptionA"},
            {"-A", "OptionA"},
            {"--OptionB", "OptionB"},
            {"-B", "OptionB"},
            {"--OptionC", "OptionC"},
            {"-c", "OptionC"}
        };
        mockStore.Setup(x => x.GetOptionForConsole()).Returns(AllOptions);

        mockStore.Setup(x => x.GetOption(It.IsAny<string>())).Throws(() => new ArgumentNullException(args[0]));

        CommandLineOptionReader reader = new CommandLineOptionReader(mockArgs.Object, mockStore.Object, mockLogger.Object, EventStore);

        reader.Init();
        mockLogger.Verify(x => x.Log(
        It.IsAny<LogLevel>(),
        It.IsAny<EventId>(),
        It.Is<It.IsAnyType>((x, v)=> x.ToString() == this.EventStore.MessageOfOptionIsNotRecongnized(args[0])),
        It.IsAny<ArgumentNullException>(),
        (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()), Times.Once);
        mockStore.Verify(x => x.GetOption(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void InitSetPresent(){
        Mock<ICommandLineArgs> mockArgs = new Mock<ICommandLineArgs>();
        string[] args = new string[]{
            "--optionA"
        };
        mockArgs.SetupGet<string[]>(x => x.Args).Returns(args);
        Mock<ILogger<ICommandLineOptionReader>> mockLogger = new Mock<ILogger<ICommandLineOptionReader>>();
        Mock<ICommandLineOptionStore> mockStore = new Mock<ICommandLineOptionStore>();
        mockStore.Setup(x => x.GetOption(It.IsAny<string>()));


        Dictionary<string, string> AllOptions = new Dictionary<string, string>(){
            {"--optionA", "OptionA"},
            {"-A", "OptionA"},
            {"--OptionB", "OptionB"},
            {"-B", "OptionB"},
            {"--OptionC", "OptionC"},
            {"-c", "OptionC"}
        };

        CommandLineOption option = new CommandLineOption(
            optionName: "OptionA",
            description: "",
            abbreviationCode: "-A",
            optionCode: "--OptionA",
            needed: true,
            NeedArg: false
        );
        mockStore.Setup(x => x.GetOptionForConsole()).Returns(AllOptions);

        mockStore.Setup(x => x.GetOption("OptionA")).Returns(option);

        CommandLineOptionReader reader = new CommandLineOptionReader(mockArgs.Object, mockStore.Object, mockLogger.Object, EventStore);

        reader.Init();
        mockStore.Verify(x => x.GetOption("OptionA"), Times.Once);
        Assert.True(option.IsPresent);
        Assert.Null(option.Value);
    }


    [Fact]
    public void InitOptionNeedArgButDoesntHave(){
        Mock<ICommandLineArgs> mockArgs = new Mock<ICommandLineArgs>();
        string[] args = new string[]{
            "--optionA"
        };
        mockArgs.SetupGet<string[]>(x => x.Args).Returns(args);
        Mock<ILogger<ICommandLineOptionReader>> mockLogger = new Mock<ILogger<ICommandLineOptionReader>>();
        Mock<ICommandLineOptionStore> mockStore = new Mock<ICommandLineOptionStore>();
        mockStore.Setup(x => x.GetOption(It.IsAny<string>()));


        Dictionary<string, string> AllOptions = new Dictionary<string, string>(){
            {"--optionA", "OptionA"},
            {"-A", "OptionA"},
            {"--OptionB", "OptionB"},
            {"-B", "OptionB"},
            {"--OptionC", "OptionC"},
            {"-c", "OptionC"}
        };

        CommandLineOption option = new CommandLineOption(
            optionName: "OptionA",
            description: "",
            abbreviationCode: "-A",
            optionCode: "--OptionA",
            needed: true,
            NeedArg: true
        );
        mockStore.Setup(x => x.GetOptionForConsole()).Returns(AllOptions);

        mockStore.Setup(x => x.GetOption("OptionA")).Returns(option);

        CommandLineOptionReader reader = new CommandLineOptionReader(mockArgs.Object, mockStore.Object, mockLogger.Object, EventStore);

        reader.Init();
        mockStore.Verify(x => x.GetOption("OptionA"), Times.Once);
        Assert.False(option.IsPresent);
        Assert.Null(option.Value);
        mockLogger.Verify(x => x.Log(
        It.Is<LogLevel>(x=> x.Equals(LogLevel.Warning)),
        It.Is<EventId>(x => x.Id == this.EventStore.OptionHasNotArgument),
        It.Is<It.IsAnyType>((x, v)=> x.ToString() == this.EventStore.MessageOfOptionHasNotArgument(args[0])),
        It.IsAny<Exception>(),
        (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()), Times.Once);
    }

    [Fact]
    public void InitOptionNeedArg(){
        Mock<ICommandLineArgs> mockArgs = new Mock<ICommandLineArgs>();
        string[] args = new string[]{
            "--optionA", "Value of OptionA"
        };
        mockArgs.SetupGet<string[]>(x => x.Args).Returns(args);
        Mock<ILogger<ICommandLineOptionReader>> mockLogger = new Mock<ILogger<ICommandLineOptionReader>>();
        Mock<ICommandLineOptionStore> mockStore = new Mock<ICommandLineOptionStore>();
        mockStore.Setup(x => x.GetOption(It.IsAny<string>()));


        Dictionary<string, string> AllOptions = new Dictionary<string, string>(){
            {"--optionA", "OptionA"},
            {"-A", "OptionA"},
            {"--OptionB", "OptionB"},
            {"-B", "OptionB"},
            {"--OptionC", "OptionC"},
            {"-c", "OptionC"}
        };

        CommandLineOption option = new CommandLineOption(
            optionName: "OptionA",
            description: "",
            abbreviationCode: "-A",
            optionCode: "--OptionA",
            needed: true,
            NeedArg: true
        );
        mockStore.Setup(x => x.GetOptionForConsole()).Returns(AllOptions);

        mockStore.Setup(x => x.GetOption("OptionA")).Returns(option);

        CommandLineOptionReader reader = new CommandLineOptionReader(mockArgs.Object, mockStore.Object, mockLogger.Object, EventStore);

        reader.Init();
        mockStore.Verify(x => x.GetOption("OptionA"), Times.Once);
        Assert.True(option.IsPresent);
        Assert.Equal(args[1],option.Value);
    }


        [Fact]
    public void InitAll(){
        Mock<ICommandLineArgs> mockArgs = new Mock<ICommandLineArgs>();
        string[] args = new string[]{
            "--optionA", "Value of OptionA",    // option with argument => valid
            "--optionB",                        // option with argument but hasn't got it
            "--optionC",                        // option without argument
            "--rgbgesg",                        // Option unknown
            "-D", "Value of OptionD",           // option with argument => valid
            "-E",                               // option with argument but hasn't got it
            "-F",                               // Option without argument
            "-Z"                                // Option unknown
        };
        mockArgs.SetupGet<string[]>(x => x.Args).Returns(args);
        Mock<ILogger<ICommandLineOptionReader>> mockLogger = new Mock<ILogger<ICommandLineOptionReader>>();
        Mock<ICommandLineOptionStore> mockStore = new Mock<ICommandLineOptionStore>();
        mockStore.Setup(x => x.GetOption(It.IsAny<string>()));


        Dictionary<string, string> AllOptions = new Dictionary<string, string>(){
            {"--optionA", "OptionA"},
            {"-A", "OptionA"},
            {"--optionB", "OptionB"},
            {"-B", "OptionB"},
            {"--optionC", "OptionC"},
            {"-c", "OptionC"},
            {"--optionD", "OptionD"},
            {"-D", "OptionD"},
            {"--optionE", "OptionE"},
            {"-E", "OptionE"},
            {"--optionF", "OptionF"},
            {"-F", "OptionF"},
        };

        CommandLineOption optionA = new CommandLineOption(
            optionName: "OptionA",
            description: "",
            abbreviationCode: "-A",
            optionCode: "--OptionA",
            needed: true,
            NeedArg: true
        );
                CommandLineOption optionB = new CommandLineOption(
            optionName: "OptionB",
            description: "",
            abbreviationCode: "-B",
            optionCode: "--OptionB",
            needed: true,
            NeedArg: true
        );
                CommandLineOption optionC = new CommandLineOption(
            optionName: "OptionC",
            description: "",
            abbreviationCode: "-C",
            optionCode: "--OptionC",
            needed: true,
            NeedArg: false
        );
                CommandLineOption optionD = new CommandLineOption(
            optionName: "OptionD",
            description: "",
            abbreviationCode: "-D",
            optionCode: "--OptionD",
            needed: true,
            NeedArg: true
        );
                CommandLineOption optionE = new CommandLineOption(
            optionName: "OptionE",
            description: "",
            abbreviationCode: "-E",
            optionCode: "--OptionE",
            needed: true,
            NeedArg: true
        );
                CommandLineOption optionF = new CommandLineOption(
            optionName: "OptionF",
            description: "",
            abbreviationCode: "-F",
            optionCode: "--optionF",
            needed: true,
            NeedArg: false
        );
        mockStore.Setup(x => x.GetOptionForConsole()).Returns(AllOptions);

        mockStore.Setup(x => x.GetOption("OptionA")).Returns(optionA);
        mockStore.Setup(x => x.GetOption("OptionB")).Returns(optionB);
        mockStore.Setup(x => x.GetOption("OptionC")).Returns(optionC);
        mockStore.Setup(x => x.GetOption("OptionD")).Returns(optionD);
        mockStore.Setup(x => x.GetOption("OptionE")).Returns(optionE);
        mockStore.Setup(x => x.GetOption("OptionF")).Returns(optionF);


        CommandLineOptionReader reader = new CommandLineOptionReader(mockArgs.Object, mockStore.Object, mockLogger.Object, EventStore);

        reader.Init();
        mockStore.Verify(x => x.GetOption("OptionA"), Times.Once);
        Assert.True(optionA.IsPresent);     // option with arg => valid
        Assert.False(optionB.IsPresent);    // option without arg but need it => not valid
        Assert.True(optionC.IsPresent);     // option without arg = not valid
        Assert.True(optionD.IsPresent);     // option with arg = valid
        Assert.False(optionE.IsPresent);    // option without arg but need it = not valid
        Assert.True(optionF.IsPresent);     // opiton without arg = valid

        Assert.Equal(args[1],optionA.Value);    // option with arg => value is defined
        Assert.Equal(args[6],optionD.Value);    // option with arg => value is definded

        Assert.Null(optionB.Value);     // option without arg but need it => value is null
        Assert.Null(optionC.Value);     // option without arg => valie is null
        Assert.Null(optionE.Value);     // option without arg but need it => value is null
        Assert.Null(optionF.Value);     // option without arg => valie is null


        mockLogger.Verify(x => x.Log(   // For option B => option without arg but need it
        It.Is<LogLevel>(x=> x.Equals(LogLevel.Warning)),
        It.Is<EventId>(x => x.Id == this.EventStore.OptionHasNotArgument),
        It.Is<It.IsAnyType>((x, v)=> x.ToString() == this.EventStore.MessageOfOptionHasNotArgument(args[2])),
        It.IsAny<Exception>(),
        (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()), Times.Once);

        mockLogger.Verify(x => x.Log(   // For option E => option without arg but need it
        It.Is<LogLevel>(x=> x.Equals(LogLevel.Warning)),
        It.Is<EventId>(x => x.Id == this.EventStore.OptionHasNotArgument),
        It.Is<It.IsAnyType>((x, v)=> x.ToString() == this.EventStore.MessageOfOptionHasNotArgument(args[7])),
        It.IsAny<Exception>(),
        (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()), Times.Once);

        mockLogger.Verify(x => x.Log(   // rgbgesg  => option is not define
        It.Is<LogLevel>(x=> x.Equals(LogLevel.Warning)),
        It.Is<EventId>(x=> x == this.EventStore.OPtionIsNotRecongnized),
        It.Is<It.IsAnyType>((x, v)=> x.ToString() == this.EventStore.MessageOfOptionIsNotRecongnized(args[4])),
        It.IsAny<Exception>(),
        (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()), Times.Once);

        mockLogger.Verify(x => x.Log(   // z => option is not define
        It.Is<LogLevel>(x=> x.Equals(LogLevel.Warning)),
        It.Is<EventId>(x=> x == this.EventStore.OPtionIsNotRecongnized),
        It.Is<It.IsAnyType>((x, v)=> x.ToString() == this.EventStore.MessageOfOptionIsNotRecongnized(args[9])),
        It.IsAny<Exception>(),
        (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()), Times.Once);
    }
}