import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { VendorUser } from '../../models';

@Injectable({
  providedIn: 'root'
})

export class VendorUserService {

  constructor(
    private http: HttpClient
  ) { }

  create(model: any): Observable<any> {
    return this.http.post<VendorUser>(`${environment.serverUrl}vendorUsers`, model);
  }

  edit(id: number, model: any): Observable<any> {
    return this.http.put<VendorUser>(`${environment.serverUrl}vendorUsers/${id}`, model);
  }

  resetPassword(id: number): Observable<any> {
    return this.http.patch<any>(`${environment.serverUrl}vendorUsers/${id}/ResetPassword`, null);
  }

  delete(id: number): Observable<any> {
    return this.http.delete<VendorUser>(`${environment.serverUrl}vendorUsers/${id}`);
  }

  get(id: number): Observable<VendorUser> {
    return this.http.get<VendorUser>(`${environment.serverUrl}vendorUsers/${id}`);
  }

  getAll(vendorId: number): Observable<VendorUser> {
    return this.http.get<VendorUser>(`${environment.serverUrl}vendorUsers/Vendors/${vendorId}`);
  }

}
