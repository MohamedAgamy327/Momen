import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CredentialService } from '../services';

@Injectable({
  providedIn: 'root'
})

export class LoginGuard implements CanActivate {

  constructor(
    private credentialService: CredentialService,
    private route: Router
  ) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (this.credentialService.isLoggedIn() && this.credentialService.checkTokenExpire()) {
      if (this.credentialService.isDoctor()) {
        this.route.navigate(['home']);
      } else if (this.credentialService.isNurse()) {
        this.route.navigate(['home/ar']);
      }
      return false;
    }
    return true;
  }
}
