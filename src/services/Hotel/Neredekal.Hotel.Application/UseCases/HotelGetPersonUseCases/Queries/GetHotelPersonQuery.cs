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
    public record GetHotelPersonQuery(Guid HotelId) : IRequest<Result<GetHotelPersonResponse>>;

    public class GetHotelPersonQueryHandler : IRequestHandler<GetHotelPersonQuery, Result<GetHotelPersonResponse>>
    {
        private readonly IHotelRepository _repository;

        public GetHotelPersonQueryHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<GetHotelPersonResponse>> Handle(GetHotelPersonQuery request, CancellationToken cancellationToken)
        {
            var hotel = await _repository.Get(request.HotelId,cancellationToken);

            var person = new GetHotelPersonResponse { PersonName = hotel.PersonName, PersonSurname = hotel.PersonSurname };

            return Result<GetHotelPersonResponse>.Success(person, "");
        }
    }
}
