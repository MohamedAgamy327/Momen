import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from '../../models';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  constructor(
    private http: HttpClient
  ) { }

  create(model): Observable<any> {
    return this.http.post<User>(`${environment.serverUrl}users`, model);
  }

  edit(id, model): Observable<any> {
    return this.http.put<User>(`${environment.serverUrl}users/${id}`, model);
  }

  changePassword(id, model): Observable<any> {
    return this.http.patch(`${environment.serverUrl}users/${id}/changepassword`, model);
  }

  delete(id): Observable<any> {
    return this.http.delete<User>(`${environment.serverUrl}users/${id}`);
  }

  get(id): Observable<User> {
    return this.http.get<User>(`${environment.serverUrl}users/${id}`);
  }

  getAll(): Observable<User> {
    return this.http.get<User>(`${environment.serverUrl}users`);
  }

}
