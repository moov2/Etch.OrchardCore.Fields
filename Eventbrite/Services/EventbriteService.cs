using Etch.OrchardCore.Fields.Eventbrite.Models.Dto;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Fields.Eventbrite.Services
{
    public class EventbriteService : IEventbriteService
    {
        #region Constants

        private const string EventsUrl = "https://www.eventbriteapi.com/v3/events";
        private const string VenuesUrl = "https://www.eventbriteapi.com/v3/venues";

        #endregion

        #region Dependencies

        private readonly IHttpClientFactory _clientFactory;
        private readonly IEventbriteSettingsService _eventbriteSettingsService;

        #endregion

        #region Constructor

        public EventbriteService(IHttpClientFactory clientFactory, IEventbriteSettingsService eventbriteSettingsService)
        {
            _clientFactory = clientFactory;
            _eventbriteSettingsService = eventbriteSettingsService;
        }

        #endregion

        #region Implementation

        public async Task<EventbriteEventDto> GetEventAsync(string eventId)
        {
            var settings = await _eventbriteSettingsService.GetSettingsAsync();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{EventsUrl}/{eventId}/");
            var client = _clientFactory.CreateClient();

            request.Headers.Add("Authorization", string.Format("Bearer {0}", settings.PrivateToken));

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return JConvert.DeserializeObject<EventbriteEventDto>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<EventbriteDescriptionDto> GetDescriptionAsync(string eventId)
        {
            var settings = await _eventbriteSettingsService.GetSettingsAsync();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{EventsUrl}/{eventId}/description/");
            var client = _clientFactory.CreateClient();

            request.Headers.Add("Authorization", $"Bearer {settings.PrivateToken}");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return JConvert.DeserializeObject<EventbriteDescriptionDto>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<EventbriteVenueDto> GetVenueAsync(string venueId)
        {
            if (string.IsNullOrWhiteSpace(venueId))
            {
                return null;
            }

            var settings = await _eventbriteSettingsService.GetSettingsAsync();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{VenuesUrl}/{venueId}/");
            var client = _clientFactory.CreateClient();

            request.Headers.Add("Authorization", string.Format("Bearer {0}", settings.PrivateToken));

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return JConvert.DeserializeObject<EventbriteVenueDto>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        #endregion
    }

    public interface IEventbriteService
    {
        Task<EventbriteEventDto> GetEventAsync(string eventId);
        Task<EventbriteVenueDto> GetVenueAsync(string venueId);
        Task<EventbriteDescriptionDto> GetDescriptionAsync(string eventId);
    }
}
