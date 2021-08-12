import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { FormGroup } from '@angular/forms';
import jwt_decode from 'jwt-decode';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import {
  NzTableFilterFn,
  NzTableFilterList,
  NzTableSortFn,
  NzTableSortOrder
} from 'ng-zorro-antd/table';
import { AppsService } from 'src/app/shared/services/apps.service';
import { UserListServiceService } from './service/user-list-service.service';
import { UserListFormComponent } from './user-list-form/user-list-form.component';

interface decode {
  Account: any;
  aud: any;
  exp: any;
  iat: any;
  iss: any;
  jti: any;
  sub: any;
}

interface dataItem {
  cTaikhoan: string,
  cMatkhau: string,
  cTen: string;
  cGioitinh: number;
  cChucvu: number;
  cNgaysinh: Date;
  cDienthoai: string;
  cDiachi: string;
  cAnh: string;
}

interface columnItem {
  name: string;
  span: number;
  sortOrder: NzTableSortOrder | null;
  sortFn: NzTableSortFn | null;
  sortDirection: NzTableSortOrder[];
  listOfFilter: NzTableFilterList;
  filterFn: NzTableFilterFn | null;
  filterMultiple: boolean;
}

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  searchValue = '';
  visible = false;
  user_Info: any = '';
  isVisible;
  dataTable: dataItem[] = [];
  listOfDisplayData: dataItem[] = [];
  datafetch;
  isOkLoading = false;
  validateForm: FormGroup;
  columnName: columnItem = {
    name: 'Tên',
    span: 5,
    sortOrder: 'descend',
    sortFn: (a: dataItem, b: dataItem) =>
      a.cTen.localeCompare(b.cTen),
    sortDirection: ['ascend', 'descend', null],
    listOfFilter: [
      { text: 'Nguyễn', value: 'Nguyễn' },
      { text: 'Lê', value: 'Lê' },
      { text: 'Trần', value: 'Trần' },
    ],
    filterMultiple: false,
    filterFn: (name: string, item: dataItem) => item.cTen.indexOf(name) !== -1,
  };
  columnGT: columnItem = {
    name: 'Giới tính',
    span: 3,
    sortOrder: null,
    sortFn: (a: dataItem, b: dataItem) => a.cGioitinh || b.cGioitinh,
    sortDirection: ['ascend', 'descend', null],
    listOfFilter: [
      { text: 'Nam', value: 1 },
      { text: 'Nữ', value: 2 },
    ],
    filterMultiple: false,
    filterFn: (list: number, item: dataItem) => item.cGioitinh == list,
  };


  constructor(
    private _user_List_Service: UserListServiceService,
    private notification: NzNotificationService,
    private modal: NzModalService,
    private viewContainerRef: ViewContainerRef,
    private _app_Service: AppsService,
  ) { }

  ngOnInit(): void {
    this.getData();
    this.getInfoUserByToken();

  }
  getData() {
    this._user_List_Service.getList().subscribe((res: any) => {
      this.dataTable = res;
      this.listOfDisplayData = [...this.dataTable];
    }, (err: any) => {
      this.notification.error(
        'Thất bại',
        err,
        { nzDuration: 5000 }
      );
      this.isOkLoading = false;
    });
  }
  deleteAccount(id: any) {
    this._user_List_Service.deleteUser(id).subscribe((res) => {
      setTimeout(() => {
        this.notification.success(
          'Thành công',
          'Xóa thành công',
          { nzDuration: 5000 }
        );
        this.getData();
      }, 2000);
    }, (err: any) => {
      this.notification.error(
        'Thất bại',
        err,
        { nzDuration: 5000 }
      );
      this.isOkLoading = false;
    });
  }
  createModal(data) {
    if (data != null) {
      const myModal = this.modal.create({
        nzTitle: 'Cập nhật tài khoản',
        nzContent: UserListFormComponent,
        nzViewContainerRef: this.viewContainerRef,
        nzComponentParams: { data },
        nzOkText: null,
        nzWidth: '40vw',
      });
      myModal.afterClose.subscribe(() => {
        this.getData();
      });
    }
    else {
      const myModal = this.modal.create({
        nzTitle: 'Thêm mới tài khoản',
        nzContent: UserListFormComponent,
        nzViewContainerRef: this.viewContainerRef,
        nzOkText: null,
        nzCloseOnNavigation: true,
        nzWidth: '40vw',
      });
      myModal.afterClose.subscribe(() => {
        this.getData();
      });
    }

  }
  getInfoUserByToken() {
    const token = localStorage.getItem('accessToken');
    const result_decode = jwt_decode(token) as decode;
    this._user_List_Service.getUserById(result_decode.Account).subscribe((res) => {
      this.user_Info = res;
      if (this.user_Info.cChucvu == false) {
        this.notification.info('Bị cấm', 'Bạn không có quyền truy cập', { nzDuration: 5000 });
      }
    });
  }
  reset(): void {
    this.searchValue = '';
    this.search();
  }

  search(): void {
    this.visible = false;
    this.listOfDisplayData = this.dataTable.filter((item: dataItem) => item.cTen.indexOf(this.searchValue) !== -1);


  }

}
