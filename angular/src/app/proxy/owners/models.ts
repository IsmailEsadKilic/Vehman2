import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetOwnersInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
}

export interface OwnerCreateDto {
  name: string;
}

export interface OwnerDto extends FullAuditedEntityDto<string> {
  name: string;
  concurrencyStamp?: string;
}

export interface OwnerExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface OwnerUpdateDto {
  name: string;
  concurrencyStamp?: string;
}
