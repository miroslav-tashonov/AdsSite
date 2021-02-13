import { Routes } from '@angular/router';

import { MainComponent } from './main/main.component';

export const rootRouterConfig: Routes = [
  {
    path: '',
    redirectTo: 'home/one',
    pathMatch: 'full'
  },
  {
    path: '',
    component: MainComponent,
    children: [
      {
        path: 'home',
        loadChildren: () => import('./shop/shop.module').then(m => m.ShopModule)
      },
      {
        path: 'pages',
        loadChildren: () => import('./pages/pages.module').then(m => m.PagesModule)
      }
    ]
  },
  {
    path: '**',
    redirectTo: 'home/one'
  }
];

