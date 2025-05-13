import { Component, Input } from '@angular/core';
import { DataUtil } from '@vowel-bonus-app/core/utils/data.util';

@Component({
  selector: 'app-sidebar',
  imports: [],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
})
export class SidebarComponent {
  totalPoint: number = 0;

  constructor(private dataUtil: DataUtil) {
    this.dataUtil.currentTotalPoint$.subscribe({
      next: (totalPoint) => {
        this.totalPoint = totalPoint;
      },
    });
  }
}
