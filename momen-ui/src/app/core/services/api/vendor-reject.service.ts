import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { VendorReject } from '../../models';

@Injectable({
  providedIn: 'root'
})

export class VendorRejectService {

  constructor(
    private http: HttpClient
  ) { }

  create(model: any): Observable<any> {
    return this.http.post<VendorReject>(`${environment.serverUrl}vendorRejects`, model);
  }

  getAll(vendorId: number): Observable<VendorReject> {
    return this.http.get<VendorReject>(`${environment.serverUrl}vendorRejects/Vendors/${vendorId}`);
  }

}
