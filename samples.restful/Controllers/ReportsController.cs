using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using samples.restful.Helpers;
using samples.restful.Models;
using samples.restful.Repositories;
using samples.restful.ResourceParameters;

namespace samples.restful.Controllers
{
   
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;

        public ReportsController(IReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("delinquencysummary", Name =nameof(GetDelinquencyReport))]
        public async Task<IActionResult> GetDelinquencyReport(ReportResourceParameters parameters)
        {
            if (!ShapeExtensions.ArePropertiesValidFor<DelinquencySummary>(parameters.Fields))
            {
                return BadRequest();
            }
            var reportData = await _repository.GetDelinquencySummaryReport(parameters);
            var previousPageLink = reportData.HasPrevious ?
                CreateReportResourceUri(parameters, parameters.PageNumber - 1) : null;

            var nextPageLink = reportData.HasNext ?
                CreateReportResourceUri(parameters, parameters.PageNumber + 1) : null;

            var paginationMetadata = new
            {
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink,
                totalCount = reportData.TotalCount,
                pageSize = reportData.PageSize,
                currentPage = reportData.CurrentPage,
                totalPages = reportData.TotalPages
            };

            Response.Headers.Add("X-Pagination",
                System.Text.Json.JsonSerializer.Serialize(paginationMetadata));

            var dtoResponse = _mapper.Map<IEnumerable<DelinquencySummaryDto>>(reportData);
            var reportResponse = new
            {
                data = dtoResponse.ShapeData(parameters.Fields),
                pagination = paginationMetadata
            };
            return Ok(reportResponse);
        }

        private string CreateReportResourceUri(ReportResourceParameters parameters, int pageNumberCalculated)
        {
            return Url.Link(nameof(GetDelinquencyReport), new
            {
                fields = parameters.Fields,
                sort = parameters.Sort,
                associationId = parameters.AssociationId,
                ownerName = parameters.OwnerName,
                lastPaymentDate = parameters.LastPaymentDate,
                pageNumber = pageNumberCalculated,
                pageSize = parameters.PageSize
            });
        }

    }
}