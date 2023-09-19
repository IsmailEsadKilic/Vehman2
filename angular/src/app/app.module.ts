import { CoreModule } from '@abp/ng.core';
import { GdprConfigModule } from '@volo/abp.ng.gdpr/config';
import { SettingManagementConfigModule } from '@abp/ng.setting-management/config';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommercialUiConfigModule } from '@volo/abp.commercial.ng.ui/config';
import { AccountAdminConfigModule } from '@volo/abp.ng.account/admin/config';
import { AccountPublicConfigModule } from '@volo/abp.ng.account/public/config';
import { AuditLoggingConfigModule } from '@volo/abp.ng.audit-logging/config';
import { IdentityConfigModule } from '@volo/abp.ng.identity/config';
import { LanguageManagementConfigModule } from '@volo/abp.ng.language-management/config';
import { registerLocale } from '@volo/abp.ng.language-management/locale';
import { SaasConfigModule } from '@volo/abp.ng.saas/config';
import { TextTemplateManagementConfigModule } from '@volo/abp.ng.text-template-management/config';
import { HttpErrorComponent, ThemeLeptonXModule } from '@volosoft/abp.ng.theme.lepton-x';
import { SideMenuLayoutModule } from '@volosoft/abp.ng.theme.lepton-x/layouts';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_ROUTE_PROVIDER } from './route.provider';
import { OpeniddictproConfigModule } from '@volo/abp.ng.openiddictpro/config';
import { FeatureManagementModule } from '@abp/ng.feature-management';
import { AbpOAuthModule } from '@abp/ng.oauth';
import { AccountLayoutModule } from '@volosoft/abp.ng.theme.lepton-x/account';
import { FUELS_FUEL_ROUTE_PROVIDER } from './fuels/fuel/providers/fuel-route.provider';
import { OWNERS_OWNER_ROUTE_PROVIDER } from './owners/owner/providers/owner-route.provider';
import { CAR_MODELS_CAR_MODEL_ROUTE_PROVIDER } from './car-models/car-model/providers/car-model-route.provider';
import { BRANDS_BRAND_ROUTE_PROVIDER } from './brands/brand/providers/brand-route.provider';
import { COMPANIES_COMPANY_ROUTE_PROVIDER } from './companies/company/providers/company-route.provider';
import { VEHICLES_VEHICLE_ROUTE_PROVIDER } from './vehicles/vehicle/providers/vehicle-route.provider';
@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule.forRoot({
      environment,
      registerLocaleFn: registerLocale(),
    }),
    AbpOAuthModule.forRoot(),
    ThemeSharedModule.forRoot({
      httpErrorConfig: {
        errorScreen: {
          component: HttpErrorComponent,
          forWhichErrors: [401, 403, 404, 500],
          hideCloseIcon: true,
        },
      },
    }),
    AccountAdminConfigModule.forRoot(),
    AccountPublicConfigModule.forRoot(),
    AccountLayoutModule.forRoot(),
    IdentityConfigModule.forRoot(),
    LanguageManagementConfigModule.forRoot(),
    SaasConfigModule.forRoot(),
    AuditLoggingConfigModule.forRoot(),
    OpeniddictproConfigModule.forRoot(),
    TextTemplateManagementConfigModule.forRoot(),
    SettingManagementConfigModule.forRoot(),
    ThemeLeptonXModule.forRoot(),
    SideMenuLayoutModule.forRoot(),
    CommercialUiConfigModule.forRoot(),
    FeatureManagementModule.forRoot(),
    GdprConfigModule.forRoot({
      cookieConsent: {
        privacyPolicyUrl: 'gdpr-cookie-consent/privacy',
        cookiePolicyUrl: 'gdpr-cookie-consent/cookie',
      },
    }),
  ],
  providers: [
    APP_ROUTE_PROVIDER,
    FUELS_FUEL_ROUTE_PROVIDER,
    OWNERS_OWNER_ROUTE_PROVIDER,
    CAR_MODELS_CAR_MODEL_ROUTE_PROVIDER,
    BRANDS_BRAND_ROUTE_PROVIDER,
    COMPANIES_COMPANY_ROUTE_PROVIDER,
    VEHICLES_VEHICLE_ROUTE_PROVIDER,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
