import { HttpClientModule } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '@vowel-bonus-app/core/services/auth.service';
import { SecureStorageService } from '@vowel-bonus-app/core/services/secure-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [FormsModule],
})
export class LoginComponent {
  username!: string;

  constructor(
    private router: Router,
    private authService: AuthService,
    private secureStorageService: SecureStorageService
  ) {}

  onLogin() {
    this.authService.login(this.username).subscribe({
      next: (res) => {
        console.log(res)
        if (res.succeeded) {
          this.authService.gotoHomePage();
        }
      },
      error: (error) => {
        console.log('ไม่สามารถเข้าสู่ระบบได้');
      },
    });
  }
}
