import {Routes} from "@angular/router";
import {isAuthorizedGuard} from "./core/guards/is-authorized-guard";
import {isUnauthorizedGuard} from "./core/guards/is-unauthorized-guard";

export const routes: Routes = [
  {
    path: '',
    canActivate: [isAuthorizedGuard],
    children: [
      { path: '', redirectTo: 'tabs', pathMatch: 'full' },
      {
        path: 'tabs',
        loadComponent: () => import('./features/tabs/shell/tabs.page'),
        loadChildren: () => import('./features/tabs/shell/tabs.routes')
      },
    ]
  },
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/shell/auth.routes'),
    canActivate: [isUnauthorizedGuard],
  }
]
