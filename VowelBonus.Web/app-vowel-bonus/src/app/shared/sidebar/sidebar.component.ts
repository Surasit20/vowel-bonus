import { Component, Input } from '@angular/core';
import { User } from '@vowel-bonus-app/core/models/user.model';
import { DataUtil } from '@vowel-bonus-app/core/utils/data.util';

@Component({
  selector: 'app-sidebar',
  imports: [],
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
