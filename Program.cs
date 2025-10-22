namespace NoteSync;

public class Program {
    private static void Main()
    {
        Config.SetConfig();
        Jira jira = new();
        if (!Directory.Exists(Config.NoteDirectory))
        {
            Console.WriteLine($"ERROR: Directory does not exist - {Config.NoteDirectory}");
            return;
        }
        
        var files = Directory.GetFiles(Config.NoteDirectory, $"*{Config.NoteExtension}", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            string dirChar = Path.DirectorySeparatorChar.ToString();
            string fileName = file.Substring(file.LastIndexOf(dirChar) + 1);

            //TODO: look to use the category as the parent page
            string category = file.Replace(Config.NoteDirectory, "").Replace(fileName, "").Replace(dirChar, "");

            string subject = fileName.Replace(Config.NoteExtension!, "");
            string content = File.ReadAllText(file);
            
            //TODO: pull parent page id from above category
            jira.CreatePage("1325105153", subject, content);
        }
        
    }    
}
    
    

