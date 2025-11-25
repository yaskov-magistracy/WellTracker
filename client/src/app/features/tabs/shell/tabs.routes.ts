import {Routes} from "@angular/router";

export default [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'food-diary',
  },
  {
    path: 'food-diary',
    loadComponent: () => import('../features/food-diary-tab/food-diary.component')
  },
  {
    path: 'personal-account',
    loadComponent: () => import('../features/personal-account-tab/personal-account.component')
  }
] satisfies Routes;
