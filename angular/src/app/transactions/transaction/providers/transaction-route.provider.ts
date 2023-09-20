import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const TRANSACTIONS_TRANSACTION_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/transactions',
        iconClass: 'fas fa-exchange-alt',
        name: '::Menu:Transactions',
        layout: eLayoutType.application,
        requiredPolicy: 'Vehman2.Transactions',
      },
    ]);
  };
}
