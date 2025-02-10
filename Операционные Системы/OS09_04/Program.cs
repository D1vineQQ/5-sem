using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string filePath = @"C:\Laboratory\Операционные Системы\OS09_03\file.txt";
        int mlsec = 30000; // 30 секунд

        try
        {
            printWatchRowFileTxt(filePath, mlsec);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void printWatchRowFileTxt(string filePath, int mlsec)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File \"{filePath}\" does not exist.");
            return;
        }

        string directory = Path.GetDirectoryName(filePath);
        string fileName = Path.GetFileName(filePath);

        // Настройка наблюдателя за каталогом
        using (FileSystemWatcher watcher = new FileSystemWatcher(directory))
        {
            watcher.Filter = fileName;
            watcher.NotifyFilter = NotifyFilters.LastWrite;

            watcher.Changed += (sender, e) =>
            {
                try
                {
                    System.Threading.Thread.Sleep(100);
                    int lineCount = File.ReadAllLines(filePath).Length;
                    Console.WriteLine($"File changed. Current line count: {lineCount}");
                }
                catch (IOException ioEx)
                {
                    Console.WriteLine($"File access error: {ioEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            };

            watcher.EnableRaisingEvents = true;

            Console.WriteLine($"Watching for changes in file \"{fileName}\" for {mlsec / 1000} seconds...");
            System.Threading.Thread.Sleep(mlsec); // Наблюдаем в течение указанного времени
        }
    }

}
