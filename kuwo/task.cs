using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

public static class task
{
    public static async Task<bool> DownloadAsync(string url, string fullPath)
    {
        try
        {
            using HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return false;

            byte[] data = await response.Content.ReadAsByteArrayAsync();

            // Write downloaded music file
            await File.WriteAllBytesAsync(fullPath, data);

            // Open File Explorer and select the downloaded file
            Process.Start(new ProcessStartInfo()
            {
                FileName = "explorer.exe",
                Arguments = $"/select,\"{fullPath}\"",
                UseShellExecute = true
            });

            return true;
        }
        catch
        {
            return false;
        }
    }
}