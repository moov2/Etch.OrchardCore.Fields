using System.Text.Json.Serialization;

namespace Etch.OrchardCore.Fields.ResponsiveMedia.Models
{
    public class ResponsiveMediaSource
    {
        [JsonPropertyName("breakpoint")]
        public int? Breakpoint { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
