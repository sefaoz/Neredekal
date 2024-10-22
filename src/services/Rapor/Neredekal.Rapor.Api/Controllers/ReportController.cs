using MediatR;
using Microsoft.AspNetCore.Mvc;
using Neredekal.Rapor.Application.UseCase.RaporUseCases.Commands;

namespace Neredekal.Rapor.Api.Controllers
{
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
    }
}
