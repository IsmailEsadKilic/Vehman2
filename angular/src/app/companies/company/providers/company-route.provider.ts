import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const COMPANIES_COMPANY_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/companies',
        iconClass: 'fas fa-building',
        name: '::Menu:Companies',
        layout: eLayoutType.application,
        requiredPolicy: 'Vehman2.Companies',
      },
    ]);
  };
}
