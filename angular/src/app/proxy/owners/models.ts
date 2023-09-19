import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { CompanyDto } from '../companies/models';

export interface GetOwnersInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
  companyId?: string;
}

export interface OwnerCreateDto {
  name: string;
  companyId: string;
}

export interface OwnerDto extends FullAuditedEntityDto<string> {
  name: string;
  companyId: string;
  concurrencyStamp?: string;
}

export interface OwnerExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface OwnerUpdateDto {
  name: string;
  companyId: string;
  concurrencyStamp?: string;
}

export interface OwnerWithNavigationPropertiesDto {
  owner: OwnerDto;
  company: CompanyDto;
}
