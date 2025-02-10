using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string filePath = @"C:\Laboratory\Операционные Системы\OS09_02\file.txt";

        try
        {
            printFileTxt(filePath);

            delRowFileTxt(filePath, 1);

            delRowFileTxt(filePath, 3);

            delRowFileTxt(filePath, 8);

            delRowFileTxt(filePath, 10);
            printFileTxt(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void delRowFileTxt(string filePath, int row)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File \"{filePath}\" does not exist.");
            return;
        }

        List<string> lines = new List<string>(File.ReadAllLines(filePath));

        if (row < 1 || row > lines.Count)
        {
            Console.WriteLine($"Row {row} does not exist in the file.");
            return;
        }


        lines.RemoveAt(row - 1);

        File.WriteAllLines(filePath, lines);

        Console.WriteLine($"Row {row} has been deleted.");
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
