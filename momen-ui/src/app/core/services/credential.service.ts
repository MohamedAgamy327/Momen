import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as jwt_decode from '../../../../node_modules/jwt-decode';
import { RoleEnum } from '../enums';

@Injectable({
  providedIn: 'root'
})

export class CredentialService {

  constructor(
    private router: Router
  ) {
  }

  getToken() {
    return localStorage.getItem('momen-token');
  }

  getUser() {
    const token = jwt_decode(this.getToken());
    return token;
  }

  checkTokenExpire() {
    if (Date.now() >= this.getUser().exp * 1000) {
      return false;
    } else {
      return true;
    }
  }

  isLoggedIn() {
    if (this.getToken()) {
      return true;
    } else {
      return false;
    }
  }

  isNurse() {
    const token = this.getUser();
    if (token.role === RoleEnum.Nurse) {
      return true;
    } else {
      return false;
    }
  }

  isDoctor() {
    const token = this.getUser();
    if (token.role === RoleEnum.Doctor) {
      return true;
    } else {
      return false;
    }
  }

  logout() {
    localStorage.removeItem('momen-token');
    this.router.navigate(['/login']);
  }

}
