using MediatR;
using Neredekal.Hotel.Application.Abstractions.Repositories;
using Neredekal.Hotel.Application.UseCases.HotelGetPersonsUseCases.Responses;
using Neredekal.Hotel.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Hotel.Application.UseCases.HotelGetPersonsUseCases.Queries
{
    public record GetHotelPersonsQuery() : IRequest<Result<List<GetHotelPersonResponse>>>;

    public class GetHotelPersonsQueryHandler : IRequestHandler<GetHotelPersonsQuery, Result<List<GetHotelPersonResponse>>>
    {
        private readonly IHotelRepository _repository;

        public GetHotelPersonsQueryHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<GetHotelPersonResponse>>> Handle(GetHotelPersonsQuery request, CancellationToken cancellationToken)
        {
            var hotels = await _repository.GetAll(cancellationToken);

            var persons = hotels.Select(x=> new GetHotelPersonResponse { PersonName = x.PersonName, PersonSurname = x.PersonSurname }).ToList();

            return Result<List<GetHotelPersonResponse>>.Success(persons, "");
        }
    }
}
