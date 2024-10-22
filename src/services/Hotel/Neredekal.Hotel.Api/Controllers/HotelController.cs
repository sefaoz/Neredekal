using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Neredekal.Hotel.Application.UseCase.HotelUseCases.Commands;
using Neredekal.Hotel.Application.UseCase.HotelUseCases.Queries;

namespace Neredekal.Hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HotelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel(CreateHotelCommand command)
        {
            await _mediator.Send(command);

            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotel(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetHotelQuery(id), cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var result = await _mediator.Send(new GetAllHotelQuery());

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            await _mediator.Send(new DeleteHotelCommand(id));

            return NoContent();
        }

        [HttpGet("{id}/Person")]
        public async Task<IActionResult> GetHotelPersons()
        {
            return Ok();
        }
    }
}
