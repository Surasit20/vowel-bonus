import { Component, Input, ViewChild } from '@angular/core';
import { User } from '@vowel-bonus-app/core/models/user.model';
import { AuthService } from '@vowel-bonus-app/core/services/auth.service';
import { DataUtil } from '@vowel-bonus-app/core/utils/data.util';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { Subject, takeUntil } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { UserService } from '@vowel-bonus-app/core/services/user.service';
import { CommonModule } from '@angular/common';
import { ToastComponent } from "../toast/toast.component";

@Component({
  selector: 'app-navbar',
  imports: [ConfirmDialogComponent, FormsModule, CommonModule, ToastComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  destroy$ = new Subject<void>();
  currentUser?: User;
  contentConfirm: string = 'Are you sure you want to log out?';
  showDialog = false;
  isEditing = false;
  editedUserName = '';

  @ViewChild(ConfirmDialogComponent) confirmDialog!: ConfirmDialogComponent;
  @ViewChild('toast') toastComponent!: ToastComponent;
  constructor(
    private dataUtil: DataUtil,
    private authService: AuthService,
    private userService: UserService
  ) {
    this.dataUtil.currentUser$
      .pipe(takeUntil(this.destroy$))
      .subscribe((user) => (this.currentUser = user));
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

  startEdit() {
    this.isEditing = true;
    this.editedUserName = this.currentUser?.userName || '';
  }

  cancelEdit() {
    this.isEditing = false;
  }

  saveUserName() {
    if (this.editedUserName.trim()) {
      this.userService
        .updateUser(this.currentUser?.userId ?? 0, this.editedUserName.trim())
        .subscribe({
          next: (res) => {
            if (res.succeeded && res.result && this.currentUser) {
              this.currentUser = res.result;
              this.dataUtil.currentUser$.next(this.currentUser);
            }
            else{
              this.toastComponent.showToast(res.message, 'error');
            }
          },
          error: (error) => {
            console.log(error);
          },
        });
    }
    this.isEditing = false;
  }

  logout() {
    this.authService.logout();
    this.authService.gotoLoginPage();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
