using MediatR;
using Neredekal.Hotel.Application.Abstractions.Repositories;
using Neredekal.Hotel.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Hotel.Application.UseCase.HotelContactUseCases.Commands
{
    public record DeleteHotelContactCommand(Guid Id) : IRequest<Result>;

    public class DeleteHotelContctCommandHandler : IRequestHandler<DeleteHotelContactCommand, Result>
    {
        private readonly IHotelContactInfoRepository _repository;

        public DeleteHotelContctCommandHandler(IHotelContactInfoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteHotelContactCommand request, CancellationToken cancellationToken)
        {
            var hotelContact = await _repository.Get(request.Id);

            if (hotelContact == null)
                return Result.Error("Hotel Contact not found.");

            await _repository.Delete(hotelContact.UUID);
            await _repository.SaveChangesAsync();

            return Result.Success("Hotel Contact Removed");
        }
    }
}
