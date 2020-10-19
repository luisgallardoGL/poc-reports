using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using samples.restful.Helpers;
using samples.restful.Models;
using samples.restful.ResourceParameters;

namespace samples.restful.Repositories
{
    public interface IReportRepository
    {
        public Task<PagedList<DelinquencySummary>> GetDelinquencySummaryReport(ReportResourceParameters reportResourceParameters);
    }
}
