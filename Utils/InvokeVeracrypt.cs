using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace VeracryptGui.Utils
{
    public class VecryptUtil
    {
        // 读取配置文件中Veracrypt执行文件路径
        private static string configExePath = ConfigurationManager.AppSettings["ExePath"];

        private static string folderPath = Path.Combine(Directory.GetCurrentDirectory(), configExePath);

        public static async Task CreateEncryptedDisk(string volumePath, string password, string size = "20M")
        {
            string exeFullPath = Path.Combine(folderPath, "VeraCryptFormat.exe");
            if (Directory.Exists(folderPath))
            {
                await Task.Run(() =>
                {
                    Process process = new Process();
                    string arguments = $"/create {volumePath} /password {password} /size {size}" +
                        $" /hash sha-512 /encryption serpent /filesystem NTFS /force /silent";
                    ProcessStartInfo startInfo = new ProcessStartInfo(exeFullPath, arguments);
                    process.StartInfo = startInfo;
                    process.Start();
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardOutput = true;
                    process.WaitForExit();
                });
            }
        }

        public static void MountEncryptDisk(string volumePath, string password, string dirveLetter)
        {
            //VeraCrypt.exe /volume testv1.disk /letter o /password 123456 /quit /silent
            string exeFullPath = Path.Combine(folderPath, "VeraCrypt.exe");
            if (Directory.Exists(folderPath) && File.Exists(volumePath))
            {
                Process process = new Process();
                string arguments = $"/volume {volumePath} /letter {dirveLetter} /password {password} /quit";
                ProcessStartInfo startInfo = new ProcessStartInfo(exeFullPath, arguments);
                process.StartInfo = startInfo;
                process.Start();
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.WaitForExit();
            }
        }

        public static void UnMountEncryptDisk(string dirveLetter)
        {
            string exeFullPath = Path.Combine(folderPath, "VeraCrypt.exe");
            if (File.Exists(exeFullPath))
            {
                Process process = new Process();
                string arguments = $"/dismount {dirveLetter} /force /quit /silent";
                ProcessStartInfo startInfo = new ProcessStartInfo(exeFullPath, arguments);
                process.StartInfo = startInfo;
                process.Start();
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                process.WaitForExit();
            }
        }
    }
}