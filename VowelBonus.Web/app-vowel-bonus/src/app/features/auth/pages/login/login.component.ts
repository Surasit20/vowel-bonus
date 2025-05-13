import { HttpClientModule } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '@vowel-bonus-app/core/services/auth.service';
import { SecureStorageService } from '@vowel-bonus-app/core/services/secure-storage.service';
import { DataUtil } from '@vowel-bonus-app/core/utils/data.util';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [FormsModule],
})
export class LoginComponent {
  username!: string;
  totalPoint: number = 0;
  constructor(
    private router: Router,
    private authService: AuthService,
    private secureStorageService: SecureStorageService,
    private dataUtil: DataUtil
  ) {
    this.dataUtil.currentTotalPoint$.subscribe({
      next: (totalPoint) => {
        this.totalPoint = totalPoint;
      },
    });
  }

  onLogin() {
    this.authService.login(this.username).subscribe({
      next: (res) => {
        if (res.succeeded && res.result) {
          this.totalPoint = res.result.totalPoint ?? 0;
          this.dataUtil.currentTotalPoint$.next(this.totalPoint);
          this.authService.gotoHomePage();
        }
      },
      error: (error) => {
        console.log('ไม่สามารถเข้าสู่ระบบได้');
      },
    });
  }
}
