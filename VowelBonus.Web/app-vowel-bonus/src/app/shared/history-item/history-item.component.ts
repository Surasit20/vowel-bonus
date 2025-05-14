import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { VowelBonusScoreHistory } from '@vowel-bonus-app/core/models/vowel-bonus-score-history.model';


@Component({
  selector: 'app-history-item',
  imports: [CommonModule],
  templateUrl: './history-item.component.html',
  styleUrl: './history-item.component.css'
})
export class HistoryItemComponent {
  @Input({ required: true }) historyItem?: VowelBonusScoreHistory[];
}
