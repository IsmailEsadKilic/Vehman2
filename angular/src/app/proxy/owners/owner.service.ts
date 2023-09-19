import type { GetOwnersInput, OwnerCreateDto, OwnerDto, OwnerExcelDownloadDto, OwnerUpdateDto, OwnerWithNavigationPropertiesDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto, LookupDto, LookupRequestDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class OwnerService {
  apiName = 'Default';
  

  create = (input: OwnerCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, OwnerDto>({
      method: 'POST',
      url: '/api/app/owners',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/owners/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, OwnerDto>({
      method: 'GET',
      url: `/api/app/owners/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getCompanyLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/owners/company-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>({
      method: 'GET',
      url: '/api/app/owners/download-token',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetOwnersInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<OwnerWithNavigationPropertiesDto>>({
      method: 'GET',
      url: '/api/app/owners',
      params: { filterText: input.filterText, name: input.name, companyId: input.companyId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAsExcelFile = (input: OwnerExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: '/api/app/owners/as-excel-file',
      params: { downloadToken: input.downloadToken, filterText: input.filterText, name: input.name },
    },
    { apiName: this.apiName,...config });
  

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, OwnerWithNavigationPropertiesDto>({
      method: 'GET',
      url: `/api/app/owners/with-navigation-properties/${id}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: OwnerUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, OwnerDto>({
      method: 'PUT',
      url: `/api/app/owners/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
