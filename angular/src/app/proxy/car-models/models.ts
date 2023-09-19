import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { BrandDto } from '../brands/models';

export interface CarModelCreateDto {
  name: string;
  brandId: string;
}

export interface CarModelDto extends FullAuditedEntityDto<string> {
  name: string;
  brandId: string;
  concurrencyStamp?: string;
}

export interface CarModelExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface CarModelUpdateDto {
  name: string;
  brandId: string;
  concurrencyStamp?: string;
}

export interface CarModelWithNavigationPropertiesDto {
  carModel: CarModelDto;
  brand: BrandDto;
}

export interface GetCarModelsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
  brandId?: string;
}
