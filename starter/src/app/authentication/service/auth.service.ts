import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { parseToFormData } from 'src/app/shared/utils/parseToFormData';
import { environment } from './../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private router: Router) { }

  private handleError(error: Response | any) {
    console.error('ApiService::handleError', error);
    return throwError(error);
  }

  SignIn(payload: any) {
    const formData = parseToFormData(payload);
    return this.http.post(`${environment.apiUrl}/Authentication`, formData).pipe(
      map((response: any) => response
      ),
      catchError(this.handleError)
    );
  }

  public doSignIn(accessToken: string) {
    if (!accessToken) {
      return;
    }
    localStorage.setItem('accessToken', accessToken);
  }
  public doSignOut() {
    localStorage.removeItem('accessToken');
    this.router.navigateByUrl('/authentication/login-3');
  }
  public isSignedIn() {
    return !!localStorage.getItem('accessToken');
  }


}
