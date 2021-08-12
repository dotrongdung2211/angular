import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

import { DanhMucTenQuyenServiceService } from './service/danh-muc-ten-quyen-service.service';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { NzModalService } from 'ng-zorro-antd/modal';
import { DanhMucTenQuyenFormComponent } from './danh-muc-ten-quyen-form/danh-muc-ten-quyen-form.component';



interface listName {
  id: string;
  c_TenQuyen: string;
  c_TrangThai: number;
  c_Code: string;
}




@Component({
  selector: 'app-danh-muc-ten-quyen',
  templateUrl: './danh-muc-ten-quyen.component.html',
  styleUrls: ['./danh-muc-ten-quyen.component.css']
})
export class DanhMucTenQuyenComponent implements OnInit {

  page = 1;
  pageSize = 10;
  searchValue = '';
  visible = false;
  isVisible = false;
  isOkLoading = false;
  datafetch;
  dataTable: listName[] = [];
  listOfDisplayData: listName[] = [];



  constructor(
    private DanhMucTenQuyenServiceService: DanhMucTenQuyenServiceService,
    private router: Router,
    private notification: NzNotificationService,
    private modal: NzModalService,
    private modalService: NzModalService,
    private viewContainerRef: ViewContainerRef,
    ) { }

  ngOnInit(): void {
    this.getlistNames()
  }

  getlistNames() {
    this.DanhMucTenQuyenServiceService.getlistNames().subscribe((res: any) => {
      this.dataTable = res.items;
      //this.listOfDisplayData = [...this.dataTable];
    }, (err: any) => {
      this.notification.error(
        'Thất bại',
        err,
        { nzDuration: 5000 }
      );
      this.isOkLoading = false;
    });
  }




  deleteList(list: any): void {

    this.modal.confirm({
      nzTitle: '<i>Bạn có muốn xóa không?</i>',
      nzContent: '<b>Bạn có chắc muốn xóa</b>',
      nzOkText: 'Có',
      nzOnOk: () => {
        list.deleted = true;
        this.DanhMucTenQuyenServiceService.deletelistName(list.id).subscribe(res => {
          setTimeout(() => {
                  this.notification.success(
                    'Thành công',
                    'Xóa thành công',
                    { nzDuration: 5000 }
                  );
                 this.getlistNames();
                }, 100);
        })
      },
      nzCancelText: 'Không',
      nzOnCancel: () => {
        list.deleted = false;
        setTimeout(() => {
              this.notification.error(
                'Thất bại',
                'Xóa thất bại',
                { nzDuration: 5000 }
              );
             this.getlistNames();
            }, 100);
            this.isOkLoading = false;
      }
    });

  }

  createModal(data: any) {
    if (data != null) {
      const myModal = this.modal.create({
        nzTitle: 'Cập nhật tài khoản',
        nzContent: DanhMucTenQuyenFormComponent,
        nzViewContainerRef: this.viewContainerRef,
        nzComponentParams: { data  },
        nzOkText: null,
        nzWidth: '60vw',
      });
      myModal.afterClose.subscribe(() => {
        this.getlistNames();
      });
    }
    else {
      const myModal = this.modal.create({
        nzTitle: 'Thêm mới tài khoản',
        nzContent: DanhMucTenQuyenFormComponent,
        nzViewContainerRef: this.viewContainerRef,
        nzOkText: null,
        nzCloseOnNavigation: true,
        nzWidth: '40vw',
      });
      myModal.afterClose.subscribe(() => {
        this.getlistNames();
      });
    }

  }


  // editCategory (listId) {
  //   this.router.navigate(['danh-muc-ten-quyen-form', listId])
  // }

  reset(): void {
    this.searchValue = '';
    this.getlistNames();
  }

  search(): void {
    this.visible = false;
    // this.listNameData = this.listNameData.filter((item: listName) => item.name.indexOf(this.searchValue) !== -1);
  }


  sortFn = (a: listName , b: listName) => a.c_TenQuyen.localeCompare(b.c_TenQuyen);
}


