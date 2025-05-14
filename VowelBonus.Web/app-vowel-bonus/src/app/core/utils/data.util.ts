import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from '@env/environment';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class DataUtil {
  currentUser$: BehaviorSubject<User>;
  private user?: User;

  constructor() {
    this.currentUser$ = new BehaviorSubject<any | null>(this.user);

    this.currentUser$.subscribe({
      next: (data) => {
        this.user = data;
      },
    });
  }
}
