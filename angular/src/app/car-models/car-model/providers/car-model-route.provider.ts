import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const CAR_MODELS_CAR_MODEL_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/car-models',
        iconClass: 'fas fa-list',
        name: '::Menu:CarModels',
        layout: eLayoutType.application,
        requiredPolicy: 'Vehman2.CarModels',
      },
    ]);
  };
}
