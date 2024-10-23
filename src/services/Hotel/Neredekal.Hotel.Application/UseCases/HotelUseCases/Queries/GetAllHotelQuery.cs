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
    public record GetAllHotelQuery : IRequest<Result<List<GetHotelResponse>>>;

    public class GetAllHotelQueryHandler : IRequestHandler<GetAllHotelQuery, Result<List<GetHotelResponse>>>
    {
        private readonly IHotelRepository _repository;

        public GetAllHotelQueryHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<GetHotelResponse>>> Handle(GetAllHotelQuery request, CancellationToken cancellationToken)
        {
            var hotels = await _repository.GetAll(cancellationToken);
            var result = hotels.Select(x => new GetHotelResponse() { Id = x.UUID, CompanyName = x.CompanyName, PersonName = x.PersonName, PersonSurname = x.PersonSurname, HotelContactInfoItems = x.HotelContactInfoItems.Select(q=> new HotelContactInfoDto { Id = q.UUID, InformationType = q.InformationType.ToString(), InformationContent = q.InformationContent}).ToList()}).ToList();

            return Result<List<GetHotelResponse>>.Success(result, "");
        }
    }
}
