import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Contract } from '../../models';

@Injectable({
  providedIn: 'root'
})

export class ContractService {

  constructor(
    private http: HttpClient
  ) { }

  create(model): Observable<any> {
    return this.http.post<Contract>(`${environment.serverUrl}contracts`, model);
  }

  edit(id, model): Observable<any> {
    return this.http.put<Contract>(`${environment.serverUrl}contracts/${id}`, model);
  }

  uploadFile(id, model): Observable<any> {
    return this.http.patch(`${environment.serverUrl}contracts/${id}/file`, model);
  }

  delete(id): Observable<any> {
    return this.http.delete<Contract>(`${environment.serverUrl}contracts/${id}`);
  }

  get(id): Observable<Contract> {
    return this.http.get<Contract>(`${environment.serverUrl}contracts/${id}`);
  }

  getAll(): Observable<Contract> {
    return this.http.get<Contract>(`${environment.serverUrl}contracts`);
  }

}
