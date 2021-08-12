import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { NzUploadFile } from 'ng-zorro-antd/upload';
import { Observable, Observer } from 'rxjs';
import { UserListServiceService } from './../service/user-list-service.service';

@Component({
  selector: 'app-user-list-form',
  templateUrl: './user-list-form.component.html',
  styleUrls: ['./user-list-form.component.css']
})
export class UserListFormComponent implements OnInit {
  @Input() data;
  acction: string;
  value = '';
  avatar: string = null;
  fileUpload: File;
  isOkLoading = false;
  validateForm: FormGroup;
  constructor(
    private _user_List_Service: UserListServiceService,
    private modal: NzModalService,
    private fb: FormBuilder,
    private notification: NzNotificationService,
    private msg: NzMessageService,
    private router: Router
  ) { }

  ngOnInit(): void {

    if (this.data == undefined) {
      this.acction = 'add';
      this.validateForm = this.fb.group({
        cTaikhoan: ['', [Validators.required, Validators.minLength(3)]],
        cMatkhau: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(16)]],
        checkPassword: [null, [Validators.required, this.confirmationValidator]],

        cTen: ['', [Validators.required, Validators.minLength(3)]],
        cGioitinh: ['', [Validators.required]],
        cNgaysinh: ['', [Validators.required]],
        cDiachi: ['', [Validators.required, Validators.minLength(3)]],
        cChucvu: ['', [Validators.required]],
        cDienthoai: ['', [Validators.required, Validators.minLength(3)]],
      });
    }
    else {
      this.acction = 'edit';
      this.validateForm = this.fb.group({
        cTaikhoan: [{ value: '', disabled: true }],
        cMatkhau: [''],
        checkPassword: [null, [this.confirmationValidatorEdit]],
        cTen: ['', [Validators.required, Validators.minLength(3)]],
        cGioitinh: ['', [Validators.required]],
        cNgaysinh: ['', [Validators.required]],
        cDiachi: ['', [Validators.required, Validators.minLength(3)]],
        cChucvu: ['', [Validators.required]],
        cDienthoai: ['', [Validators.required, Validators.minLength(3)]],
      });
      this.validateForm.patchValue(this.data);
      this.validateForm.controls.cMatkhau.setValue('');

    }

  }
  confirmationValidatorEdit = (control: FormControl): { [s: string]: boolean } => {
    if (!control.value) {
      return {};
    }
    else if (control.value !== this.validateForm.controls.cMatkhau.value) {
      return { confirm: true, error: true };
    }
    return {};
  };
  confirmationValidator = (control: FormControl): { [s: string]: boolean } => {
    if (!control.value) {
      return { required: true };
    }
    else if (control.value !== this.validateForm.controls.cMatkhau.value) {
      return { confirm: true, error: true };
    }
    return {};
  };
  submitForm() {
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[i].markAsDirty();
      this.validateForm.controls[i].updateValueAndValidity();
    }
    if (!this.validateForm.valid) {
      return;
    }

    this.isOkLoading = true;
    if (this.data == undefined) {
      this._user_List_Service
        .addUser(this.validateForm.value, this.fileUpload)
        .subscribe((res) => {
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
            'Thất bại',
            err,
            { nzDuration: 5000 }
          );
          this.isOkLoading = false;
        });
    }
    else {
      this._user_List_Service
        .updateUser(this.data.cTaikhoan, this.validateForm.value, this.fileUpload)
        .subscribe((res) => {
          setTimeout(() => {
            this.notification.success(
              'Thành công',
              'Sửa thành công',
              { nzDuration: 5000 }
            );
            this.isOkLoading = false;
            // this.modal.closeAll();
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

  }
  beforeUpload = (file: NzUploadFile, _fileList: NzUploadFile[]) => {
    return new Observable((observer: Observer<boolean>) => {
      const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png';
      if (!isJpgOrPng) {
        this.msg.error('Chỉ upload được file jpg hoặc png');
        observer.complete();
        return;
      }
      observer.next(isJpgOrPng);
      observer.complete();
    });
  };
  private getBase64(img: File, callback: (img: {}) => void): void {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
  }
  handleChange(info: { file: NzUploadFile }): void {
    this.getBase64(info.file.originFileObj, (img: string) => {
      this.avatar = img;
      this.fileUpload = info.file.originFileObj;
    });
  }

}
