import { Component, Input } from '@angular/core';
import { User } from '@vowel-bonus-app/core/models/user.model';
import { DataUtil } from '@vowel-bonus-app/core/utils/data.util';
import { HistoryItemComponent } from '../history-item/history-item.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sidebar',
  imports: [HistoryItemComponent,CommonModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
})
export class SidebarComponent {
  currentUser: User | null = null;

  constructor(private dataUtil: DataUtil) {
      this.dataUtil.currentUser$.subscribe({
      next: (currentUser) => {
        this.currentUser = currentUser;
      },
    });
  }
}
