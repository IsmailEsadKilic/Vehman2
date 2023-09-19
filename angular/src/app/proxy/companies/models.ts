import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CompanyCreateDto {
  name: string;
}

export interface CompanyDto extends FullAuditedEntityDto<string> {
  name: string;
  concurrencyStamp?: string;
}

export interface CompanyExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface CompanyUpdateDto {
  name: string;
  concurrencyStamp?: string;
}

export interface GetCompaniesInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
}
