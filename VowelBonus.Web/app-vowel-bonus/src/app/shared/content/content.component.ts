import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PointService } from '@vowel-bonus-app/core/services/point.service';

@Component({
  selector: 'app-content',
  imports: [FormsModule],
  templateUrl: './content.component.html',
  styleUrl: './content.component.css',
})
export class ContentComponent {
  word!: string;

  constructor(private pointService: PointService) {}

    onSend() {
      this.pointService.calculatePoint(this.word).subscribe({
      next: (res) => {
          console.log(res)
        if (res.succeeded) {
        }
      },
      error: (error) => {
        console.log('Error Something');
      },
    });

  }
}
