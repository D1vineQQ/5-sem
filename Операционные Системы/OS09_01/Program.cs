using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {

        string filePath = @"C:\Laboratory\Операционные Системы\OS09_01\file.txt";

        try
        {
            printFileInfo(filePath);
            printFileTxt(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    static void printFileInfo(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File \"{filePath}\" does not exist.");
            return;
        }

        FileInfo fileInfo = new FileInfo(filePath);
        Console.WriteLine("File Information:");
        Console.WriteLine($"- Name: {fileInfo.Name}");
        Console.WriteLine($"- Type: {fileInfo.Extension}");
        Console.WriteLine($"- Size: {fileInfo.Length} bytes");
        Console.WriteLine($"- Creation Time: {fileInfo.CreationTime}");
        Console.WriteLine($"- Last Updated: {fileInfo.LastWriteTime}");
    }


    static void printFileTxt(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File \"{filePath}\" does not exist.");
            return;
        }

        Console.WriteLine("\nFile Content:");
        string content = File.ReadAllText(filePath);
        Console.WriteLine(content);
    }
}
