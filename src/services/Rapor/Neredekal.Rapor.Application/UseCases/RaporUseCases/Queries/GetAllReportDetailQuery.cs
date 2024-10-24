using MediatR;
using Neredekal.Rapor.Application.Abstractions.Repositories;
using Neredekal.Rapor.Application.UseCases.RaporUseCases.Responses;
using Neredekal.Rapor.Application.Wrapper;
using Neredekal.Rapor.Domain.AggregateModels.RaporModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Rapor.Application.UseCases.RaporUseCases.Queries
{
    public record GetAllReportDetailQuery : IRequest<Result<List<GetAllReportResponse>>>;

    public class GetAllReportDetailQueryHandler : IRequestHandler<GetAllReportDetailQuery, Result<List<GetAllReportResponse>>>
    {
        private readonly IReportDetailRepository _repository;

        public GetAllReportDetailQueryHandler(IReportDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<GetAllReportResponse>>> Handle(GetAllReportDetailQuery request, CancellationToken cancellationToken)
        {
            var reports = await _repository.GetAll(cancellationToken);

            var result = reports.Select(x=> new GetAllReportResponse() { Id = x.UUID, ReportStatus = x.ReportStatus, RequestDate = x.RequestDate }).ToList();

            return Result<List<GetAllReportResponse>>.Success(result, "");
        }
    }
}
