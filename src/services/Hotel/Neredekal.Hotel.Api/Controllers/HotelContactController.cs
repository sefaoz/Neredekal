using MediatR;
using Microsoft.AspNetCore.Mvc;
using Neredekal.Hotel.Application.UseCase.HotelContactUseCases.Commands;

namespace Neredekal.Hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelContactController : Controller
    {
        private readonly IMediator _mediator;

        public HotelContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHotelContactCommand command)
        {
            await _mediator.Send(command);

            return Created();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteHotelContactCommand(id));

            return NoContent();
        }
    }
}
