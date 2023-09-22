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
  companyName?: string;
}

export interface GetReportsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  priceMin?: number;
  priceMax?: number;
  litersMin?: number;
  litersMax?: number;
  dateMin?: string;
  dateMax?: string;
  vehicleId?: string;
  companyName?: string;
}

export interface TransactionCreateDto {
  price: number;
  liters?: number;
  date?: string;
  vehicleId: string;
  plate?: string;
}

export interface Transaction {
  price: number;
  liters?: number;
  date?: string;
  vehicleId: string;
  companyName?: string;
  plate?: string;
}

export interface TransactionDto extends FullAuditedEntityDto<string> {
  price: number;
  liters?: number;
  date?: string;
  vehicleId: string;
  companyName?: string;
  concurrencyStamp?: string;
}

export interface ReportDto extends FullAuditedEntityDto<string> {
  totalPrice: number;
  totalLiters?: number;
  vehicleId: string;
  companyName?: string;
  concurrencyStamp?: string;
  totalTransactions: number;
  averagePrice: number;
  averageLiters: number;
  averagePricePerLiter: number;
  averageLitersPerTransaction: number;
  averagePricePerTransaction: number;
  averageLitersPerPrice: number;
}

export interface TransactionExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface ReportExcelDOwnloadDto {
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

export interface ReportWithNavigationPropertiesDto {
  report: ReportDto;
  vehicle: VehicleDto;
}

