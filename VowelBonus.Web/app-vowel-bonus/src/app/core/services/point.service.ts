import { SecureStorageService } from './secure-storage.service';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '@env/environment.development';
import { catchError, map, throwError } from 'rxjs';
import { AuthService } from './auth.service';
import { VowelBonusScoreResponse } from '../models/vowel-bonus-score-response.model';
import { ApiResponse } from '../models/api-response.model';
import { VowelBonusScoreHistory } from '../models/vowel-bonus-score-history.model';

@Injectable({
  providedIn: 'root',
})
export class PointService {
  private apiUrl = environment.apiUrl + 'VowelBonusScoreHistories/';

  private currentUser: any = null;

  constructor(
    private http: HttpClient,
    public router: Router,
    private secureStorageService: SecureStorageService,
    private authService: AuthService
  ) {}

  calculatePoint(word: string) {
    return this.http
      .post<ApiResponse<VowelBonusScoreResponse>>(
        this.apiUrl + 'CreateVowelBonusScoreHistories',
        {
          Word: word,
          UserId: this.authService.userLoggedIn?.userId,
        }
      )
      .pipe(
        map((result: ApiResponse<VowelBonusScoreResponse>) => {
          if (result?.succeeded && result?.result) {
          }

          return result;
        }),
        catchError((error: HttpErrorResponse) => {
          return throwError(() => error);
        })
      );
  }

  getVowelBonusScoreHistoriesByFilter(
    pageSize: number,
    currentPage: number,
    startWord?: string | null,
    endWord?: string | null,
    startPoint?: number | null,
    endPorint?: number| null,
    sortBy?: string | null,
    sortDirection?: string | null,

  ) {
    return this.http
      .post<ApiResponse<VowelBonusScoreHistory[]>>(
        this.apiUrl + 'GetVowelBonusScoreHistoriesByFilter',
        {
          UserId: this.authService.userLoggedIn?.userId,
          Page: currentPage,
          PageSize: pageSize,
          StartWord: startWord,
          EndWord: endWord,
          StartPoint: startPoint,
          EndPorint: endPorint,
          SortBy: sortBy,
          SortDirection:sortDirection
        }
      )
      .pipe(
        map((result: ApiResponse<VowelBonusScoreHistory[]>) => {
          return result;
        }),
        catchError((error: HttpErrorResponse) => {
          return throwError(() => error);
        })
      );
  }
}
