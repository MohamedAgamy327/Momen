import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Vendor } from '../../models';

@Injectable({
  providedIn: 'root'
})

export class VendorService {

  constructor(
    private http: HttpClient
  ) { }

  create(model: any): Observable<any> {
    return this.http.post<Vendor>(`${environment.serverUrl}vendors`, model);
  }

  edit(id: number, model: any): Observable<any> {
    return this.http.put<Vendor>(`${environment.serverUrl}vendors/${id}`, model);
  }

  uploadLogo(id: number, model: any): Observable<any> {
    return this.http.patch(`${environment.serverUrl}vendors/${id}/logofile`, model);
  }

  uploadLicense(id: number, model: any): Observable<any> {
    return this.http.patch(`${environment.serverUrl}vendors/${id}/licensefile`, model);
  }

  uploadPersonalId(id: number, model: any): Observable<any> {
    return this.http.patch(`${environment.serverUrl}vendors/${id}/personalIdfile`, model);
  }

  delete(id: number): Observable<any> {
    return this.http.delete<Vendor>(`${environment.serverUrl}vendors/${id}`);
  }

  get(id: number): Observable<Vendor> {
    return this.http.get<Vendor>(`${environment.serverUrl}vendors/${id}`);
  }

  getAll(): Observable<Vendor> {
    return this.http.get<Vendor>(`${environment.serverUrl}vendors`);
  }

}
