import { Routes } from '@angular/router';

export const routes: Routes = [
  {path: "facturas", loadComponent: () => import("./facturas/facturas.component"),},
  {
    path: '',
    redirectTo: 'facturas',
    pathMatch: 'full',
  },
];
