using MediatR;
using Neredekal.Common.Domain.IntegrationEvents.HotelIntegrationEvents;
using Neredekal.Hotel.Application.Abstractions.Repositories;
using Neredekal.Hotel.Application.Wrappers;
using Neredekal.Hotel.Domain.AggregateModels.HotelModels;

namespace Neredekal.Hotel.Application.UseCase.HotelUseCases.Commands
{
    public class CreateHotelCommand : IRequest<Result>
    {
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public string CompanyName { get; set; }
        public required List<HotelContactInfoItemDto> HotelContactInfoItems { get; set; }
    }

    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Result>
    {
        private readonly IHotelRepository _repository;

        public CreateHotelCommandHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            Guid hotelId = Guid.NewGuid();
            List<HotelContactInfoItems> contactItems = new List<HotelContactInfoItems>();

            foreach (var contactItem in request.HotelContactInfoItems)
            {
                contactItems.Add(HotelContactInfoItems.CreateHotelContactInfoItems(Guid.NewGuid(), (InformationTypeEnum)contactItem.InformationType, contactItem.InformationContent, hotelId));
            }

            var hotel = Domain.AggregateModels.HotelModels.Hotel.CreateHotel(hotelId, request.PersonName,
                request.PersonSurname, request.CompanyName, contactItems);

            await _repository.Create(hotel);
            await _repository.SaveChangesAsync();

            return Result.Success("Hotel Created");
        }
    }
}
