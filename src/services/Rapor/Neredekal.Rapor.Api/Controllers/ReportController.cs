using MediatR;
using Microsoft.AspNetCore.Mvc;
using Neredekal.Rapor.Application.UseCase.RaporUseCases.Commands;
using Neredekal.Rapor.Application.UseCases.RaporUseCases.Queries;

namespace Neredekal.Rapor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            await _mediator.Send(new CreateReportDetailCommand());

            return Accepted();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetReportDetailQuery(id));

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllReportDetailQuery());

            return Ok(result);
        }
    }
}
