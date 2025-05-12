import { SecureStorageService } from './secure-storage.service';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '@env/environment.development';
import { catchError, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PointService {
  private apiUrl = environment.apiUrl + 'VowelBonusScoreHistories/';

  private currentUser: any = null;

  constructor(
    private http: HttpClient,
    public router: Router,
    private secureStorageService: SecureStorageService
  ) {
   
  }


  calculatePoint(word: string) {
    return this.http
      .post(this.apiUrl + 'CreateVowelBonusScoreHistories', {
        Word: word,
        UserId:1 //TODO: เดี๋ยวกลับมาแก้
      })
      .pipe(
        map((result: any) => {
          return result;
        }),
        catchError((error: HttpErrorResponse) => {
          console.error(error)
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
}
