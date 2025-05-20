import { Component } from '@angular/core';
import { NavbarComponent } from "../../../../shared/navbar/navbar.component";
import { SidebarComponent } from "../../../../shared/sidebar/sidebar.component";
import { ContentComponent } from "../../../../shared/content/content.component";
import { User } from '@vowel-bonus-app/core/models/user.model';
import { DataUtil } from '@vowel-bonus-app/core/utils/data.util';

@Component({
  selector: 'app-home',
  imports: [NavbarComponent, ContentComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
 
}
