namespace OfficeFinderCommandLine.Inputs;

/// <summary>
/// Retrieve options define by user and save them inside the <see cref="ICommandLineOptionStore"/> 
/// </summary>
public interface ICommandLineOptionReader{
    /// <summary>
    /// <inheritdoc cref="ICommandLineOptionReader"/>
    /// </summary>
    public void Init();
}