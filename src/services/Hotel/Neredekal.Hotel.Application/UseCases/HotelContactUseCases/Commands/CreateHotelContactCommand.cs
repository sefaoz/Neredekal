using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Neredekal.Hotel.Application.Abstractions.Repositories;
using Neredekal.Hotel.Application.Wrappers;
using Neredekal.Hotel.Domain.AggregateModels.HotelModels;

namespace Neredekal.Hotel.Application.UseCase.HotelContactUseCases.Commands
{
    public class CreateHotelContactCommand : IRequest<Result>
    {
        public required Guid HotelId { get; set; }
        public required InformationTypeEnum InformationType { get; set; }
        public required string InformationContent { get; set; }
    }

    public class CreateHotelContactCommandHandler : IRequestHandler<CreateHotelContactCommand, Result>
    {
        private readonly IHotelContactInfoRepository _repository;

        public CreateHotelContactCommandHandler(IHotelContactInfoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(CreateHotelContactCommand request, CancellationToken cancellationToken)
        {
            var hotelContact = HotelContactInfoItems.Create(Guid.NewGuid(),
                request.InformationType, request.InformationContent, request.HotelId);

            await _repository.Create(hotelContact);
            await _repository.SaveChangesAsync();

            return Result.Success("Hotel Contact Created");
        }
    }
}
