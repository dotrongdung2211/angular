import { NzMessageService } from 'ng-zorro-antd/message';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, PreloadAllModules } from '@angular/router';

import { FullLayoutComponent } from "./layouts/full-layout/full-layout.component";
import { CommonLayoutComponent } from "./layouts/common-layout/common-layout.component";

import { FullLayout_ROUTES } from "./shared/routes/full-layout.routes";
import { CommonLayout_ROUTES } from "./shared/routes/common-layout.routes";
import { Error1Component } from './authentication/error-1/error-1.component';
import { CanActivateGuard } from './authentication/can-active.guard';

const appRoutes: Routes = [
  // {
  //   path: '',
  //   redirectTo: '/dashboard/home',
  //   pathMatch: 'full',
  // },
  {
    path: '',
    component: CommonLayoutComponent,
    canActivate: [CanActivateGuard],
    children: CommonLayout_ROUTES
  },
  {
    path: 'authentication',
    loadChildren: () => import('../app/authentication/authentication.module').then(m => m.AuthenticationModule)
  },
  // {
  //   path: '',
  //   component: FullLayoutComponent,
  //   children: FullLayout_ROUTES
  // },
  {
    path: '404',
    component: Error1Component
  },
  {
    path: '**',
    redirectTo: '404',

  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes, {
      preloadingStrategy: PreloadAllModules,
      anchorScrolling: 'enabled',
      scrollPositionRestoration: 'enabled'
    })
  ],
  exports: [
    RouterModule
  ],
  providers: [CanActivateGuard, NzMessageService]
})

export class AppRoutingModule {
}
