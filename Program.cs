namespace NoteSync;

public class Program {
    private static void Main(string[] args)
    {
        string cmd = args.Length > 0 ? args[0] : "sync";
       
        Config.SetConfig();
        
        Jira jira = new();
        string[] files;

        switch(cmd) {
            case "sync":
                (files, var dir, var sftpEnabled) = GetFiles(true);
                string parentId = Config.Jira.RootPageID;
                foreach (var file in files)
                {
                    string dirChar = Path.DirectorySeparatorChar.ToString();
                    string fileName = file[(file.LastIndexOf(dirChar) + 1)..];
                    string category = file.Replace(dir, "").Replace(fileName, "").Replace(dirChar, "");
                    string subject = fileName.Replace(Config.NoteExtension, "");
                    string content = File.ReadAllText(file);

                    if(!string.IsNullOrEmpty(category))
                    {
                        jira.CreatePage(Config.Jira.RootPageID, category, "", out string id, true);
                        if (!string.IsNullOrEmpty(id))
                            parentId = id;
                        jira.CreatePage(parentId, subject, content, out _);
                        
                    } else
                    {
                        jira.CreatePage(Config.Jira.RootPageID, subject, content, out _);
                    }
                }
                if(sftpEnabled) {
                    Directory.Delete(dir, true);
                }
                break;
            case "list":
                (files, var _, var _) = GetFiles(false);
                foreach(var f in files) {
                    Console.WriteLine(f);
                }
                break;
            case "spaceid":
                if(args.Length < 2) {
                    Console.WriteLine("ERROR: not enough arguments");
                    return;
                }
                Console.WriteLine($"Space ID: {Jira.GetSpaceId(args[1])}");
                break;
            default:
                Console.WriteLine($"COMMAND NOT IMPLEMENTED {cmd}");
                break;
        }
    }    
    private static (string[], string, bool) GetFiles(bool delete) {
        var sftpEnabled = false;
        if (Config.NoteDirectory.StartsWith("sftp://")) {
            if (Config.Sftp == null) {
                Console.WriteLine("ERROR: Sftp directory specified without Sftp appsettings.");
                return ([], "", false);
            }
            Sftp.DownloadFiles(Config.NoteDirectory, delete);
            sftpEnabled = true;

        } else
        if (!Directory.Exists(Config.NoteDirectory))
        {
            Console.WriteLine($"ERROR: Directory does not exist - {Config.NoteDirectory}");
            return ([], "", false);
        }
        var dir = sftpEnabled ? "./notes/" : Config.NoteDirectory;
        var files = Directory.GetFiles(
                        dir, 
                        $"*{Config.NoteExtension}", 
                        SearchOption.AllDirectories);

        return (files, dir, sftpEnabled);
    }
}
    
    

