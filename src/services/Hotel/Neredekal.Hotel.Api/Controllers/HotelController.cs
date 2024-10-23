using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Neredekal.Hotel.Application.UseCase.HotelUseCases.Commands;
using Neredekal.Hotel.Application.UseCase.HotelUseCases.Queries;
using Neredekal.Hotel.Application.UseCases.HotelGetPersonsUseCases.Queries;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetHotelQuery(id), cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllHotelQuery());

            return Ok(result);
        }

        [HttpGet("{id}/Person")]
        public async Task<IActionResult> GetPerson(Guid id)
        {
            var result = await _mediator.Send(new GetHotelPersonQuery(id));

            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateHotelCommand command)
        {
            await _mediator.Send(command);

            return Created();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteHotelCommand(id));

            return Ok(result);
        }

        [HttpGet("Report")]
        public async Task<IActionResult> GetReport()
        {
            return Ok();
        }
    }
}
