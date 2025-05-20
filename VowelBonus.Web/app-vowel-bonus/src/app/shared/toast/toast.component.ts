import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-toast',
  imports: [CommonModule],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.css'
})
export class ToastComponent {
  @Input() message = '';
  @Input() type: 'success' | 'error' | 'info' = 'info';
  show = false;

  showToast(msg?: string, type: 'success' | 'error' | 'info' = 'info') {
    this.message = msg ?? '';
    this.type = type;
    this.show = true;

    setTimeout(() => {
      this.show = false;
    }, 4000); 
  }
}
