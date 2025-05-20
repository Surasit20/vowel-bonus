import { Component, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { User } from '@vowel-bonus-app/core/models/user.model';
import { PointService } from '@vowel-bonus-app/core/services/point.service';
import { DataUtil } from '@vowel-bonus-app/core/utils/data.util';
import { Subject, takeUntil } from 'rxjs';
import { ListDialogComponent } from '../list-dialog/list-dialog.component';
import { HistoryItemComponent } from '../history-item/history-item.component';
import { CommonModule } from '@angular/common';
import { ToastComponent } from "../toast/toast.component";

@Component({
  selector: 'app-content',
  imports: [
    FormsModule,
    ListDialogComponent,
    HistoryItemComponent,
    CommonModule,
    ToastComponent
],
  templateUrl: './content.component.html',
  styleUrl: './content.component.css',
})
export class ContentComponent {
 @ViewChild(ListDialogComponent) listDialog!: ListDialogComponent;
@ViewChild('toast') toastComponent!: ToastComponent;

  destroy$ = new Subject<void>();
  word!: string;
  currentUser?: User;
  constructor(private pointService: PointService, private dataUtil: DataUtil) {
    this.dataUtil.currentUser$
      .pipe(takeUntil(this.destroy$))
      .subscribe((user) => (this.currentUser = user));
  }

  onSend() {
    this.pointService.calculatePoint(this.word).subscribe({
      next: (res) => {
        if (res.succeeded && res.result) {
          this.clearInput();
          this.pointService.getTotalScore();
          this.toastComponent.showToast(`The word ‘${res.result.word}’ scored ${res.result.point} points.`, 'success');
        }
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  clearInput() {
    this.word = '';
  }

  allowOnlyEnglishLetters(event: KeyboardEvent) {
    const pattern = /^[a-zA-Z]+$/;
    const inputChar = event.key;
    if (!pattern.test(inputChar)) {
      event.preventDefault();
    }
  }

  handleOpenHistoryDialog() {
    this.listDialog.onOpenDialog();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
