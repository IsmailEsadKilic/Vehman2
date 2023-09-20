import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { CarModelDto } from '../car-models/models';
import type { FuelDto } from '../fuels/models';
import type { OwnerDto } from '../owners/models';

export interface GetVehiclesInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  plate?: string;
  carModelId?: string;
  fuelId?: string;
  ownerId?: string;
}

export interface VehicleCreateDto {
  plate: string;
  carModelId: string;
  fuelId: string;
  ownerId: string;
  brandId?: string;
  companyId?: string;
}

export interface VehicleDto extends FullAuditedEntityDto<string> {
  plate: string;
  carModelId: string;
  fuelId: string;
  ownerId: string;
  concurrencyStamp?: string;
}

export interface VehicleExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface VehicleUpdateDto {
  plate: string;
  carModelId: string;
  fuelId: string;
  ownerId: string;
  concurrencyStamp?: string;
}

export interface VehicleWithNavigationPropertiesDto {
  vehicle: VehicleDto;
  carModel: CarModelDto;
  fuel: FuelDto;
  owner: OwnerDto;
}
