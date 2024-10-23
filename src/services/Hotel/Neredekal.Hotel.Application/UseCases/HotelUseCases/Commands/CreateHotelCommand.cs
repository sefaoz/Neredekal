using MediatR;
using Neredekal.Hotel.Application.Abstractions.Repositories;
using Neredekal.Hotel.Application.UseCases.HotelUseCases.Requests;
using Neredekal.Hotel.Application.Wrappers;
using Neredekal.Hotel.Domain.AggregateModels.HotelModels;

namespace Neredekal.Hotel.Application.UseCase.HotelUseCases.Commands
{
    public class CreateHotelCommand : IRequest<Result>
    {
        public required string PersonName { get; set; }
        public required string PersonSurname { get; set; }
        public required string CompanyName { get; set; }
        public required List<HotelContactInfoRequest> HotelContactInfoItems { get; set; }
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
                contactItems.Add(HotelContactInfoItems.Create(Guid.NewGuid(), contactItem.InformationType, contactItem.InformationContent, hotelId));
            }

            var hotel = Domain.AggregateModels.HotelModels.Hotel.Create(hotelId, request.PersonName,
                request.PersonSurname, request.CompanyName, contactItems);

            await _repository.Create(hotel,cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return Result.Success("Hotel Created");
        }
    }
}
