using MediatR;
using Neredekal.Hotel.Application.Abstractions.Repositories;
using Neredekal.Hotel.Application.Wrappers;
using Neredekal.Hotel.Domain.AggregateModels.HotelModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Hotel.Application.UseCase.HotelUseCases.Commands
{
    public record DeleteHotelCommand(Guid HotelId) : IRequest<Result>;

    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, Result>
    {
        private readonly IHotelRepository _repository;

        public DeleteHotelCommandHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _repository.Get(request.HotelId,cancellationToken);

            if (hotel == null)
                return Result.Error("Hotel not found.");

            await _repository.Delete(hotel.UUID);
            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success("Hotel Removed");
        }
    }
}
