using Vehman2.CarModels;
using Vehman2.Fuels;
using Vehman2.Owners;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace Vehman2.Vehicles
{
    public class VehicleWithNavigationPropertiesDto
    {
        public VehicleDto Vehicle { get; set; }

        public CarModelDto CarModel { get; set; }
        public FuelDto Fuel { get; set; }
        public OwnerDto Owner { get; set; }

    }
}