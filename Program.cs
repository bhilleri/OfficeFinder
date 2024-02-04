using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OfficeFinder;

// Initialization of the dependency injection
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<ICommandLineOPtionReader, CommandLineOPtionReader>();
builder.Services.AddSingleton<IWordSearcher, WordSearcher>();
builder.Services.AddTransient<IProgramManager, ProgramManager>();

using IHost host = builder.Build();

// Launch the program
IProgramManager manager = host.Services.GetRequiredService<IProgramManager>();
manager.Start(args);

host.Dispose();