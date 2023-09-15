import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CarModelCreateDto {
  name: string;
}

export interface CarModelDto extends FullAuditedEntityDto<string> {
  name: string;
  concurrencyStamp?: string;
}

export interface CarModelExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface CarModelUpdateDto {
  name: string;
  concurrencyStamp?: string;
}

export interface GetCarModelsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
}
