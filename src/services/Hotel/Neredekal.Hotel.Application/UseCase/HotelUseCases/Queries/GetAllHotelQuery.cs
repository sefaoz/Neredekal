using MediatR;
using Neredekal.Common.Domain.IntegrationEvents.HotelIntegrationEvents;
using Neredekal.Hotel.Application.Abstractions.Repositories;
using Neredekal.Hotel.Application.UseCase.HotelUseCases.Response;
using Neredekal.Hotel.Application.Wrappers;
using Neredekal.Hotel.Domain.AggregateModels.HotelModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Hotel.Application.UseCase.HotelUseCases.Queries
{
    public record GetAllHotelQuery : IRequest<Result<List<HotelGetResponse>>>;

    public class GetAllHotelQueryHandler : IRequestHandler<GetAllHotelQuery, Result<List<HotelGetResponse>>>
    {
        private readonly IHotelRepository _repository;

        public GetAllHotelQueryHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<HotelGetResponse>>> Handle(GetAllHotelQuery request, CancellationToken cancellationToken)
        {
            var hotels = await _repository.GetAll();
            var result = hotels.Select(x => new HotelGetResponse() { Id = x.UUID, CompanyName = x.CompanyName, PersonName = x.PersonName, PersonSurname = x.PersonSurname, HotelContactInfoItems = x.HotelContactInfoItems.Select(q=> new HotelContactInfoDto { Id = q.UUID, InformationType = q.InformationType.ToString(), InformationContent = q.InformationContent}).ToList()}).ToList();

            return Result<List<HotelGetResponse>>.Success(result, "");
        }
    }
}
