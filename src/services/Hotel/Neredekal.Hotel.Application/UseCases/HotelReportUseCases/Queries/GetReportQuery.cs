using MediatR;
using Neredekal.Hotel.Application.Abstractions.Repositories;
using Neredekal.Hotel.Application.UseCases.HotelReportUseCases.Responses;
using Neredekal.Hotel.Application.Wrappers;
using Neredekal.Hotel.Domain.AggregateModels.HotelModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Hotel.Application.UseCases.HotelReportUseCases.Queries
{
    public record GetReportQuery : IRequest<Result<List<GetReportResponse>>>;

    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, Result<List<GetReportResponse>>>
    {
        private readonly IHotelRepository _repository;

        public GetReportQueryHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<GetReportResponse>>> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            var hotels = await _repository.GetAll(cancellationToken);

            var result = hotels.SelectMany(x => x.HotelContactInfoItems
                .Where(w => w.InformationType == InformationTypeEnum.Location || w.InformationType == InformationTypeEnum.Phone)
                .Select(s => new { Otel = s, s.InformationContent, s.InformationType })
            )
                .GroupBy(g => g.InformationType == InformationTypeEnum.Location ? g.InformationContent : null)
                .Where(w => w.Key != null)
                .Select(s => new GetReportResponse()
                {
                    Location = s.Key,
                    HotelCount = s.Count(c => c.InformationType == InformationTypeEnum.Location),
                    PhoneCount = s.Count(c => c.InformationType == InformationTypeEnum.Phone)
                }).ToList();

            return Result<List<GetReportResponse>>.Success(result, "");
        }
    }
}
