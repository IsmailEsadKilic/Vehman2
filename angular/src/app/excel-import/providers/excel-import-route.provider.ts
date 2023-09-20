import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const EXCEL_IMPORT_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/excel-import',
        iconClass: 'fas fa-upload',
        name: 'Excel Import',
        layout: eLayoutType.application,
        //requiredPolicy: 'Vehman2.ExcelImport',
      },
    ]);
  };
}