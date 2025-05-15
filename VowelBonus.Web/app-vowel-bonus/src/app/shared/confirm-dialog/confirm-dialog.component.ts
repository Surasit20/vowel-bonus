import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-confirm-dialog',
  imports: [],
  templateUrl: './confirm-dialog.component.html',
  styleUrl: './confirm-dialog.component.css',
})
export class ConfirmDialogComponent {
  @Input() content!: string;
  @Input() showDialog = false;
  @Output() confirmed = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();


  onConfirm() {
    this.confirmed.emit();
    this.showDialog = false;
  }

  onCancel() {
    this.cancelled.emit();
    this.showDialog = false;
  }

  onOpenDialog() {
    this.showDialog = true;
  }

  onCloseDialog() {
    this.showDialog = false;
  }
}
