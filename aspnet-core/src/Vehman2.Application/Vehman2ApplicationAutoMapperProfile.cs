using Vehman2.Brands;
using Vehman2.CarModels;
using Vehman2.Owners;
using System;
using Vehman2.Shared;
using Volo.Abp.AutoMapper;
using Vehman2.Fuels;
using AutoMapper;

namespace Vehman2;

public class Vehman2ApplicationAutoMapperProfile : Profile
{
    public Vehman2ApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Fuel, FuelDto>();
        CreateMap<Fuel, FuelExcelDto>();

        CreateMap<Owner, OwnerDto>();
        CreateMap<Owner, OwnerExcelDto>();

        CreateMap<CarModel, CarModelDto>();
        CreateMap<CarModel, CarModelExcelDto>();

        CreateMap<Brand, BrandDto>();
        CreateMap<Brand, BrandExcelDto>();
    }
}