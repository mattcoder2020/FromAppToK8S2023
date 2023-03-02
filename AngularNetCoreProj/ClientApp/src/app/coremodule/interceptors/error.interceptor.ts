import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router, NavigationExtras } from '@angular/router';
//import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

 constructor(private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        if (error) {
          if (error.status === 400) {
            if (error.error.errors) {
              this.router.navigateByUrl('/core/exceptionview');
            } 
          }
          //if (error.status === 401) {
          //  this.toastr.error(error.error.message, error.error.statusCode);
          //}
          if (error.status === 404) {
            this.router.navigateByUrl('/core/exceptionview');
          }
          if (error.status === 500) {
            const navigationExtras: NavigationExtras = { state: { error: error.error } }
            this.router.navigateByUrl('/core/exceptionview', navigationExtras);
          }
        }
        return throwError(error);
      })
    )
  }
}
