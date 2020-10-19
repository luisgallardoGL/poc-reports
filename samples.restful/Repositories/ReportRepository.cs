using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using samples.restful.Models;
using samples.restful.ResourceParameters;
using System.Linq;
using samples.restful.Helpers;

namespace samples.restful.Repositories
{
    public class ReportRepository: IReportRepository
    {
        public ReportRepository()
        {
        }

        public Task<PagedList<DelinquencySummary>> GetDelinquencySummaryReport(ReportResourceParameters reportResourceParameters)
        {
            var query = CreateFakeReportData().AsQueryable();

            if (!string.IsNullOrEmpty(reportResourceParameters.OwnerName))
            {
                query = query.Where(r => r.OwnerName.Contains(reportResourceParameters.OwnerName));
            }
            query = query.ApplySorting(reportResourceParameters.Sort);
            return Task.FromResult(PagedList<DelinquencySummary>.Create(query,
                reportResourceParameters.PageNumber,
                reportResourceParameters.PageSize));
        }

        private IEnumerable<DelinquencySummary> CreateFakeReportData()
        {
            var data = new List<DelinquencySummary>();
            for (int i = 0; i < 100; i++)
            {
                var record = new DelinquencySummary
                {
                    
                    AccountId = i.ToString(),
                    AccountStatus = "active",
                    CurrentBalance = 10 * i,
                    DelinquencyStatus = "active",
                    LastPaymentAmount = 10 * i,
                    LastPaymentDate = DateTime.UtcNow,
                    MailAddress = "someone@something.com",
                    OwnerName = i % 2 == 0 ? $"Juan {i}" : $"Diego {i}",
                    Total30Days = 2 * i,
                    Total60Days = 4 * i,
                    Total90Days = 6 * i,
                    TotalOver90Days = 8 * i,
                    UnitAddress = "NA"
                };
                data.Add(record);
            }
            return data;
        }
    }
}
