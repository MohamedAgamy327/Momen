import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Customer } from '../../models';

@Injectable({
  providedIn: 'root'
})

export class CustomerService {

  constructor(
    private http: HttpClient
  ) { }

  create(model: any): Observable<any> {
    return this.http.post<Customer>(`${environment.serverUrl}customers`, model);
  }

  edit(id: number, model: any): Observable<any> {
    return this.http.put<Customer>(`${environment.serverUrl}customers/${id}`, model);
  }

  uploadFile(id: number, model: any): Observable<any> {
    return this.http.patch(`${environment.serverUrl}customers/${id}/file`, model);
  }

  changePassword(id: number, model: any): Observable<any> {
    return this.http.patch<any>(`${environment.serverUrl}customers/${id}/ChangePassword`, model);
  }

  resetPassword(id: number): Observable<any> {
    return this.http.patch<any>(`${environment.serverUrl}customers/${id}/ResetPassword`, null);
  }

  block(id: number): Observable<any> {
    return this.http.patch<any>(`${environment.serverUrl}customers/${id}/block`, null);
  }

  unblock(id: number): Observable<any> {
    return this.http.patch<any>(`${environment.serverUrl}customers/${id}/unblock`, null);
  }

  delete(id: number): Observable<any> {
    return this.http.delete<Customer>(`${environment.serverUrl}customers/${id}`);
  }

  get(id: number): Observable<Customer> {
    return this.http.get<Customer>(`${environment.serverUrl}customers/${id}`);
  }

  getAll(): Observable<Customer> {
    return this.http.get<Customer>(`${environment.serverUrl}customers`);
  }

}
