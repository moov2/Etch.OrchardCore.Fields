using System.Text.Json.Serialization;

namespace Etch.OrchardCore.Fields.Eventbrite.Models.Dto
{
    public class EventbriteDescriptionDto
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
