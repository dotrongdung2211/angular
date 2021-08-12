import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { NguoiDungServiceService } from './service/nguoi-dung-service.service';


interface Users {
  id: string;
  c_Code: string;
  c_Email: string;
  c_HoVaTen: string;
  c_SoDienThoai: string;
  c_NgayTao: string;
  c_Avatar: string;
  c_TrangThai: number;
  c_TenQuyen: string;
  fk_DanhMucTenQuyen:string;
}

@Component({
  selector: 'app-nguoi-dung',
  templateUrl: './nguoi-dung.component.html',
  styleUrls: ['./nguoi-dung.component.css']
})
export class NguoiDungComponent implements OnInit {

  visible = false;
  isVisible = false;
  isOkLoading = false;
  dataTable: Users[] = []

  constructor(private nguoidungservice: NguoiDungServiceService,
    private notification: NzNotificationService,
    private modal: NzModalService,
    private modalService: NzModalService,
    private viewContainerRef: ViewContainerRef,) { }

  ngOnInit(): void {
    this.getUsers()
  }

  getUsers() {
    this.nguoidungservice.getUsers().subscribe((res: any) => {
      this.dataTable = res.items;

    }, (err: any) => {
      this.notification.error(
        'Thất bại',
        err,
        { nzDuration: 5000 }
      );
      this.isOkLoading = false;
    });
  }


  createModal (i) {

  }
}
