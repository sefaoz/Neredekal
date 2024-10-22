
namespace Neredekal.Rapor.Consumer.Services
{
    public class HotelService : IHotelService
    {
        private readonly HttpClient _httpClient;

        public HotelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetReport()
        {
            var response = await _httpClient.GetAsync("Report");

            return response;
        }
    }
}
