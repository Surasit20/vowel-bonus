import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'app-vowel-bonus';

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.router.navigate(['/login']);
  }
}
