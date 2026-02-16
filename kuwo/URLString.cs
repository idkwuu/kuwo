using System;

public static class URLString
{
    public static string Build(string id, string format)
    {
        // Example: http://antiserver.kuwo.cn/anti.s?format=mp3&rid=MUSIC_123456&type=convert_url&response=res

        return $"http://antiserver.kuwo.cn/anti.s?format={format}&rid=MUSIC_{id}&type=convert_url&response=res";
    }
}
