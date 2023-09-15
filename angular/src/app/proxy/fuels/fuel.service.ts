import type { FuelCreateDto, FuelDto, FuelExcelDownloadDto, FuelUpdateDto, GetFuelsInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class FuelService {
  apiName = 'Default';
  

  create = (input: FuelCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FuelDto>({
      method: 'POST',
      url: '/api/app/fuels',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/fuels/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FuelDto>({
      method: 'GET',
      url: `/api/app/fuels/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>({
      method: 'GET',
      url: '/api/app/fuels/download-token',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetFuelsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<FuelDto>>({
      method: 'GET',
      url: '/api/app/fuels',
      params: { filterText: input.filterText, name: input.name, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAsExcelFile = (input: FuelExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: '/api/app/fuels/as-excel-file',
      params: { downloadToken: input.downloadToken, filterText: input.filterText, name: input.name },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: FuelUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FuelDto>({
      method: 'PUT',
      url: `/api/app/fuels/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
