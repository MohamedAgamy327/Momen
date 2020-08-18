import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Category } from '../../models';

@Injectable({
  providedIn: 'root'
})

export class CategoryService {

  constructor(
    private http: HttpClient
  ) { }

  create(model: any): Observable<any> {
    return this.http.post<Category>(`${environment.serverUrl}Categories`, model);
  }

  edit(id: number, model: any): Observable<any> {
    return this.http.put<Category>(`${environment.serverUrl}Categories/${id}`, model);
  }

  delete(id: number): Observable<any> {
    return this.http.delete<Category>(`${environment.serverUrl}Categories/${id}`);
  }

  get(id: number): Observable<Category> {
    return this.http.get<Category>(`${environment.serverUrl}Categories/${id}`);
  }

  getAll(): Observable<Category> {
    return this.http.get<Category>(`${environment.serverUrl}Categories`);
  }

}
