using System.Text.Json;
using System.Text.Json.Serialization;

namespace NoteSync;

public class Config
{
    /*
    Public config properties used throughout the lifetime of the application
    */
    public static string JiraSecret { get; private set; } = string.Empty;
    public static string JiraEmail { get; private set; } = string.Empty;
    public static string JiraBaseURL { get; private set; } = string.Empty;
    public static string JiraSpaceID { get; private set; } = string.Empty;
    public static string JiraRootPageID { get; private set; } = string.Empty;
    public static string NoteDirectory { get; private set; } = string.Empty;
    public static string NoteExtension { get; private set; } = string.Empty;

    /*
    Private config properties only used in this instance of Config to read from the appsettings.json file
    */
    [JsonPropertyName("JiraSecret")]
    public required string _jiraSecret { private get; set; }
    [JsonPropertyName("JiraEmail")]
    public required string _jiraEmail { private get; set; }
    [JsonPropertyName("JiraBaseURL")]
    public required string _jiraBaseURL { private get; set; }
    [JsonPropertyName("JiraSpaceID")]
    public required string _jiraSpaceID { private get; set; }
    [JsonPropertyName("JiraRootPageID")]
    public required string _jiraRootPageID { private get; set; }
    [JsonPropertyName("NoteDirectory")]
    public required string _noteDirectory { private get; set; }
    [JsonPropertyName("NoteExtension")]
    public required string _noteExtension { private get; set; }
    
    
    public static void SetConfig()
    {
        string appSettings = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
        if (!Path.Exists(appSettings))
        {
            Console.WriteLine("ERROR: appsettings.json file doesn't exist");
        }
        var content = File.ReadAllText(appSettings);
        try
        {
            var results = JsonSerializer.Deserialize<Config>(content);
            JiraSecret = results!._jiraSecret;
            JiraBaseURL = results!._jiraBaseURL;
            JiraEmail = results!._jiraEmail;
            JiraSpaceID = results!._jiraSpaceID;
            JiraRootPageID = results!._jiraRootPageID;
            NoteDirectory = results!._noteDirectory;
            NoteExtension = results!._noteExtension;
            
        } catch (Exception e)
        {
            //If the config cannot be set, exit the application and show the error message.
            Console.WriteLine($"ERROR: unable to set config - {e.Message}");
            Environment.Exit(-1);
        }

    }
}