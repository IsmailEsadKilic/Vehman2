import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface BrandCreateDto {
  name: string;
}

export interface BrandDto extends FullAuditedEntityDto<string> {
  name: string;
  concurrencyStamp?: string;
}

export interface BrandExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface BrandUpdateDto {
  name: string;
  concurrencyStamp?: string;
}

export interface GetBrandsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
}
