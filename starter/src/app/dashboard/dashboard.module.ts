import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { DashboardRoutingModule } from "./dashboard-routing.module";
import { DashboardComponent } from './dashboard.component';



import { UserListComponent } from './user-list/user-list.component';
import { UserListFormComponent } from './user-list/user-list-form/user-list-form.component';

/** Import any ng-zorro components as the module required except icon module */
import { NzButtonModule } from 'ng-zorro-antd/button';
import { DanhMucTenQuyenComponent } from './danh-muc-ten-quyen/danh-muc-ten-quyen.component';
import { DanhMucTenQuyenFormComponent } from './danh-muc-ten-quyen/danh-muc-ten-quyen-form/danh-muc-ten-quyen-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzNotificationModule } from 'ng-zorro-antd/notification';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NguoiDungComponent } from './nguoi-dung/nguoi-dung.component';
import { NguoiDungFormComponent } from './nguoi-dung/nguoi-dung-form/nguoi-dung-form.component';




/** Assign all ng-zorro modules to this array*/
const antdModule = [
  NzButtonModule,
  NzFormModule,
  NzTableModule,
  NzIconModule,
  NzDividerModule,
  NzPaginationModule,
  NzDropDownModule,
  NzNotificationModule,
  NzModalModule,
  NzSelectModule
]

@NgModule({
  imports: [
    SharedModule,
    ReactiveFormsModule,
    DashboardRoutingModule,
    ...antdModule
  ],
  exports: [],
  declarations: [
    DashboardComponent,
    UserListComponent,
    UserListFormComponent,
    DanhMucTenQuyenComponent,
    DanhMucTenQuyenFormComponent,
    NguoiDungComponent,
    NguoiDungFormComponent

  ]
})
export class DashboardModule { }
