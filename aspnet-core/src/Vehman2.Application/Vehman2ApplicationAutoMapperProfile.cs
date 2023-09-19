using Vehman2.Transactions;
using Vehman2.Vehicles;
using Vehman2.Companies;
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

        CreateMap<CarModelWithNavigationProperties, CarModelWithNavigationPropertiesDto>();
        CreateMap<Brand, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Company, CompanyDto>();
        CreateMap<Company, CompanyExcelDto>();

        CreateMap<OwnerWithNavigationProperties, OwnerWithNavigationPropertiesDto>();
        CreateMap<Company, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Vehicle, VehicleDto>();
        CreateMap<Vehicle, VehicleExcelDto>();
        CreateMap<VehicleWithNavigationProperties, VehicleWithNavigationPropertiesDto>();
        CreateMap<CarModel, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
        CreateMap<Fuel, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
        CreateMap<Owner, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Transaction, TransactionDto>();
        CreateMap<Transaction, TransactionExcelDto>();
        CreateMap<TransactionWithNavigationProperties, TransactionWithNavigationPropertiesDto>();
        CreateMap<Vehicle, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Plate));
    }
}