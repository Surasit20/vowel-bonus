import { SecureStorageService } from './secure-storage.service';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '@env/environment.development';
import { catchError, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = environment.apiUrl + 'Auth/';

  private currentUser: any = null;

  constructor(
    private http: HttpClient,
    public router: Router,
    private secureStorageService: SecureStorageService
  ) {
   
  }

  public get userName() {
    return this.secureStorageService.recallUserName();
  }
  public get userLoggedIn() {
    return this.currentUser;
  }

  login(username: string) {
    return this.http
      .post(this.apiUrl + 'Login', {
        username: username,
      })
      .pipe(
        map((result: any) => {
          return result;
        }),
        catchError((error: HttpErrorResponse) => {
          console.error(
            error.statusText,
            error.status,
            error.message,
            error.error
          );
          return error.error;
        })
      );
  }

  logout() {
    return this.http.post(this.apiUrl + 'Logout', {}).subscribe({
      next: () => {
        this.secureStorageService.removeUserName();
        this.gotoLoginPage();
      },
    });
  }

  gotoLoginPage() {
    this.router.navigate(['login']);
  }
}
