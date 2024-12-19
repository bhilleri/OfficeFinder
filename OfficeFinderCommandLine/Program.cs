using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OfficeFinderLibrary;
using OfficeFinderLibrary.Data;
using OfficeFinderLibrary.Inputs;
using OfficeFinderCommandLine.Inputs;
using OfficeFinderLibrary.Inputs.FilesTools;
using OfficeFinderLibrary.Output;
using OfficeFinderCommandLine.Output;
using OfficeFinderLibrary.Searcher;
using OfficeFinderCommandLine;
using OfficeFinderCommandLine.src;

// Initialization of the dependency injection
HostApplicationBuilder builder = Host.CreateApplicationBuilder();

builder.Services.AddLogging(builder => builder.AddConsole());
builder.Services.AddSingleton<IConsoleWrapper,ConsoleWrapper>();
builder.Services.AddSingleton<IEventIDOfficeFinderCommandLIne, EventIDOfficeFInderCommandLine>();
builder.Services.AddSingleton<ICommandLineArgs>(new CommandLineArgs(args));
builder.Services.AddSingleton<ICommandLineOptionStore, CommandLineOptionStore>();
builder.Services.AddSingleton<IHelper, ConsoleHelper>();
builder.Services.AddSingleton<IInputErrorManager, CommandLineInputErrorManager>();
builder.Services.AddSingleton<ICommandLineOptionReader, CommandLineOptionReader>();
builder.Services.AddSingleton<IOptionProvider, CommandLineOptionProvider>();
builder.Services.AddSingleton<IFileFilter, FileFilter>();
builder.Services.AddSingleton<IDirectoryExplorer, DirectoryExplorer>();
builder.Services.AddSingleton<IFileFinderManager, FileFinderManager>();
builder.Services.AddSingleton<IOccurenceStore, OccurenceStore>();
builder.Services.AddSingleton<ErrorSignification>();
builder.Services.AddSingleton<IOutput, CommandLinePrinter>();
builder.Services.AddSingleton<IOptionManager, OptionManger>();
builder.Services.AddSingleton<ISearcher, WordSearcher>();
builder.Services.AddSingleton<IProgramManager, ProgramManager>();

using IHost host = builder.Build();

// Launch the program
IProgramManager manager = host.Services.GetRequiredService<IProgramManager>();
manager.Start();

host.Dispose();