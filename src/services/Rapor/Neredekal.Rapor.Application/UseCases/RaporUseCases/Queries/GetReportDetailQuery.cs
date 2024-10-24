using MediatR;
using Neredekal.Rapor.Application.Abstractions.Repositories;
using Neredekal.Rapor.Application.Wrapper;
using Neredekal.Rapor.Domain.AggregateModels.RaporModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Rapor.Application.UseCases.RaporUseCases.Queries
{
    public record GetReportDetailQuery(Guid Id) : IRequest<Result<ReportDetail>>;

    public class GetReportDetailQueryHandler : IRequestHandler<GetReportDetailQuery, Result<ReportDetail>>
    {
        private readonly IReportDetailRepository _repository;

        public GetReportDetailQueryHandler(IReportDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ReportDetail>> Handle(GetReportDetailQuery request, CancellationToken cancellationToken)
        {
            var report = await _repository.Get(request.Id, cancellationToken);

            return Result<ReportDetail>.Success(report, "");
        }
    }
}
