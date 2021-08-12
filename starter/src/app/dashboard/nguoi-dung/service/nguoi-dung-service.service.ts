import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NguoiDungServiceService {

  pageSize: number = 20;

  constructor(private http: HttpClient) { }

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

  getUsers(): Observable<Object> {
    return this.http
      .get(`${environment.apiUrl}/NguoiDung?PageIndex=1&PageSize=${this.pageSize}`)
      .pipe(retry(3), catchError(this.handleError));
  }

}
