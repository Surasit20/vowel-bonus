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
  currentUser?: User;
  constructor(private pointService: PointService, private dataUtil: DataUtil) {
    this.dataUtil.currentUser$.subscribe({
      next: (currentUser) => {
        this.currentUser = currentUser;
      },
    });
  }

  onSend() {
    this.pointService.calculatePoint(this.word).subscribe({
      next: (res) => {
        if (res.succeeded && res.result && this.currentUser) {
          this.currentUser.totalPoint = res.result.totalPoint;
          this.currentUser.vowelBonusScoreHistories =
            res.result.vowelBonusScoreHistories;
          this.dataUtil.currentUser$.next(this.currentUser);
          this.word = '';
        }
      },
      error: (error) => {
        console.log('Error Something');
      },
    });
  }

  clearInput() {
    this.word = '';
  }

  allowOnlyEnglishLetters(event: KeyboardEvent) {
    console.log(event.key)
    const pattern = /^[a-zA-Z]+$/;
    const inputChar = event.key;
    if (!pattern.test(inputChar)) {
      event.preventDefault();
    }
  }
}
