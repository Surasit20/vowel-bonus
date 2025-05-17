import { SecureStorageService } from './secure-storage.service';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '@env/environment.development';
import { catchError, map, throwError } from 'rxjs';
import { User } from '../models/user.model';
import { ApiResponse } from '../models/api-response.model';

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
  ) {}

  public get userLoggedIn() {
    return this.secureStorageService.recallUser();
  }

  login(username: string) {
    return this.http
      .post<ApiResponse<User>>(this.apiUrl + 'Login', {
        UserName: username,
      })
      .pipe(
        map((result: ApiResponse<User>) => {
          if (result?.succeeded && result?.result) {
            let user = {userId:result?.result.userId , 
                        userName:result?.result.userName}
            this.secureStorageService.rememberUser(user);
          }

          return result;
        }),
        catchError((error: HttpErrorResponse) => {
          return throwError(() => error);
        })
      );
  }

  logout() {
    this.secureStorageService.removeUser();
    //Wait For Implement API

    // return this.http.post(this.apiUrl + 'Logout', {}).subscribe({
    //   next: () => {
    //     this.secureStorageService.removeUser();
    //     this.gotoLoginPage();
    //   },
    // });
  }

  gotoLoginPage() {
    this.router.navigate(['login']);
  }

  gotoHomePage() {
    this.router.navigate(['home']);
  }
}
