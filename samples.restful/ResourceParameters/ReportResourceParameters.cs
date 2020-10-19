using System;
namespace samples.restful.ResourceParameters
{
    public class ReportResourceParameters
    {
        public ReportResourceParameters()
        {
        }
        private static readonly int maxPageSize = 20;
        public int AssociationId { get; set; }
        public string OwnerName { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public string Sort { get; set; } = nameof(LastPaymentDate);
        public string Fields { get; set; }
    }
}
