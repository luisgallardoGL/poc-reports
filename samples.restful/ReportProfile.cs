using System;
using AutoMapper;
using samples.restful.Models;

namespace samples.restful
{
    public class ReportProfile: Profile
    {
        public ReportProfile()
        {
            CreateMap<DelinquencySummary, DelinquencySummaryDto>().ReverseMap();
        }
    }
}
