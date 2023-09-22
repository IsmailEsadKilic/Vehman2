using Volo.Abp.Application.Dtos;
using System;

namespace Vehman2.Transactions
{
    public class GetTransactionsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public double? PriceMin { get; set; }
        public double? PriceMax { get; set; }
        public double? LitersMin { get; set; }
        public double? LitersMax { get; set; }
        public DateTime? DateMin { get; set; }
        public DateTime? DateMax { get; set; }
        public Guid? VehicleId { get; set; }
        public string? CompanyName { get; set; }

        public GetTransactionsInput()
        {

        }
    }

    public class GetReportsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public DateTime? DateMin { get; set; }
        public DateTime? DateMax { get; set; }
        public Guid? VehicleId { get; set; }
        public string? CompanyName { get; set; }

        public GetReportsInput()
        {

        }
    }
}