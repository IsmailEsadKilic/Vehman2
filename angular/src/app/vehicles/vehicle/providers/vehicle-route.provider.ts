import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const VEHICLES_VEHICLE_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/vehicles',
        iconClass: 'fas fa-car',
        name: '::Menu:Vehicles',
        layout: eLayoutType.application,
        requiredPolicy: 'Vehman2.Vehicles',
      },
    ]);
  };
}
