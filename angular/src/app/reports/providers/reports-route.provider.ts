import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const REPORTS_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/reports',
        iconClass: 'fas fa-file',
        name: 'Raporlar',
        layout: eLayoutType.application,
        //requiredPolicy: 'Vehman2.Reports',
      },
    ]);
  };
}