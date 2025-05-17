import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { User } from './core/models/user.model';
import { AuthService } from './core/services/auth.service';
import { DataUtil } from './core/utils/data.util';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'app-vowel-bonus';
  currentUser?: User;

  constructor(
    private router: Router,
    private authService: AuthService,
    private dataUtil: DataUtil
  ) {
    this.dataUtil.currentUser$.subscribe({
      next: (currentUser) => {
        this.currentUser = currentUser;
      },
    });
  }

  ngOnInit(): void {
    if (this.authService.userLoggedIn) {
      this.authService
        .login(this.authService.userLoggedIn.userName ?? '')
        .subscribe({
          next: (res) => {
            if (res.succeeded && res.result) {
              console.log(res.result);
              this.dataUtil.currentUser$.next(res.result);
            }
          },
          error: (error) => {
            console.log('ไม่สามารถเข้าสู่ระบบได้');
          },
        });
    } else {
      this.router.navigate(['/login']);
    }
  }
}
