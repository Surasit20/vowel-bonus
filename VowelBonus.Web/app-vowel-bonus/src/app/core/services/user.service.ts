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
export class UserService {
  private apiUrl = environment.apiUrl + 'Users/';

  constructor(
    private http: HttpClient,
    private secureStorageService: SecureStorageService
  ) {}

  updateUser(userId: number, username: string) {
    return this.http
      .patch<ApiResponse<User>>(this.apiUrl + 'UpdateUser', {
        UserId: userId,
        UserName: username,
      })
      .pipe(
        map((result: ApiResponse<User>) => {
          if (result?.succeeded && result?.result) {
            let user = {
              userId: result?.result.userId,
              userName: result?.result.userName,
            };
            this.secureStorageService.rememberUser(user);
          }

          return result;
        }),
        catchError((error: HttpErrorResponse) => {
          return throwError(() => error);
        })
      );
  }
}
