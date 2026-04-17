using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Console Title
        Console.Title = "Kuwo | Made by ＳΛＲＴΞ";

        // Show ASCII banner
        Ascii.ASCII();

        string id;
        string format = "mp3"; // Feature: Changeable music format

        // Ask for music ID
        if (args.Length < 1)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[SYS]: Enter the music ID: ");
            id = Console.ReadLine();
        }
        else
        {
            id = args[0];
        }

        Console.WriteLine("[SYS]: Fetching track information...");

        // Fetch metadata (artist + title)
        var track = await TrackInfo.GetAsync(id);

        // Build automatic filename
        string filename = $"{track.artist} - {track.title}";
        string file = $"{filename}.{format}";

        // Default path logic (your original code)
        string fullPath = Path.GetFullPath(file);

        // Get user's default Music folder
        string musicFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

        // Combine Music folder with filename
        fullPath = Path.Combine(musicFolder, file);

        // Build download URL
        string url = URLString.Build(id, format);

        Console.WriteLine("[SYS]: URL: " + url);
        Console.WriteLine("[SYS]: File: " + fullPath);

        // Download music
        bool success = await task.DownloadAsync(url, fullPath);

        if (success)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[SYS]: Download successful! Launching File Explorer...");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[SYS]: Download failed.");
        }

        Console.ReadKey(true);
    }
}