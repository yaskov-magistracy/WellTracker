import {Routes} from "@angular/router";
import {isAuthorizedGuard} from "./shared/guards/is-authorized-guard";
import {isUnauthorizedGuard} from "./shared/guards/is-unauthorized-guard";

export const routes: Routes = [
  {
    path: '',
    canActivate: [isAuthorizedGuard],
    children: [
      { path: '', redirectTo: 'tabs', pathMatch: 'full' },
      {
        path: 'tabs',
        loadComponent: () => import('./tabs/shell/tabs.page'),
        loadChildren: () => import('./tabs/shell/tabs.routes')
      },
    ]
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/shell/auth.routes'),
    canActivate: [isUnauthorizedGuard],
  }
]
