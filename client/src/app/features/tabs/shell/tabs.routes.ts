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
    path: 'user-profile',
    loadComponent: () => import('../features/user-profile-tab/user-profile-tab.component')
  },
  {
    path: 'ai-consultant',
    loadComponent: () => import('../features/ai-consultant/ai-consultant.component')
  }
] satisfies Routes;
