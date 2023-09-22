using Vehman2.Vehicles;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace Vehman2.Transactions
{
    public class TransactionWithNavigationPropertiesDto
    {
        public TransactionDto Transaction { get; set; }

        public VehicleDto Vehicle { get; set; }

    }

    public class ReportWithNavigationPropertiesDto
    {
        public ReportDto Report { get; set; }

        public VehicleDto Vehicle { get; set; }

    }
}