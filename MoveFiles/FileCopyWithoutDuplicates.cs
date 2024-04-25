using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

class FileCopyWithoutDuplicates
{
    static string sourceDir = @"D:\source_path";
    static string destinationDir = @"D:\destination_path";

    // ฟังก์ชันหา hash ของไฟล์
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
    /*
    static void Main(string[] args)
    {
        string[] sourceFiles = Directory.GetFiles(sourceDir);
        List<string> copiedFiles = new List<string>();

        // ตรวจสอบว่าไฟล์ใน destination path มีอยู่หรือไม่
        if (Directory.Exists(destinationDir))
        {
            string[] copiedFilePaths = Directory.GetFiles(destinationDir);
            foreach (string copiedFilePath in copiedFilePaths)
            {
                copiedFiles.Add(Path.GetFileName(copiedFilePath));
            }
        }

        foreach (string sourceFilePath in sourceFiles)
        {
            string fileName = Path.GetFileName(sourceFilePath);
            string destinationFilePath = Path.Combine(destinationDir, fileName);

            // ตรวจสอบว่าไฟล์ถูกคัดลอกไปแล้วหรือไม่
            if (!copiedFiles.Contains(fileName))
            {
                // คำนวณ hash ของไฟล์
                string fileHash = CalculateFileHash(sourceFilePath);

                // ตรวจสอบว่าไฟล์ที่มี hash เดียวกันอยู่ใน destination path หรือไม่
                if (!File.Exists(destinationFilePath) || CalculateFileHash(destinationFilePath) != fileHash)
                {
                    // ถ้าไม่มีหรือ hash ไม่ตรงกัน ให้คัดลอกไฟล์
                    File.Copy(sourceFilePath, destinationFilePath, true);
                    Console.WriteLine("File copied: " + fileName);
                    copiedFiles.Add(fileName); // เพิ่มชื่อไฟล์ลงใน List เพื่อแสดงว่าไฟล์นี้ถูกคัดลอกไปแล้ว
                }
                else
                {
                    Console.WriteLine("File already exists in destination: " + fileName);
                }
            }
            else
            {
                Console.WriteLine("File already copied before: " + fileName);
            }
        }
    }*/
}
