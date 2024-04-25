using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

class CodeCopyFile
{
    static string sourceDir = @"D:\repo\MoveFiles\in";
    static string destinationDir = @"D:\repo\MoveFiles\out";
    static DateTime lastCheckTime = DateTime.Now;

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
        string[] destinationFiles = Directory.GetFiles(destinationDir);

        foreach (string sourceFilePath in sourceFiles)
        {
            string fileName = Path.GetFileName(sourceFilePath);

            // Check if the file exists in the destination directory by name
            if (Array.IndexOf(destinationFiles, Path.Combine(destinationDir, fileName)) == -1)
            {
                string sourceFileHash = CalculateFileHash(sourceFilePath);

                string[] destinationFileHashes = new string[destinationFiles.Length];
                for (int i = 0; i < destinationFiles.Length; i++)
                {
                    destinationFileHashes[i] = CalculateFileHash(destinationFiles[i]);
                }

                // Check if the file exists in the destination directory by hash
                if (Array.IndexOf(destinationFileHashes, sourceFileHash) == -1)
                {
                    File.Copy(sourceFilePath, Path.Combine(destinationDir, fileName));
                    Console.WriteLine("New file copied: " + fileName);
                }
            }
        }
        lastCheckTime = DateTime.Now;
    }

    /*
    public static void Main(string[] args)
    {
        while (true)
        {
            CopyNewFiles();
            Thread.Sleep(60); // 3600 seconds = 1 hour
        }
    }
    */
}
