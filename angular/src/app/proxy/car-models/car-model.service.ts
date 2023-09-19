import type { CarModelCreateDto, CarModelDto, CarModelExcelDownloadDto, CarModelUpdateDto, CarModelWithNavigationPropertiesDto, GetCarModelsInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto, LookupDto, LookupRequestDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class CarModelService {
  apiName = 'Default';
  

  create = (input: CarModelCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CarModelDto>({
      method: 'POST',
      url: '/api/app/car-models',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/car-models/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CarModelDto>({
      method: 'GET',
      url: `/api/app/car-models/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getBrandLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/car-models/brand-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>({
      method: 'GET',
      url: '/api/app/car-models/download-token',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetCarModelsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CarModelWithNavigationPropertiesDto>>({
      method: 'GET',
      url: '/api/app/car-models',
      params: { filterText: input.filterText, name: input.name, brandId: input.brandId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAsExcelFile = (input: CarModelExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: '/api/app/car-models/as-excel-file',
      params: { downloadToken: input.downloadToken, filterText: input.filterText, name: input.name },
    },
    { apiName: this.apiName,...config });
  

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CarModelWithNavigationPropertiesDto>({
      method: 'GET',
      url: `/api/app/car-models/with-navigation-properties/${id}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CarModelUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CarModelDto>({
      method: 'PUT',
      url: `/api/app/car-models/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
