import {Injectable} from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from '@angular/router';
import {Observable, of, throwError} from 'rxjs';
import {catchError, map, take, tap} from 'rxjs/operators';
import {AuthService} from '../services/auth.service';
import {HttpErrorResponse} from "@angular/common/http";
import {NzMessageService} from "ng-zorro-antd/message";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router, private message: NzMessageService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    const hasJwt = localStorage.getItem('hasJwt');
    if (!hasJwt && (state.url === '/login' || state.url === '/register')) {
      this.message.error('You are not logged in');
      return of(true);
    }
    if (hasJwt && (state.url === '/login' || state.url === '/register')) {
      this.message.error('You are already logged in');
      this.router.navigate(['/offer']);
      return of(false);
    }

    return this.authService.checkSession().pipe(
      tap(isLoggedIn => {
          if (!isLoggedIn) {
            console.log('redirecting to login page');
            setTimeout(() => {
              this.router.navigate(['/login']);
            }, 1000);
            return false;
          }// if page is login page, redirect to offer page
          else if (state.url === '/login' || state.url === '/register') {
            console.log('redirecting to offer page');
            setTimeout(() => {
              this.router.navigate(['/offer']);
            }, 1000);
            return false;
          }
          console.log('isLoggedIn:', isLoggedIn);
          setTimeout(() => {
          }, 1000);
          return true;
        }
      ),
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          if (state.url === '/login' || state.url === '/register') {
            return of(true);
          } else {
            console.log('redirecting to login page2');
            setTimeout(() => {
              this.router.navigate(['/login']);
            }, 1000);
            return of(false);
          }
        }
        return throwError(error);
      })
    );
  }
}
