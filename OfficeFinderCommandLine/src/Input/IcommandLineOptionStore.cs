namespace OfficeFinderCommandLine.Inputs;
/// <summary>
/// <para>Contains the definition of all command line applicaiton option</para>
/// <para>This object is also used to save option define by the user</para>
/// </summary>
public interface ICommandLineOptionStore{
    /// <summary>
    /// the name affected to the Regex option. Used to retrieve it inside the list of option
    /// </summary>
    string REGEXOPTIONNAME { get; }
    /// <summary>
    /// the name affected to the Path option. Used to retrieve it inside the list of option
    /// </summary>
    string PATHOPTIONNAME { get; }
    /// <summary>
    /// the name affected to the Help option. Used to retrieve it inside the list of option
    /// </summary>
    string HELPOPTIONNAME { get; }

    /// <summary>
    /// Return the list of available option for the console
    /// </summary>
    /// <returns>a dictionary of :
    /// <list type="bullet">
    ///     <item>key : code of the option (for example "--option"</item>
    ///     <item>value : Name of the option</item>
    /// </list>
    /// </returns>
    public Dictionary<string,string> GetOptionForConsole();
    /// <summary>
    /// Return the option that have the name define in parameter
    /// </summary>
    /// <param name="name">The name of the option</param>
    /// <returns>The option</returns>
    public CommandLineOption GetOption(string name);
    /// <summary>
    /// Get the name of all option define inside this object
    /// </summary>
    /// <returns>A list of all option name</returns>
    public string[] GetNames();
}