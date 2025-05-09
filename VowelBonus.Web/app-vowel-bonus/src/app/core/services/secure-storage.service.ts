import { Injectable } from '@angular/core';
import { environment } from '@env/environment.development';

@Injectable({
  providedIn: 'root',
})
export class SecureStorageService {
  constructor() {}

  /* ============== User Name ================ */
  recallUserName() {
    let data: any = null;
    if (localStorage) {
      data = localStorage.getItem(environment.storageKeys.userName);
    }
    return data;
  }
  rememberUserName(data: any) {
    if (localStorage) {
      if (data) {
        localStorage.setItem(environment.storageKeys.userName, data);
      } else {
        localStorage.removeItem(environment.storageKeys.userName);
      }
      return true;
    }
    return false;
  }

  removeUserName() {
    return this.rememberUserName(null);
  }
}
