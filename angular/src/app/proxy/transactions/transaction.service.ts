import type { GetTransactionsInput, TransactionCreateDto, TransactionDto, TransactionExcelDownloadDto, TransactionUpdateDto, TransactionWithNavigationPropertiesDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto, LookupDto, LookupRequestDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  apiName = 'Default';
  

  create = (input: TransactionCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TransactionDto>({
      method: 'POST',
      url: '/api/app/transactions',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/transactions/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TransactionDto>({
      method: 'GET',
      url: `/api/app/transactions/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>({
      method: 'GET',
      url: '/api/app/transactions/download-token',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetTransactionsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TransactionWithNavigationPropertiesDto>>({
      method: 'GET',
      url: '/api/app/transactions',
      params: { filterText: input.filterText, priceMin: input.priceMin, priceMax: input.priceMax, litersMin: input.litersMin, litersMax: input.litersMax, dateMin: input.dateMin, dateMax: input.dateMax, vehicleId: input.vehicleId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  getListAll() {
    let input: GetTransactionsInput = { maxResultCount: 1000 };
    return this.getList(input);
  }

  getListAsExcelFile = (input: TransactionExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: '/api/app/transactions/as-excel-file',
      params: { downloadToken: input.downloadToken, filterText: input.filterText, name: input.name },
    },
    { apiName: this.apiName,...config });
  

  getVehicleLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/transactions/vehicle-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TransactionWithNavigationPropertiesDto>({
      method: 'GET',
      url: `/api/app/transactions/with-navigation-properties/${id}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: TransactionUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TransactionDto>({
      method: 'PUT',
      url: `/api/app/transactions/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
