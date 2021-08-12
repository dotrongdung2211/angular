
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { AuthService } from 'src/app/authentication/service/auth.service';
import { environment } from './../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserListServiceService {

  constructor(private http: HttpClient, private router: Router, private authSerVice: AuthService) { }
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
      localStorage.removeItem('accessToken');
      this.authSerVice.doSignOut();
    }
    // // Return an observable with a user-facing error message.
    return throwError(txt_err);
  }
  getList(): Observable<Object> {
    return this.http.get(`${environment.apiUrl}/TblTaikhoans`).pipe(
      retry(3),
      catchError(this.handleError)
    );
  }
  getUserById(id: string) {
    return this.http.get(`${environment.apiUrl}/TblTaikhoans/${id}`).pipe(
      retry(3),
      catchError(this.handleError)
    );
  }
  addUser(payload: any, avatarFile: File) {
    const formData = this.parseToFormData(payload);
    if (avatarFile) {
      formData.append('CAnh', avatarFile);
    }
    return this.http.post(`${environment.apiUrl}/TblTaikhoans`, formData).pipe(
      catchError(this.handleError)
    );
  }
  updateUser(id: string, payload: any, avatarFile: File) {
    const formData = this.parseToFormData(payload);
    if (avatarFile) {
      formData.append('CAnh', avatarFile);
    }
    return this.http.put(`${environment.apiUrl}/TblTaikhoans/${id}`, formData).pipe(
      catchError(this.handleError)
    );
  }
  deleteUser(id: string) {
    return this.http.delete(`${environment.apiUrl}/TblTaikhoans/${id}`).pipe(
      catchError(this.handleError)
    );
  }
  parseToFormData(payload: any) {
    const formData: FormData = new FormData();
    Object.keys(payload).forEach((key) => {
      const capitalizeKey = this.capitalizeFirstLetter(key);
      if (payload[key]) {
        if (payload[key] instanceof Date) {
          const datestr = new Date(payload[key]).toUTCString();
          formData.append(capitalizeKey, datestr);
        }
        else {
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
