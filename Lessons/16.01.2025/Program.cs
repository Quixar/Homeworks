using System;
using System.Diagnostics;

internal class Program
{
    static void Main(string[] args)
    {
        // string classPath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        // if (!File.Exists(classPath))
        // {
        //     Console.WriteLine($"Not found: {classPath}");
        // }
        // ProcessStartInfo s = new ProcessStartInfo()
        // {
        //     FileName = classPath,
        //     Arguments = "--new-window http://www.google.com",
        //     CreateNoWindow = false,
        //     UseShellExecute = true,
        // };
        // try
        // {
        //     Process process = Process.Start(s);
        //     Console.WriteLine("Успешно выполнен.");
        //     Console.WriteLine($"ID: {process.Id}");
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine(ex.Message);
        // }
        //Process process = new Process();
        //process.StartInfo.FileName = classPath;
        //process.Start();

        // Console.WriteLine("Запуск основного процесса");
        // Thread threadFirst = new Thread(() => 
        // {
        //     for(int i = 0; i < 10; i++)
        //     {
        //         Console.WriteLine($"Поток 1: {i}");
        //         Thread.Sleep(500);
        //     }
        // });

        // Thread threadSecond = new Thread(() =>
        // {
        //     for (int i = 0; i < 10; i++)
        //     {
        //         Console.WriteLine($"Поток 2: {i}");
        //         Thread.Sleep(750);
        //     }
        // });

        // threadFirst.Start();
        // threadSecond.Start();

        // threadFirst.Join();
        // threadSecond.Join();
        
        string url = "https://fmi.unibuc.ro";
        try
        {
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = "open",
                Arguments = url,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            Process process = Process.Start(start);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
        }


    }
}