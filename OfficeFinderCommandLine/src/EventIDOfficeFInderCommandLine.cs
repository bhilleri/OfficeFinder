using Microsoft.Extensions.Logging;

namespace OfficeFinderCommandLine;

public class EventIDOfficeFInderCommandLine : IEventIDOfficeFinderCommandLIne{
    public EventId OPtionIsNotRecongnized { get; } = new(10000, "OptionIsNotRecognized");
    public EventId OptionHasNotArgument { get; } = new(10001, "OptionHasNotArg");
    public string MessageOfOptionIsNotRecongnized(string option){
        return $"L'option {option} n'est pas définie dans la liste des options disponibles";
    }

    public string MessageOfOptionHasNotArgument(string option){
        return $"L'option {option} est renseignée sans argument (nécessaire pour cette option)";
    }

}