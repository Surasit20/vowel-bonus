import { Injectable } from '@angular/core';
import { environment } from '@env/environment.development';
import { User } from '../models/user.model';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SecureStorageService {
  constructor() {}
  private currentUserId: number | undefined;

  /* ============== User ================ */

  recallUser(): User | null {
    let data: string | null = null;
    if (localStorage) {
      data = localStorage.getItem(environment.storageKeys.user);
    }

    if (data) {
      const user: User = JSON.parse(data);
      return user;
    }
    
    return null;
  }
  rememberUser(data: any) {
    if (localStorage) {
      if (data) {
        localStorage.setItem(environment.storageKeys.user, JSON.stringify(data));
      } else {
        localStorage.removeItem(environment.storageKeys.user);
      }
      return true;
    }
    return false;
  }

  removeUser() {
    return this.rememberUser(null);
  }
}
