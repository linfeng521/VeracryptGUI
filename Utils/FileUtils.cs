using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VeracryptGui.Utils
{
    public class DriveDiskUtils
    {
        // 遍历所有可用的盘符，找到空闲的盘符
        public static List<string> GetFreeDriveLetters()
        {
            var localDrives = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed).Select(d => d.Name[0]);
            var allDrives = DriveInfo.GetDrives().Select(d => d.Name[0]);
            var networkDrives = allDrives.Except(localDrives).Where(d => d >= 'A' && d <= 'Z');

            var freeDrives = new List<string>();

            //从盘符C开始
            for (char c = 'C'; c <= 'Z'; c++)
            {
                if (!allDrives.Contains(c))
                {
                    // 该驱动器没有被分配
                    //freeDrives.Add($"{c}:\\");
                    freeDrives.Add($"{c}");
                }
                else if (networkDrives.Contains(c))
                {
                    // 当前是网络驱动器，测试共享目录是否有效，连接字符必须使用反斜杠
                    //var path = $"{c}:\\";
                    var path = $"{c}";
                    if (!Directory.Exists(path) && !File.Exists(path))
                    {
                        freeDrives.Add(path);
                    }
                }
            }

            return freeDrives;
        }
    }

    public class FileUtils
    {
        /// <summary>
        /// 根据文件字节大小返回动态的文件大小
        /// </summary>
        /// <param name="fileSizeInBytes">Long 文件字节大小</param>
        /// <returns></returns>
        public static string GetFileSize(long fileSizeInBytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" }; //2 GB
            int order = 0;
            double size = fileSizeInBytes;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }

            string result = $"{size:0.#} {sizes[order]}";

            Console.WriteLine($"File size: {result}");// Output: File size: 1.9 GB
            return result;
        }

        public static string GetFileSize(FileInfo file)
        {
            return GetFileSize(file.Length);
        }

        public static string GetFileSize(string filename)
        {
            FileInfo fileInfo = new FileInfo(filename);
            return GetFileSize(fileInfo);
        }
    }
}