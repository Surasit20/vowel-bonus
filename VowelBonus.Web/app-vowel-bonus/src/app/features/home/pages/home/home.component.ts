import { Component } from '@angular/core';
import { NavbarComponent } from "../../../../shared/navbar/navbar.component";
import { SidebarComponent } from "../../../../shared/sidebar/sidebar.component";
import { ContentComponent } from "../../../../shared/content/content.component";

@Component({
  selector: 'app-home',
  imports: [NavbarComponent, SidebarComponent, ContentComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  point = 10;
}
