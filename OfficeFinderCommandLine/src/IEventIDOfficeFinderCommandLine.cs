using Microsoft.Extensions.Logging;

namespace OfficeFinderCommandLine;

public interface IEventIDOfficeFinderCommandLIne{
    public EventId OPtionIsNotRecongnized { get; }
    public EventId OptionHasNotArgument { get; }
    public string MessageOfOptionIsNotRecongnized(string option);
    public string MessageOfOptionHasNotArgument(string option);
}