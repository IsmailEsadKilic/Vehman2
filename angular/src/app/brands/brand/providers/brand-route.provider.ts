import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const BRANDS_BRAND_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/brands',
        iconClass: 'fas fa-copyright',
        name: '::Menu:Brands',
        layout: eLayoutType.application,
        requiredPolicy: 'Vehman2.Brands',
      },
    ]);
  };
}
