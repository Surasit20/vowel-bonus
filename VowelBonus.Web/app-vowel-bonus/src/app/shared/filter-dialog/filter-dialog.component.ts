import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-filter-dialog',
  imports: [],
  templateUrl: './filter-dialog.component.html',
  styleUrl: './filter-dialog.component.css'
})
export class FilterDialogComponent {
  @Output() applied = new EventEmitter<void>();
  @Output() reseted = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();

  showDialog = false;

  onApply() {
    this.applied.emit();
  }

  onCancel() {
    this.cancelled.emit();
  }

  onReset() {
    this.reseted.emit();
  }


  onOpenDialog() {
    this.showDialog = true;
  }

  onCloseDialog() {
    this.showDialog = false;
  }

}
