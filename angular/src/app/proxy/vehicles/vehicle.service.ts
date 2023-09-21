import type { GetVehiclesInput, VehicleCreateDto, VehicleDto, VehicleExcelDownloadDto, VehicleUpdateDto, VehicleWithNavigationPropertiesDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto, LookupDto, LookupRequestDto } from '../shared/models';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  apiName = 'Default';
  private vehicleAddedSource = new Subject<void>();
  vehicleAdded$ = this.vehicleAddedSource.asObservable();

  triggerVehicleAdded() {
    this.vehicleAddedSource.next();
  }
  
  create = (input: VehicleCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, VehicleDto>({
      method: 'POST',
      url: '/api/app/vehicles',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/vehicles/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, VehicleDto>({
      method: 'GET',
      url: `/api/app/vehicles/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getCarModelLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/vehicles/car-model-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>({
      method: 'GET',
      url: '/api/app/vehicles/download-token',
    },
    { apiName: this.apiName,...config });
  

  getFuelLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/vehicles/fuel-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetVehiclesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<VehicleWithNavigationPropertiesDto>>({
      method: 'GET',
      url: '/api/app/vehicles',
      params: { filterText: input.filterText, plate: input.plate, carModelId: input.carModelId, fuelId: input.fuelId, ownerId: input.ownerId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  getListAll() {
    let input: GetVehiclesInput = { maxResultCount: 1000 };
    return this.getList(input);
  }
  
  getListAsExcelFile = (input: VehicleExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: '/api/app/vehicles/as-excel-file',
      params: { downloadToken: input.downloadToken, filterText: input.filterText, name: input.name },
    },
    { apiName: this.apiName,...config });
  

  getOwnerLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/vehicles/owner-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, VehicleWithNavigationPropertiesDto>({
      method: 'GET',
      url: `/api/app/vehicles/with-navigation-properties/${id}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: VehicleUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, VehicleDto>({
      method: 'PUT',
      url: `/api/app/vehicles/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
