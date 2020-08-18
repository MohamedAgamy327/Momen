import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AccountService {

  constructor(
    private http: HttpClient
  ) { }

  login(model: any): Observable<any> {
    return this.http.post(environment.serverUrl + 'accounts/login', model);
  }
}
