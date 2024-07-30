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
export class RouteGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router, private message: NzMessageService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    const hasJwt = localStorage.getItem('hasJwt');

    this.message.error('Wriong route');

    if (hasJwt) {
      this.router.navigate(['/offer']);
      return of(false);
    } else {
      this.router.navigate(['/login']);
      return of(false);
    }
  }
}
