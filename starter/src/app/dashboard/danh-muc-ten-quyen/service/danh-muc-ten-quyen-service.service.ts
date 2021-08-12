import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';
import { AuthService } from 'src/app/authentication/service/auth.service';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root',
})

export class DanhMucTenQuyenServiceService {



  pageSize: number = 20;

  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private authSerVice: AuthService
  ) {}

  private handleError(error: HttpErrorResponse) {
    let txt_err = '';
    switch (error.status) {
      case 0:
        txt_err = environment.CODE_0;
        break;
      case 400:
        txt_err = environment.CODE_400;
        break;
      case 401:
        txt_err = environment.CODE_401;
        break;
      case 403:
        txt_err = environment.CODE_404;
        break;
      case 404:
        txt_err = environment.CODE_404;
        break;
      case 405:
        txt_err = environment.CODE_405;
        break;
      case 409:
        txt_err = environment.CODE_409;
        break;
      case 415:
        txt_err = environment.CODE_415;
        break;
    }
    if (error.status === 401) {
      // localStorage.removeItem('accessToken');
      // this.authSerVice.doSignOut();
    }
    // // Return an observable with a user-facing error message.
    return throwError(txt_err);
  }

  getlistNames(): Observable<Object> {
    return this.httpClient
      .get(`${environment.apiUrl}/DanhMucTenQuyen?PageIndex=1&PageSize=${this.pageSize}`)
      .pipe(retry(3), catchError(this.handleError));
  }




  deletelistName(id: string) {
    return this.httpClient
      .delete(`${environment.apiUrl}/DanhMucTenQuyen/${id}`)
      .pipe(catchError(this.handleError));
  }

  postlistName(payload: any ) {
    const formData = this.parseToFormData(payload);
    return this.httpClient
      .post(`${environment.apiUrl}/DanhMucTenQuyen`, formData)
      .pipe(catchError(this.handleError));
  }



  parseToFormData(payload: any) {
    const formData: FormData = new FormData();
    Object.keys(payload).forEach((key) => {
      const capitalizeKey = this.capitalizeFirstLetter(key);
      if (payload[key]) {
        if (payload[key] instanceof Date) {
          const datestr = new Date(payload[key]).toUTCString();
          formData.append(capitalizeKey, datestr);
        } else {
          formData.append(capitalizeKey, payload[key]);
        }
      }
    });
    return formData;
  }
  capitalizeFirstLetter(key: string) {
    return key[0].toUpperCase() + key.substr(1);
  }
}
