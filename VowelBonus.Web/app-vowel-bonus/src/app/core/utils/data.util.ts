import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root',
})
export class DataUtil {
  currentReceiptOrder$: BehaviorSubject<any | null>;
  private receiptOrder: any = {};

  currentTotalPoint$: BehaviorSubject<number>;
  private totalPoint: number = 0;


  constructor() {
    this.currentReceiptOrder$ = new BehaviorSubject<any | null>(
      this.receiptOrder
    );

    this.currentReceiptOrder$.subscribe({
      next: (data) => {
        this.receiptOrder = data;
      },
    });

    this.currentTotalPoint$ = new BehaviorSubject<number>(
    this.totalPoint
    );

      this.currentTotalPoint$.subscribe({
      next: (data) => {
        this.totalPoint = data;
      },
    });
  }
}
