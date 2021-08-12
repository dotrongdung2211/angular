import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { AuthService } from './../service/auth.service';
@Component({
  templateUrl: './login-3.component.html'
})

export class Login3Component {
  loginForm: FormGroup;
  isLoading = false;
  constructor(
    private _auth_Service: AuthService,
    private fb: FormBuilder,
    private router: Router,
    private message: NzMessageService
  ) {
  }
  ngOnInit(): void {
    this.loginForm = this.fb.group({
      cTaikhoan: ['', [Validators.required, Validators.minLength(3)]],
      cMatkhau: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(16)]],
    });
  }
  submitForm(): void {
    for (const i in this.loginForm.controls) {
      this.loginForm.controls[i].markAsDirty();
      this.loginForm.controls[i].updateValueAndValidity();
    }
    if (!this.loginForm.valid) {
      return;
    }
    this.isLoading = true;
    this._auth_Service.SignIn(this.loginForm.value).subscribe(
      (response: any) => {
        console.log('token', response);

        this._auth_Service.doSignIn(response.accessToken);
        this.message.success('Đăng nhập thành công');
        this.isLoading = false;
        this.router.navigate(['']);
      },
      (error) => {
        this.message.error('Đăng nhập thất bại');
        this.isLoading = false;
      }
    );

  }
}
