import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { VehicleDto } from '../vehicles/models';

export interface GetTransactionsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  priceMin?: number;
  priceMax?: number;
  litersMin?: number;
  litersMax?: number;
  dateMin?: string;
  dateMax?: string;
  vehicleId?: string;
}

export interface TransactionCreateDto {
  price: number;
  liters?: number;
  date?: string;
  vehicleId: string;
}

export interface TransactionDto extends FullAuditedEntityDto<string> {
  price: number;
  liters?: number;
  date?: string;
  vehicleId: string;
  concurrencyStamp?: string;
}

export interface TransactionExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface TransactionUpdateDto {
  price: number;
  liters?: number;
  date?: string;
  vehicleId: string;
  concurrencyStamp?: string;
}

export interface TransactionWithNavigationPropertiesDto {
  transaction: TransactionDto;
  vehicle: VehicleDto;
}
