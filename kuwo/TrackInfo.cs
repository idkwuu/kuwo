using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

public static class TrackInfo
{
    /// <summary>
    /// Fetch track metadata (artist + title) from Kuwo API.
    /// </summary>
    public static async Task<(string artist, string title)> GetAsync(string id)
    {
        try
        {
            using HttpClient client = new HttpClient();

            // Kuwo requires a browser-like header
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

            string url = $"https://www.kuwo.cn/api/www/music/musicInfo?mid={id}";
            string json = await client.GetStringAsync(url);

            using JsonDocument doc = JsonDocument.Parse(json);

            string title = doc.RootElement
                .GetProperty("data")
                .GetProperty("name")
                .GetString();

            string artist = doc.RootElement
                .GetProperty("data")
                .GetProperty("artist")
                .GetString();

            // Remove invalid filename characters
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                title = title.Replace(c, '_');
                artist = artist.Replace(c, '_');
            }

            return (artist, title);
        }
        catch
        {
            return ("UnknownArtist", "UnknownTitle");
        }
    }
}