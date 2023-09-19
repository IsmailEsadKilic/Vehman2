using Vehman2.Brands;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace Vehman2.CarModels
{
    public class CarModelWithNavigationPropertiesDto
    {
        public CarModelDto CarModel { get; set; }

        public BrandDto Brand { get; set; }

    }
}