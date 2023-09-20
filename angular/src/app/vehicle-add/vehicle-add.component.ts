import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BrandDto, GetBrandsInput } from '@proxy/brands';
import { CarModelDto, GetCarModelsInput } from '@proxy/car-models';
import { CompanyDto, GetCompaniesInput } from '@proxy/companies';
import { FuelDto, GetFuelsInput } from '@proxy/fuels';
import { GetOwnersInput, OwnerDto } from '@proxy/owners';
import { VehicleCreateDto } from '@proxy/vehicles';
import { ToastrService } from 'ngx-toastr';
import { VehicleService } from '@proxy/vehicles/vehicle.service';
import { BrandService } from '@proxy/brands/brand.service';
import { CarModelService } from '@proxy/car-models/car-model.service';
import { FuelService } from '@proxy/fuels/fuel.service';
import { CompanyService } from '@proxy/companies/company.service';
import { OwnerService } from '@proxy/owners/owner.service';

@Component({
  selector: 'app-vehicle-add',
  templateUrl: './vehicle-add.component.html',
  styleUrls: ['./vehicle-add.component.scss']
})
export class VehicleAddComponent implements OnInit {
  @Input() plate: string | undefined = '';
  brands: BrandDto[] = [];
  models: CarModelDto[] = [];
  fuelTypes: FuelDto[] = [];
  companies: CompanyDto[] = [];
  owners: OwnerDto[] = [];
  vehicleAdd: VehicleCreateDto = {} as VehicleCreateDto;

  constructor( private Toastr: ToastrService, private vehicleService: VehicleService,
    private brandService: BrandService, private modelService: CarModelService,
    private fuelService: FuelService, private companyService: CompanyService,
    private ownerService: OwnerService) { }

  ngOnInit(): void {
    if (this.plate) {
      this.vehicleAdd.plate = this.plate;
      console.log("a");
      console.log(this.vehicleAdd.plate);
    }
    if (!this.plate) {
      console.log("b");
    }
    this.getVehProps();
  }

  getVehProps() {
    this.getBrands();
    this.getFuelTypes();
    this.getCompanies();
  }

  getBrands() {
    this.brandService.getList({} as GetBrandsInput).subscribe({
      next: brands => {
        this.brands = brands.items;
      }
    })
  }

  getModelsByBrandId(brandId: string) {
    this.modelService.getList({brandId: brandId} as GetCarModelsInput).subscribe({
      next: models => {
        this.models = models.items.map((item) => item.carModel);
      }
    })
  }

  getFuelTypes() {
    this.fuelService.getList({} as GetFuelsInput).subscribe({
      next: fuelTypes => {
        this.fuelTypes = fuelTypes.items;
      }
    })
  }

  getCompanies() {
    this.companyService.getList({} as GetCompaniesInput).subscribe({
      next: companies => {
        this.companies = companies.items;
      }
    })
  }

  getOwnersByCompanyId(companyId: string) {
    this.ownerService.getList({companyId: companyId} as GetOwnersInput).subscribe({
      next: owners => {
        this.owners = owners.items.map((item) => item.owner);
      }
    })
  }

  onBrandSelectionChange() {
    this.vehicleAdd.carModelId = "0";

    if (this.vehicleAdd.brandId) {
        this.getModelsByBrandId(this.vehicleAdd.brandId);
    } else {
        this.models = []; // Reset models if no brand is selected
    }
  }

  onCompanySelectionChange() {
    this.vehicleAdd.ownerId = "0";
    if (this.vehicleAdd.companyId) {
        this.getOwnersByCompanyId(this.vehicleAdd.companyId);
    } else {
        this.owners = []; // Reset owners if no company is selected
    }
  }


  addVehicle() {
    this.vehicleService.create(this.vehicleAdd).subscribe({
      next: () => {
        this.Toastr.success("Araç eklendi.");
        this.vehicleAdd = {} as VehicleCreateDto;
        this.models = [];
        this.owners = [];
      }
    })
  }
}
