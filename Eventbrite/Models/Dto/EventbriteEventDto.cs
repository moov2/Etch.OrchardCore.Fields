using System;
using System.Text.Json.Serialization;

namespace Etch.OrchardCore.Fields.Eventbrite.Models.Dto
{
    public class EventbriteEventDto
    {
        [JsonPropertyName("name")]
        public Name Name { get; set; }

        [JsonPropertyName("description")]
        public Description Description { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("vanity_url")]
        public string VanityUrl { get; set; }

        [JsonPropertyName("start")]
        public Start Start { get; set; }

        [JsonPropertyName("end")]
        public End End { get; set; }

        [JsonPropertyName("organization_id")]
        public string OrganizationId { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("changed")]
        public DateTime Changed { get; set; }

        [JsonPropertyName("published")]
        public DateTime Published { get; set; }

        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; }

        [JsonPropertyName("capacity_is_custom")]
        public bool? CapacityIsCustom { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("listed")]
        public bool Listed { get; set; }

        [JsonPropertyName("shareable")]
        public bool Shareable { get; set; }

        [JsonPropertyName("invite_only")]
        public bool InviteOnly { get; set; }

        [JsonPropertyName("online_event")]
        public bool OnlineEvent { get; set; }

        [JsonPropertyName("show_remaining")]
        public bool ShowRemaining { get; set; }

        [JsonPropertyName("tx_time_limit")]
        public int TxTimeLimit { get; set; }

        [JsonPropertyName("hide_start_date")]
        public bool HideStartDate { get; set; }

        [JsonPropertyName("hide_end_date")]
        public bool HideEndDate { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("is_locked")]
        public bool IsLocked { get; set; }

        [JsonPropertyName("privacy_setting")]
        public string PrivacySetting { get; set; }

        [JsonPropertyName("is_series")]
        public bool IsSeries { get; set; }

        [JsonPropertyName("is_series_parent")]
        public bool IsSeriesParent { get; set; }

        [JsonPropertyName("inventory_type")]
        public string InventoryType { get; set; }

        [JsonPropertyName("is_reserved_seating")]
        public bool IsReservedSeating { get; set; }

        [JsonPropertyName("show_pick_a_seat")]
        public bool ShowPickASeat { get; set; }

        [JsonPropertyName("show_seatmap_thumbnail")]
        public bool ShowSeatmapThumbnail { get; set; }

        [JsonPropertyName("show_colors_in_seatmap_thumbnail")]
        public bool ShowColorsInSeatmapThumbnail { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("is_free")]
        public bool IsFree { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("logo_id")]
        public string LogoId { get; set; }

        [JsonPropertyName("organizer_id")]
        public string OrganizerId { get; set; }

        [JsonPropertyName("venue_id")]
        public string VenueId { get; set; }

        [JsonPropertyName("category_id")]
        public string CategoryId { get; set; }

        [JsonPropertyName("subcategory_id")]
        public object SubcategoryId { get; set; }

        [JsonPropertyName("format_id")]
        public string FormatId { get; set; }

        [JsonPropertyName("resource_uri")]
        public string ResourceUri { get; set; }

        [JsonPropertyName("is_externally_ticketed")]
        public bool IsExternallyTicketed { get; set; }

        [JsonPropertyName("logo")]
        public Logo Logo { get; set; }
    }

    public class Name
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("html")]
        public string Html { get; set; }
    }

    public class Description
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("html")]
        public string Html { get; set; }
    }

    public class Start
    {
        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("local")]
        public DateTime Local { get; set; }

        [JsonPropertyName("utc")]
        public DateTime Utc { get; set; }
    }

    public class End
    {
        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("local")]
        public DateTime Local { get; set; }

        [JsonPropertyName("utc")]
        public DateTime Utc { get; set; }
    }

    public class Logo
    {
        [JsonPropertyName("crop_mask")]
        public CropMask CropMask { get; set; }

        [JsonPropertyName("original")]
        public Original Original { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("aspect_ratio")]
        public string AspectRatio { get; set; }

        [JsonPropertyName("edge_color")]
        public object EdgeColor { get; set; }

        [JsonPropertyName("edge_color_set")]
        public bool EdgeColorSet { get; set; }
    }

    public class CropMask
    {
        [JsonPropertyName("top_left")]
        public TopLeft TopLeft { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }

    public class TopLeft
    {
        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }
    }

    public class Original
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }
}
