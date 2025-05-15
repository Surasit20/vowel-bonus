import { Component, Input, ViewChild } from '@angular/core';
import { User } from '@vowel-bonus-app/core/models/user.model';
import { AuthService } from '@vowel-bonus-app/core/services/auth.service';
import { DataUtil } from '@vowel-bonus-app/core/utils/data.util';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-navbar',
  imports: [ConfirmDialogComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  currentUser?: User;
  contentConfirm: string = 'Are you sure you want to log out?';
  showDialog = false;

  @ViewChild(ConfirmDialogComponent) confirmDialog!: ConfirmDialogComponent;

  constructor(private dataUtil: DataUtil, private authService: AuthService) {
    this.dataUtil.currentUser$.subscribe({
      next: (currentUser) => {
        this.currentUser = currentUser;
      },
    });
  }

  confirmLogout() {
    this.confirmDialog.onOpenDialog();
  }

  handleConfirm() {
    this.logout();
  }

  handleCancel() {
    this.confirmDialog.onCloseDialog();
  }

  logout() {
    this.authService.logout();
    this.authService.gotoLoginPage();
  }
}
