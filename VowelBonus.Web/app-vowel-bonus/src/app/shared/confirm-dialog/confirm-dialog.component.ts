import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-confirm-dialog',
  imports: [],
  templateUrl: './confirm-dialog.component.html',
  styleUrl: './confirm-dialog.component.css',
})
export class ConfirmDialogComponent {
  @Input() content!: string;
  @Output() confirmed = new EventEmitter<void>();
  @Output() confirmedWithData = new EventEmitter<any>();
  @Output() cancelled = new EventEmitter<void>();

  showDialog = false;
  context: any;

  onConfirm() {
    if (this.context) {
      this.confirmedWithData.emit(this.context);
    } else {
      this.confirmed.emit();
    }
  }

  onCancel() {
    this.cancelled.emit();
  }

  onOpenDialog(): void;
  onOpenDialog(context: any): void;
  onOpenDialog(context?: any): void {
    if (context) {
      this.context = context;
    }
    this.showDialog = true;
  }

  onCloseDialog() {
    this.showDialog = false;
  }
}
