import {Routes} from "@angular/router";

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'auth',
    children: [
      {
        path: 'tabs',
        loadComponent: () => import('./tabs/shell/tabs.page'),
        loadChildren: () => import('./tabs/shell/tabs.routes')
      },
    ]
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/shell/auth.routes')
  }
]
