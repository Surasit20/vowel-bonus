import { Component, Input } from '@angular/core';
import { User } from '@vowel-bonus-app/core/models/user.model';
import { AuthService } from '@vowel-bonus-app/core/services/auth.service';
import { DataUtil } from '@vowel-bonus-app/core/utils/data.util';

@Component({
  selector: 'app-navbar',
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  currentUser?: User;
  constructor(private dataUtil: DataUtil, private authService: AuthService) {
    this.dataUtil.currentUser$.subscribe({
      next: (currentUser) => {
        this.currentUser = currentUser;
      },
    });
  }

  logout() {
    this.authService.logout();
    this.authService.gotoLoginPage();
  }
}
