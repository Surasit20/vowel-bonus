import {
  Component,
  ContentChild,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import { HistoryItemComponent } from '../history-item/history-item.component';

@Component({
  selector: 'app-list-dialog',
  imports: [],
  templateUrl: './list-dialog.component.html',
  styleUrl: './list-dialog.component.css',
})
export class ListDialogComponent {
  @ContentChild(HistoryItemComponent) historyItem!: HistoryItemComponent;

  @Input({ required: true }) headerText!: string;
  @Output() closed = new EventEmitter<void>();

  showDialog = false;

  onClose() {
    this.closed.emit();
  }

  onOpenDialog() {
    this.historyItem.ngOnChanges();
    this.showDialog = true;
  }

  onCloseDialog() {
    this.showDialog = false;
  }
}
