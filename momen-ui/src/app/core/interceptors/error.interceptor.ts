import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Router, NavigationExtras } from '@angular/router';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})

export class ErrorInterceptor implements HttpInterceptor {

  constructor(
    private router: Router,
    private toastrService: ToastrService
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        if (error) {
          if (error.status === 400) {
            this.toastrService.error(error.error.message, 'Error');
          }
          if (error.status === 401) {
            this.toastrService.error(error.error.message, 'Error');
            this.router.navigate(['/login']);
          }
          if (error.status === 404) {
            this.router.navigateByUrl('/not-found');
          }
          if (error.status === 409) {
            this.toastrService.error(error.error.message, 'Error');
          }
          if (error.status === 500) {
            this.toastrService.error(error.error.message, 'Error');
          }
        }
        return throwError(error);
      })
    );
  }

}
