import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule),
    canActivate: [AuthGuard, PermissionGuard],
  },
  {
    path: 'account',
    loadChildren: () =>
      import('@volo/abp.ng.account/public').then(m => m.AccountPublicModule.forLazy()),
  },
  {
    path: 'gdpr',
    loadChildren: () => import('@volo/abp.ng.gdpr').then(m => m.GdprModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@volo/abp.ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'language-management',
    loadChildren: () =>
      import('@volo/abp.ng.language-management').then(m => m.LanguageManagementModule.forLazy()),
  },
  {
    path: 'saas',
    loadChildren: () => import('@volo/abp.ng.saas').then(m => m.SaasModule.forLazy()),
  },
  {
    path: 'audit-logs',
    loadChildren: () =>
      import('@volo/abp.ng.audit-logging').then(m => m.AuditLoggingModule.forLazy()),
  },
  {
    path: 'openiddict',
    loadChildren: () =>
      import('@volo/abp.ng.openiddictpro').then(m => m.OpeniddictproModule.forLazy()),
  },
  {
    path: 'text-template-management',
    loadChildren: () =>
      import('@volo/abp.ng.text-template-management').then(m =>
        m.TextTemplateManagementModule.forLazy()
      ),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
  {
    path: 'gdpr-cookie-consent',
    loadChildren: () =>
      import('./gdpr-cookie-consent/gdpr-cookie-consent.module').then(
        m => m.GdprCookieConsentModule
      ),
  },
  { path: 'fuels', loadChildren: () => import('./fuels/fuel/fuel.module').then(m => m.FuelModule) },
  {
    path: 'owners',
    loadChildren: () => import('./owners/owner/owner.module').then(m => m.OwnerModule),
  },
  {
    path: 'car-models',
    loadChildren: () =>
      import('./car-models/car-model/car-model.module').then(m => m.CarModelModule),
  },
  {
    path: 'brands',
    loadChildren: () => import('./brands/brand/brand.module').then(m => m.BrandModule),
  },
  {
    path: 'companies',
    loadChildren: () => import('./companies/company/company.module').then(m => m.CompanyModule),
  },
  {
    path: 'vehicles',
    loadChildren: () => import('./vehicles/vehicle/vehicle.module').then(m => m.VehicleModule),
  },
  {
    path: 'transactions',
    loadChildren: () =>
      import('./transactions/transaction/transaction.module').then(m => m.TransactionModule),
  },
  {
    path: 'excel-import',
    loadChildren: () =>
      import('./excel-import/excel-import.module').then(m => m.ExcelImportModule),
  },
  {
    path: 'reports',
    loadChildren: () => import('./reports/reports.module').then(m => m.ReportsModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
