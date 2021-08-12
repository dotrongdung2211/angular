import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './service/auth.service';

@Injectable()
export class CanActivateGuard implements CanActivate {

  constructor(
    private auth: AuthService,
    private router: Router
  ) {
  }

  public canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    if (!this.auth.isSignedIn()) {
      this.router.navigate(['/authentication/login-3']);
      return false;
    }
    return true;
  }

}
