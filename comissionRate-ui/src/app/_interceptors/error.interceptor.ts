import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private tostr: ToastrService, private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => { 
        if(error)
        { 
          console.log(error);
          switch(error.status)
          { 
            case 400:
              if(error.error.errors) {
                const modalStateErrors = [];
                for(const key in error.error.errors) {
                  if(error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key]);
                  }
                }
                throw modalStateErrors.flat();
              }
              else if(typeof(error.error) === 'object') {
                if(error.error.title) {
                  this.tostr.error(error.error.title, error.status);
                }
                else if(error.error.length > 0 && error.error[0].code) {
                  const modalStateErrors = [];
                  for(const key in error.error) {
                    if(error.error[key]) {
                      modalStateErrors.push(error.error[key].description);
                    }
                  }
                  throw modalStateErrors.flat();
                }
                else {
                  this.tostr.error(error.error, error.status);
                }
              }
              else {
                this.tostr.error(error.error, error.status);
              }
              break;
            case 401: 
              console.log(error);
              if(typeof(error.error) === 'object') {
                if(error.error.title) {
                  this.tostr.error(error.error.title, error.status);
                }
                else {
                  this.tostr.error(error.error, error.status);
                }
              }
              else {
                this.tostr.error(error.error, error.status);
              }
              break;
            case 404: 
              this.router.navigateByUrl('/not-found');
              break;
            case 500: 
              const navigationExtras: NavigationExtras = {state: {error: error.error}};
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default: this.tostr.error('Something unexpected went wrong'); break;
          }
        }
        return throwError(()=> new Error(error));
      })
    );
  }
}
