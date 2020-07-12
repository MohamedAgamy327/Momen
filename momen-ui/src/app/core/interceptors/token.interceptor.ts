import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CredentialService } from '../services';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class TokenInterceptor implements HttpInterceptor {

  constructor(
    private credentialService: CredentialService
  ) { }

  intercept(httpReq: HttpRequest<any>, next: HttpHandler):
    Observable<HttpEvent<any>> {
    let headers;
    if (this.credentialService.getToken()) {
      headers = httpReq.headers.set('Authorization', `bearer ${this.credentialService.getToken()}`);
    }
    const newReq = httpReq.clone({ headers });
    return next.handle(newReq);
  }
}
