using Renci.SshNet;

namespace NoteSync;

public class Sftp {
    public static void DownloadFiles(string path) {
        using var sftp = new SftpClient(Config.Sftp!.Hostname, Config.Sftp!.Username, Config.Sftp!.Password);
        try
        {
            sftp.Connect();
            if(!Directory.Exists("./notes/")) {
                Directory.CreateDirectory("./notes/");
            }
            var remoteFiles = sftp.ListDirectory(path.Replace("sftp://", ""));
            foreach (var file in remoteFiles)
            {
                if (file.IsDirectory)
                {
                    continue;
                }
                using (var stream = File.Create($"./notes/{file.Name}"))
                {
                    sftp.DownloadFile(file.FullName, stream);
                }
                sftp.DeleteFile(file.FullName);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: Could not download files from Sftp server {Config.Sftp.Hostname} - {ex}");
        }
        finally
        {
            sftp.Disconnect();
        }
    }
}