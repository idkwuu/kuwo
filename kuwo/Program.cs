using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Console Title
        Console.Title = "Kuwo | Made by ＳΛＲＴΞ";

        // Show ASCII
        Ascii.ASCII();

        string id;
        string filename;
        string format = "mp3"; // Feature: Changeable music format

        if (args.Length < 2)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[SYS]: Enter the music ID: ");
            id = Console.ReadLine();

            Console.Write("[SYS]: Enter the filename: ");
            filename = Console.ReadLine();
        }
        else
        {
            id = args[0];
            filename = args[1];
        }

        string file = $"{filename}.{format}";
        string fullPath = Path.GetFullPath(file);

        string url = URLString.Build(id, format);

        Console.WriteLine("[SYS]: URL: " + url);
        Console.WriteLine("[SYS]: File: " + fullPath);

        bool success = await task.DownloadAsync(url, fullPath);

        if (success)
            Console.WriteLine("[SYS]: Download successful! Launching File Explorer..."); // open explorer .....
        else
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[SYS]: Download failed.");

        Console.ReadKey(true);
    }
}
