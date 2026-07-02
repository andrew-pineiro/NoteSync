using System.Text.Json;
using System.Text.Json.Serialization;

namespace NoteSync;

public static class Config
{
    public static JiraConfig Jira { get; private set; } = null!;
    public static SftpConfig? Sftp { get; private set; } = null;
    public static string NoteDirectory { get; private set; } = string.Empty;
    public static string NoteExtension { get; private set; } = string.Empty;
    
    public static void SetConfig()
    {
        string appSettingsPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
        if (!File.Exists(appSettingsPath))
        {
            Console.WriteLine("ERROR: appsettings.json file doesn't exist");
            Environment.Exit(-1);
        }
        try
        {
            var content = File.ReadAllText(appSettingsPath);
            var root = JsonSerializer.Deserialize<AppSettings>(content)
                        ?? throw new InvalidOperationException("Config file was empty or invalid.");

            Jira = root.Jira;
            Sftp = root.Sftp;
            NoteDirectory = root.NoteDirectory;
            NoteExtension = root.NoteExtension;
            
        } catch (Exception e)
        {
            //If the config cannot be set, exit the application and show the error message.
            Console.WriteLine($"ERROR: unable to set config - {e.Message}");
            Environment.Exit(-1);
        }
    }
    private class AppSettings
    {
        public required JiraConfig Jira { get; set; }
        public SftpConfig? Sftp { get; set; }
        public required string NoteDirectory { get; set; }
        public required string NoteExtension { get; set; }
    }
    public class JiraConfig {
        public required string Secret { get; set; }
        public required string Email { get; set; }
        public required string BaseURL { get; set; }
        public required string SpaceID { get; set; }
        public required string RootPageID { get; set; }
    }
    public class SftpConfig {
        public required string Hostname { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}