import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { User } from '@vowel-bonus-app/core/models/user.model';
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
  currentUser: User | null = null;
  constructor(private pointService: PointService, private dataUtil: DataUtil) {
      this.dataUtil.currentUser$.subscribe({
      next: (currentUser) => {
        this.currentUser = currentUser;
      },
    });
  }

  onSend() {
    this.pointService.calculatePoint(this.word).subscribe({
      next: (res: any) => {
        if (res.succeeded) {
          this.totalPoint = res.result.totalPoint;
          this.dataUtil.currentUser$.next(res.result);
          this.word = "";
        }
      },
      error: (error) => {
        console.log('Error Something');
      },
    });
  }
}
