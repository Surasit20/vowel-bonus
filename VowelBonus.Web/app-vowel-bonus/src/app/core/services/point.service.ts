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
import { User } from '../models/user.model';
import { DataUtil } from '../utils/data.util';

@Injectable({
  providedIn: 'root',
})
export class PointService {
  private apiUrl = environment.apiUrl + 'VowelBonusScoreHistories/';
  currentUser: User | null = null;

  constructor(
    private http: HttpClient,
    public router: Router,
    private secureStorageService: SecureStorageService,
    private authService: AuthService,
    private dataUtil: DataUtil
  ) {
    this.dataUtil.currentUser$.subscribe((user) => (this.currentUser = user));
  }

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
    endPoint?: number | null,
    sortBy?: string | null,
    sortDirection?: string | null
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
          EndPoint: endPoint,
          SortBy: sortBy,
          SortDirection: sortDirection,
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

  updateHistory(historyId: number, word: string) {
    return this.http
      .patch<ApiResponse<VowelBonusScoreHistory>>(
        this.apiUrl + 'UpdateVowelBonusScoreHistory',
        {
          VowelBonusScoreHistoryId: historyId,
          Word: word,
        }
      )
      .pipe(
        map((result: ApiResponse<VowelBonusScoreHistory>) => {
          if (result?.succeeded && result?.result) {
          }

          return result;
        }),
        catchError((error: HttpErrorResponse) => {
          return throwError(() => error);
        })
      );
  }

  deleteHistory(historyId: number) {
    return this.http
      .delete<ApiResponse<boolean>>(
        this.apiUrl + 'DeleteVowelBonusScoreHistory' + '?Id=' + historyId
      )
      .pipe(
        map((result: ApiResponse<boolean>) => {
          return result;
        }),
        catchError((error: HttpErrorResponse) => {
          return throwError(() => error);
        })
      );
  }

  getTotalScore() {
    return this.http
      .get<ApiResponse<number>>(
        this.apiUrl +
          'GetVowelBonusTotalScoreHistory' +
          '?Id=' +
          this.authService.userLoggedIn?.userId,
      )
      .pipe(
        map((result: ApiResponse<number>) => {
         return result;
        }),
        catchError((error: HttpErrorResponse) => {
          return throwError(() => error);
        })
      );
  }
}
