namespace Neredekal.Rapor.Consumer.Services
{
    public interface IHotelService
    {
        Task<HttpResponseMessage> GetReport();
    }
}
