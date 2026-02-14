using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

class Program
{
    static async Task Main(string[] args)
    {

        // ASCII: 
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(@"
 ██ ▄█▀ █    ██  █     █░ ▒█████  
 ██▄█▒  ██  ▓██▒▓█░ █ ░█░▒██▒  ██▒
▓███▄░ ▓██  ▒██░▒█░ █ ░█ ▒██░  ██▒
▓██ █▄ ▓▓█  ░██░░█░ █ ░█ ▒██   ██░
▒██▒ █▄▒▒█████▓ ░░██▒██▓ ░ ████▓▒░
▒ ▒▒ ▓▒░▒▓▒ ▒ ▒ ░ ▓░▒ ▒  ░ ▒░▒░▒░ 
░ ░▒ ▒░░░▒░ ░ ░   ▒ ░ ░    ░ ▒ ▒░ 
░ ░░ ░  ░░░ ░ ░   ░   ░  ░ ░ ░ ▒  
░  ░      ░         ░        ░ ░  
                                  ");

        string id;
        string filename;
        string format = "mp3"; // Standard one

        // Arguments: 
        if (args.Length < 2)
        {
            Console.ForegroundColor = ConsoleColor.White; // Change color back to white
            Console.Write("[SYS]: Enter the music ID: ");
            id = Console.ReadLine();

            Console.Write("[SYS]: Enter the filename:");
            filename = Console.ReadLine();
        }
        else
        {
            id = args[0];
            filename = args[1];
        }

        string file = $"{filename}.{format}";
        string fullPath = Path.GetFullPath(file);

        // URL
        string url = $"http://antiserver.kuwo.cn/anti.s?format={format}&rid=MUSIC_{id}";

        Console.WriteLine("[SYS]: URL: " + url);
        Console.WriteLine("[SYS]: File: " + fullPath);

        try
        {
            using HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("[SYS]: Download failed: " + response.StatusCode);
            }
            else
            {
                byte[] data = await response.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync(fullPath, data);
                Console.WriteLine("[SYS]: Download completed!");

                // Open the folder
                Process.Start(new ProcessStartInfo()
                {
                    FileName = "explorer.exe",
                    Arguments = $"/select,\"{fullPath}\"",
                    UseShellExecute = true
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadKey(true);
    }
}
