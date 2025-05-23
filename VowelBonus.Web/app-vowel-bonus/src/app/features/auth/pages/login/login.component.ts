import { HttpClientModule } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '@vowel-bonus-app/core/models/user.model';
import { AuthService } from '@vowel-bonus-app/core/services/auth.service';
import { SecureStorageService } from '@vowel-bonus-app/core/services/secure-storage.service';
import { DataUtil } from '@vowel-bonus-app/core/utils/data.util';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [FormsModule],
})
export class LoginComponent {
  destroy$ = new Subject<void>();
  username!: string;
  currentUser: User | null = null;
  constructor(
    private router: Router,
    private authService: AuthService,
    private secureStorageService: SecureStorageService,
    private dataUtil: DataUtil
  ) {
  this.dataUtil.currentUser$
      .pipe(takeUntil(this.destroy$))
      .subscribe(user => this.currentUser = user);
  }

   goToHamePage() {
    this.router.navigate(['/home']);
  }

  onLogin() {
    this.authService.login(this.username).subscribe({
      next: (res) => {
        if (res.succeeded && res.result) {
          this.dataUtil.currentUser$.next(res.result);
          this.goToHamePage()
        }
      },
      error: (error) => {
        console.log('ไม่สามารถเข้าสู่ระบบได้');
      },
    });
  }

   ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
