import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const FUELS_FUEL_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/fuels',
        iconClass: 'fas fa-gas-pump',
        name: '::Menu:Fuels',
        layout: eLayoutType.application,
        requiredPolicy: 'Vehman2.Fuels',
      },
    ]);
  };
}
