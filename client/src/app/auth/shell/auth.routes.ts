import {Routes} from "@angular/router";

export default [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'login',
  },
  {
    path: 'login',
    loadComponent: () => import('../features/login/login.page')
  },
  {
    path: 'signup',
    loadComponent: () => import('../features/signup/signup.page')
  }
] satisfies Routes;
