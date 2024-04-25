using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

class FileCopyScheduler
{
    static string sourceDir = @"D:\repo\MoveFiles\in";
    static string destinationDir = @"D:\repo\MoveFiles\out";

    static string CalculateFileHash(string filePath)
    {
        using (var sha256 = SHA256.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hashBytes = sha256.ComputeHash(stream);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }

    static void CopyNewFiles()
    {
        string[] sourceFiles = Directory.GetFiles(sourceDir);

        foreach (string sourceFilePath in sourceFiles)
        {
            string fileName = Path.GetFileName(sourceFilePath);
            string destinationFilePath = Path.Combine(destinationDir, fileName);

            if (!File.Exists(destinationFilePath) || CalculateFileHash(destinationFilePath) != CalculateFileHash(sourceFilePath))
            {
                File.Copy(sourceFilePath, destinationFilePath, true);
                Console.WriteLine("File copied: " + fileName);
            }
        }
    }

    public static void Main(string[] args)
    {
        while (true)
        {
            CopyNewFiles();
            Thread.Sleep(300); // 3600 วินาที = 1 ชั่วโมง
        }
    }
}
