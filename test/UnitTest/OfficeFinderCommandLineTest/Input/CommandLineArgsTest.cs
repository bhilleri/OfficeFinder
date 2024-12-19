using System.Data;
using System.Runtime.CompilerServices;
using OfficeFinderCommandLine.Inputs;

namespace OfficeFinderCommandLineTest.Inputs;

public class CommandLineArgsTest
{
    [Fact]
    public void ArgsGetterTest(){
        const string string1 = "string1";
        const string string2 = "string2";
        const string string3 = "string3";
        const string string4 = "string4";
        string[] args = new string[]{
            string1, string2, string3, string4
        };
        CommandLineArgs testedCommandLineArgs = new CommandLineArgs(args);
        Assert.Equal(args, testedCommandLineArgs.Args);
        Assert.Equal(string1, testedCommandLineArgs.Args[0]);
        Assert.Equal(string2, testedCommandLineArgs.Args[1]);
        Assert.Equal(string3, testedCommandLineArgs.Args[2]);
        Assert.Equal(string4, testedCommandLineArgs.Args[3]);
        Assert.Equal(4, testedCommandLineArgs.Args.Length);
    }

}