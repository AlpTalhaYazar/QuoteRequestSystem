import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, Observable, of, throwError} from 'rxjs';
import {catchError, map, tap} from 'rxjs/operators';
import {ApiResponse} from '../models/api-responses/api-response.model';
import {UserLoginResponseDto} from '../models/api-responses/user-login-response-dto';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private readonly baseUrl = 'http://localhost:5184';
  private readonly tokenKey = 'jwt';
  public isLoggedInValue = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {
  }

  login(email: string, password: string): Observable<ApiResponse<boolean>> {
    return this.http.post<ApiResponse<boolean>>(`${this.baseUrl}/api/v1/auth/login`,
      {email, password}, {withCredentials: true})
      .pipe(
        tap(response => {
          if (response.isSuccess && response.data) {
            this.isLoggedInValue.next(true);
            localStorage.setItem('hasJwt', 'true');
          } else if (!response.isSuccess) {
          }
        }),
        catchError(error => {
          return throwError(error);
        })
      );
  }

  logout(): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/v1/auth/logout`, null)
      .pipe(
        tap(response => {
          if ((response as any).data === true) {
            this.isLoggedInValue.next(false);
            localStorage.removeItem('hasJwt');
          }
        }),
        catchError(error => {
          return throwError(error);
        })
      );
  }


  register(email: string, password: string, firstName: string, lastName: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/v1/auth/register`, {email, password, firstName, lastName});
  }

  checkSession(): Observable<any> {
    return this.http.get(`${this.baseUrl}/api/v1/auth/check-session`, {withCredentials: true})
      .pipe(
        tap(response => {
          if ((response as any).data.isAuthenticated === true) {
            this.isLoggedInValue.next(true);
          }
        }),
        catchError(error => {
          this.isLoggedInValue.next(false);
          return throwError(error);
        })
    );
  }

  isLoggedIn(): Observable<boolean> {
    return this.isLoggedInValue.asObservable();
  }
}
