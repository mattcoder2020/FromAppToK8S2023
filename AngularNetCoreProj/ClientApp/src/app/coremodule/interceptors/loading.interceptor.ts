import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, finalize } from 'rxjs';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private spinnerService: NgxSpinnerService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.spinnerService.show();
    return next.handle(request).pipe(finalize(() => this.spinnerService.hide()));
  }
}
