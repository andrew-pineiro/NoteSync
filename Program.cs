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
        string parentId = Config.JiraRootPageID;
        foreach (var file in files)
        {
            string dirChar = Path.DirectorySeparatorChar.ToString();
            string fileName = file.Substring(file.LastIndexOf(dirChar) + 1);
            string category = file.Replace(Config.NoteDirectory, "").Replace(fileName, "").Replace(dirChar, "");
            string subject = fileName.Replace(Config.NoteExtension, "");
            string content = File.ReadAllText(file);

            if(!string.IsNullOrEmpty(category))
            {
                jira.CreatePage(Config.JiraRootPageID, category, "", out string id);
                if (!string.IsNullOrEmpty(id))
                    parentId = id;
                jira.CreatePage(parentId, subject, content, out _);
                
            } else
            {
                jira.CreatePage(Config.JiraRootPageID, subject, content, out _);
            }
        }
        
    }    
}
    
    

