import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const OWNERS_OWNER_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/owners',
        iconClass: 'fas fa-portrait',
        name: '::Menu:Owners',
        layout: eLayoutType.application,
        requiredPolicy: 'Vehman2.Owners',
      },
    ]);
  };
}
