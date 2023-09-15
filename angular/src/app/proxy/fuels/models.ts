import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface FuelCreateDto {
  name: string;
}

export interface FuelDto extends FullAuditedEntityDto<string> {
  name: string;
  concurrencyStamp?: string;
}

export interface FuelExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface FuelUpdateDto {
  name: string;
  concurrencyStamp?: string;
}

export interface GetFuelsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
}
