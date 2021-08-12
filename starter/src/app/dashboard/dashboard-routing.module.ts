import { DanhMucTenQuyenComponent } from './danh-muc-ten-quyen/danh-muc-ten-quyen.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { UserListComponent } from './user-list/user-list.component';
import { DanhMucTenQuyenFormComponent } from './danh-muc-ten-quyen/danh-muc-ten-quyen-form/danh-muc-ten-quyen-form.component';

import { UserListFormComponent } from './user-list/user-list-form/user-list-form.component';
import { NguoiDungComponent } from './nguoi-dung/nguoi-dung.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    data: {
      title: 'Dashboard ',
      headerDisplay: "none"
    }
  },
  {
    path: 'user-list',
    component: UserListComponent,
    data: {
      title: 'Danh sách tài khoản ',
      headerDisplay: 'none'
    }
  },
  {
    path: 'danh-muc-ten-quyen',
    component: DanhMucTenQuyenComponent,
    data: {
      title: 'Danh mục tên quyền',
      headerDisplay: 'none'
    }
  },
  {
    path: 'nguoi-dung',
    component: NguoiDungComponent,
    data: {
      title: 'Người Dùng',
      headerDisplay: 'none'
    }
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule { }
