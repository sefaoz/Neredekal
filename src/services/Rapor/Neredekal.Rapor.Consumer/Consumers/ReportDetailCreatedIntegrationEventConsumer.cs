using MassTransit;
using Neredekal.Common.Domain.IntegrationEvents.ReportEvents;
using Neredekal.Rapor.Application.Abstractions.Repositories;
using Neredekal.Rapor.Consumer.Services;

namespace Neredekal.Rapor.Consumer.Consumers
{
    public class ReportDetailCreatedIntegrationEventConsumer : IConsumer<ReportDetailCreatedIntegrationEvent>
    {
        private readonly IReportDetailRepository _repository;
        private readonly IHotelService _hotelService;

        public ReportDetailCreatedIntegrationEventConsumer(IReportDetailRepository repository, IHotelService hotelService)
        {
            _repository = repository;
            _hotelService = hotelService;
        }

        public async Task Consume(ConsumeContext<ReportDetailCreatedIntegrationEvent> context)
        {
            var result = await _hotelService.GetReport();

            if (result.IsSuccessStatusCode)
            {
                var data = result.Content;

                var reportDetail = await _repository.Get(context.Message.Id, context.CancellationToken);

                reportDetail.SetStatusCompleted(await data.ReadAsStringAsync());

                await _repository.SaveChangesAsync();
            }
        }
    }
}
