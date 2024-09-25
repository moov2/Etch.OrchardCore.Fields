
using System.Text.Json.Serialization;

namespace Etch.OrchardCore.Fields.Eventbrite.Models.Dto
{
    public class EventbriteVenueDto
    {
        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("resource_uri")]
        public string ResourceUri { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("age_restriction")]
        public object AgeRestriction { get; set; }

        [JsonPropertyName("capacity")]
        public object Capacity { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
    }

    public class Address
    {
        [JsonPropertyName("address_1")]
        public string Address1 { get; set; }

        [JsonPropertyName("address_2")]
        public string Address2 { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        [JsonPropertyName("localized_address_display")]
        public string LocalizedAddressDisplay { get; set; }

        [JsonPropertyName("localized_area_display")]
        public string LocalizedAreaDisplay { get; set; }

        [JsonPropertyName("localized_multi_line_address_display")]
        public string[] LocalizedMultiLineAddressDisplay { get; set; }
    }
}
