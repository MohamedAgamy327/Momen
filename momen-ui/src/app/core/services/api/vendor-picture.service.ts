import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { VendorPicture } from '../../models';

@Injectable({
  providedIn: 'root'
})

export class VendorPictureService {

  constructor(
    private http: HttpClient
  ) { }

  create(model: any): Observable<any> {
    return this.http.post<VendorPicture>(`${environment.serverUrl}vendorPictures`, model);
  }

  delete(id: number): Observable<any> {
    return this.http.delete<VendorPicture>(`${environment.serverUrl}vendorPictures/${id}`);
  }

  getAll(vendorId: number): Observable<VendorPicture> {
    return this.http.get<VendorPicture>(`${environment.serverUrl}vendorPictures/vendors/${vendorId}`);
  }

}
