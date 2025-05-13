import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PointService } from '@vowel-bonus-app/core/services/point.service';
import { DataUtil } from '@vowel-bonus-app/core/utils/data.util';

@Component({
  selector: 'app-content',
  imports: [FormsModule],
  templateUrl: './content.component.html',
  styleUrl: './content.component.css',
})
export class ContentComponent {
  word!: string;
  totalPoint: number = 0;
  constructor(private pointService: PointService, private dataUtil: DataUtil) {
    this.dataUtil.currentTotalPoint$.subscribe({
      next: (totalPoint) => {
        this.totalPoint = totalPoint;
      },
    });
  }

  onSend() {
    this.pointService.calculatePoint(this.word).subscribe({
      next: (res: any) => {
        if (res.succeeded) {
          this.totalPoint = res.result.totalPoint;
          this.dataUtil.currentTotalPoint$.next(this.totalPoint);
          this.word = "";
        }
      },
      error: (error) => {
        console.log('Error Something');
      },
    });
  }
}
