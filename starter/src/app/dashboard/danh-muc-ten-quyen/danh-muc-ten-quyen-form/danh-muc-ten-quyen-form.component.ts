import { Input } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { DanhMucTenQuyenServiceService } from '../service/danh-muc-ten-quyen-service.service';



@Component({
  selector: 'app-danh-muc-ten-quyen-form',
  templateUrl: './danh-muc-ten-quyen-form.component.html',
  styleUrls: ['./danh-muc-ten-quyen-form.component.css']
})
export class DanhMucTenQuyenFormComponent implements OnInit {

  @Input() data;
  value = '';
  listNameForm : FormGroup;
  acction: string;
  id ?= null;
  isOkLoading = false;


  constructor(
    private DanhMucTenQuyenServiceService: DanhMucTenQuyenServiceService,
    private router: Router,
    private formbuilder: FormBuilder,
    private notification: NzNotificationService,
    private modal: NzModalService,
    private modalService: NzModalService,
     private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    // this.id = this.route.snapshot.paramMap.get('id');
    // if (this.id > 0) {
    //   this.loadData(this.id)
    // }

    if(this.data == undefined) {
      this.acction = 'add';
      this.listNameForm = this.formbuilder.group({
        c_Code: ['',Validators.required],
        c_TenQuyen:['', [Validators.required, Validators.minLength(3)]],
        c_TrangThai:['', Validators.required]
      })
    }
    else {
      this.acction = 'edit';
      this.listNameForm = this.formbuilder.group({
        c_Code: ['',Validators.required],
        c_TenQuyen:['', [Validators.required, Validators.minLength(3)]],
        c_TrangThai:['', Validators.required]
      })
    }



  }

  control(name: string) {
    return this.listNameForm.get(name)
  }



  submitForm() {
    for (const i in this.listNameForm.controls) {
      this.listNameForm.controls[i].markAsDirty();
      this.listNameForm.controls[i].updateValueAndValidity();
    }
    if (!this.listNameForm.valid) {
      return;
    }

    this.isOkLoading = true;
    if (this.data == undefined) {
      this.DanhMucTenQuyenServiceService
        .postlistName(this.listNameForm.value)
        .subscribe( res => {
          setTimeout(() => {
            this.notification.success(
              'Thành công',
              'Thêm mới thành công',
              { nzDuration: 5000 }
            );
            this.isOkLoading = false;
            this.modal.closeAll();
          }, 2000);
        }, (err: any) => {
          this.notification.error(
            'Thêm Thất bại',
            err,
            { nzDuration: 5000 }
          );
          this.isOkLoading = false;
        });
    }
    else {
      // this.DanhMucTenQuyenServiceService
      //   .updatelistName()
      //   .subscribe((res) => {
      //     setTimeout(() => {
      //       this.notification.success(
      //         'Thành công',
      //         'Sửa thành công',
      //         { nzDuration: 5000 }
      //       );
      //       this.isOkLoading = false;

      //     }, 2000);
      //   }, (err: any) => {
      //     this.notification.error(
      //       'Sửa Thất bại',
      //       err,
      //       { nzDuration: 5000 }
      //     );
      //     this.isOkLoading = false;
      //   });
    }

  }

  // updateCategory() {
  //   for( const i in this.listNameForm.controls) {
  //     this.listNameForm.controls[i].markAsDirty();
  //     this.listNameForm.controls[i].updateValueAndValidity();
  //   }
  //   if (!this.listNameForm.valid) {
  //     setTimeout(() => {
  //       this.notification.error(
  //         'Thất bại',
  //         'Cập Nhập thất bại',
  //         { nzDuration: 5000 }
  //       );
  //     }, 100);
  //     return;
  //   }

  //   this.listNameObj.code =this.listNameForm.value.code;
  //   this.listNameObj.name = this.listNameForm.value.name;
  //   this.listNameObj.status = this.listNameForm.value.status;
  //   this.DanhMucTenQuyenServiceService.updatelistName(this.listNameObj, this.id).subscribe(res => {
  //     setTimeout(() => {
  //       this.notification.success(
  //         'Thành công',
  //         'Cập Nhập thành công',
  //         { nzDuration: 5000 }
  //       );
  //       this.router.navigate(['danh-muc-ten-quyen'])
  //     }, 100);
  //   })
  // }


  genderChange() {

  }
}



