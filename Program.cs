using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OfficeFinder;
using OfficeFinder.Data;
using OfficeFinder.Inputs;
using OfficeFinder.Output;
using OfficeFinder.Output.CommandLine;
using OfficeFinder.Searcher;

// Initialization of the dependency injection
HostApplicationBuilder builder = Host.CreateApplicationBuilder();


builder.Services.AddSingleton<IOptionManager>(new OptionManager(args));
builder.Services.AddSingleton<IOccurenceStore, OccurenceStore>();
builder.Services.AddSingleton<ErrorSignification>();
builder.Services.AddSingleton<IOutput, CommandLinePrinter>();
builder.Services.AddSingleton<ICommandLineOPtionReader, CommandLineOPtionReader>();
builder.Services.AddSingleton<ISearcher, WordSearcher>();
builder.Services.AddSingleton<IProgramManager, ProgramManager>();

using IHost host = builder.Build();

// Launch the program
IProgramManager manager = host.Services.GetRequiredService<IProgramManager>();
manager.Start();

host.Dispose();