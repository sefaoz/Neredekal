using MediatR;
using Neredekal.Hotel.Application.Abstractions.Repositories;
using Neredekal.Hotel.Application.UseCase.HotelUseCases.Responses;
using Neredekal.Hotel.Application.Wrappers;
using Neredekal.Hotel.Domain.AggregateModels.HotelModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Hotel.Application.UseCase.HotelUseCases.Queries
{
    public record GetHotelQuery(Guid HotelId) : IRequest<Result<GetHotelResponse>>;

    public class GetHotelQueryHandler : IRequestHandler<GetHotelQuery, Result<GetHotelResponse>>
    {
        private readonly IHotelRepository _repository;

        public GetHotelQueryHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<GetHotelResponse>> Handle(GetHotelQuery request, CancellationToken cancellationToken)
        {
            var hotel = await _repository.Get(request.HotelId,cancellationToken);

            var result = new GetHotelResponse()
            {
                Id = hotel.UUID,
                PersonName = hotel.PersonName,
                CompanyName = hotel.CompanyName,
                PersonSurname = hotel.PersonSurname,
                HotelContactInfoItems = hotel.HotelContactInfoItems.Select(x => new HotelContactInfoDto { Id = x.UUID, InformationContent = x.InformationContent, InformationType = x.InformationType.ToString() }).ToList()
            };

            return Result<GetHotelResponse>.Success(result, "");
        }
    }
}
