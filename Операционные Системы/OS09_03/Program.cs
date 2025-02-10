using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string filePath = @"C:\Laboratory\Операционные Системы\OS09_03\file.txt";

        try
        {
            insRowFileTxt(filePath, 0, "Вставленный текст в начало");
            insRowFileTxt(filePath, -1, "Вставленный текст в конец");
            insRowFileTxt(filePath, 5, "Вставленный текст в строку 5");
            insRowFileTxt(filePath, 7, "Вставленный текст в строку 7");
            printFileTxt(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void insRowFileTxt(string filePath, int row, string text)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File \"{filePath}\" does not exist.");
            return;
        }

        List<string> lines = new List<string>(File.ReadAllLines(filePath));

        if (row == -1)
        {
            lines.Add(text);
        }
        else if (row <= 0)
        {
            lines.Insert(0, text);
        }
        else if (row > lines.Count)
        {
            lines.Add(text);
        }
        else
        {
            lines.Insert(row - 1, text);
        }

        File.WriteAllLines(filePath, lines);

        Console.WriteLine($"Row {row} has been inserted.");
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
