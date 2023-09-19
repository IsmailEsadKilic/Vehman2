using Volo.Abp.Application.Dtos;
using System;

namespace Vehman2.Vehicles
{
    public class VehicleExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? Plate { get; set; }
        public Guid? CarModelId { get; set; }
        public Guid? FuelId { get; set; }
        public Guid? OwnerId { get; set; }

        public VehicleExcelDownloadDto()
        {

        }
    }
}