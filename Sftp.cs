using Renci.SshNet;

namespace NoteSync;

public class Sftp {
    public static void DownloadFiles(string path, bool delete = true, string rootDir = "./notes/") {
        using var sftp = new SftpClient(Config.Sftp!.Hostname, Config.Sftp!.Username, Config.Sftp!.Password);
        try
        {
            sftp.Connect();
            if(!Directory.Exists(rootDir)) {
                Directory.CreateDirectory(rootDir);
            }
            var sftpRootDir = path.Replace("sftp://", "");
            var remoteFiles = sftp.ListDirectory(sftpRootDir);
            foreach (var file in remoteFiles)
            {
                if (file.IsDirectory)
                {
                    var newDir = Path.Join(rootDir, file.Name);
                    Directory.CreateDirectory(newDir);
                    var subDirFiles = sftp.ListDirectory(file.FullName);
                    foreach (var subFile in subDirFiles) {
                        if (subFile.IsDirectory) {
                            var new2Dir = Path.Join(rootDir, file.Name, subFile.Name);
                            Directory.CreateDirectory(newDir);
                            var sub2DirFiles = sftp.ListDirectory(subFile.FullName);
                            foreach(var sub2File in sub2DirFiles) {
                                if(sub2File.IsDirectory) {
                                    continue;
                                }
                                if (Path.GetExtension(sub2File.Name) != Config.NoteExtension) {
                                    continue;
                                }
                                using (var stream = File.Create(Path.Join(new2Dir, sub2File.Name))) {
                                  sftp.DownloadFile(sub2File.FullName, stream);  
                                }
                                if (delete)
                                    sftp.DeleteFile(sub2File.FullName);
                            }
                            continue;
                        }
                        if (Path.GetExtension(subFile.Name) != Config.NoteExtension) {
                            continue;
                        }
                        using (var stream = File.Create(Path.Join(newDir, subFile.Name))) {
                            sftp.DownloadFile(subFile.FullName, stream);
                        }
                        if (delete)
                            sftp.DeleteFile(subFile.FullName);
                    }
                    continue;
                }
                if (Path.GetExtension(file.Name) != Config.NoteExtension) {
                    continue;
                }
                using (var stream = File.Create(Path.Join(rootDir, file.Name)))
                {
                    sftp.DownloadFile(file.FullName, stream);
                }
                if (delete)
                    sftp.DeleteFile(file.FullName);
            }
        }
        catch (DirectoryNotFoundException) {
            
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