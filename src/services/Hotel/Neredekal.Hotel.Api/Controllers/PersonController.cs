using MediatR;
using Microsoft.AspNetCore.Mvc;
using Neredekal.Hotel.Application.UseCases.HotelGetPersonsUseCases.Queries;

namespace Neredekal.Hotel.Api.Controllers
{
    public class PersonController : Controller
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetHotelPersonsQuery());
            return Ok(result);
        }
    }
}
