import {Routes} from "@angular/router";

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'tabs'
  },
  {
    path: 'tabs',
    loadComponent: () => import('./tabs/shell/tabs.page'),
    loadChildren: () => import('./tabs/shell/tabs.routes')
  },
]
