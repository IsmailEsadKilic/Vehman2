import type { BrandCreateDto, BrandDto, BrandExcelDownloadDto, BrandUpdateDto, GetBrandsInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class BrandService {
  apiName = 'Default';
  

  create = (input: BrandCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BrandDto>({
      method: 'POST',
      url: '/api/app/brands',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/brands/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BrandDto>({
      method: 'GET',
      url: `/api/app/brands/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>({
      method: 'GET',
      url: '/api/app/brands/download-token',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetBrandsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<BrandDto>>({
      method: 'GET',
      url: '/api/app/brands',
      params: { filterText: input.filterText, name: input.name, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAsExcelFile = (input: BrandExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: '/api/app/brands/as-excel-file',
      params: { downloadToken: input.downloadToken, filterText: input.filterText, name: input.name },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: BrandUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BrandDto>({
      method: 'PUT',
      url: `/api/app/brands/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
