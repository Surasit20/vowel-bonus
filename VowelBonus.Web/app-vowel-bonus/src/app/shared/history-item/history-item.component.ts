import { CommonModule } from '@angular/common';
import { Component, Input, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { VowelBonusScoreHistory } from '@vowel-bonus-app/core/models/vowel-bonus-score-history.model';
import { PointService } from '@vowel-bonus-app/core/services/point.service';
import { DataUtil } from '@vowel-bonus-app/core/utils/data.util';
import { Subject } from 'rxjs';
import { FilterDialogComponent } from '../filter-dialog/filter-dialog.component';

@Component({
  selector: 'app-history-item',
  imports: [CommonModule, FormsModule, FilterDialogComponent],
  templateUrl: './history-item.component.html',
  styleUrl: './history-item.component.css',
})
export class HistoryItemComponent {
  @ViewChild(FilterDialogComponent) filterDialog!: FilterDialogComponent;

  destroy$ = new Subject<void>();
  historyItem?: VowelBonusScoreHistory[];

  constructor(private pointService: PointService, private dataUtil: DataUtil) {}

  currentPage = 1;
  pageSize = 5;
  pageSizes = [5, 10, 20, 50];
  pagedItems: VowelBonusScoreHistory[] = [];
  totalPages = 0;
  totalItems = 0;
  visiblePages: number[] = [1, 2, 3, 4, 5];
  pagerStart = 1;
  pagerEnd = 1;
  maxVisible = 5;
  isLoading = false;
  openFilter = false;

  filters = {
    startWord: null,
    endWord: null,
    minPoint: null,
    maxPoint: null,
    startDate: null,
    endDate: null,
    sortBy: 'createdDate',
    sortDirection: 'desc',
  };

  ngOnInit(): void {
    this.updatePagedItems();
  }

  ngOnChanges(): void {
    this.updatePagedItems();
  }

  async updatePagedItems() {
    //this.isLoading = true; <---- ยังไม่ได้ทำ Loading

    this.pointService
      .getVowelBonusScoreHistoriesByFilter(
        this.pageSize,
        this.currentPage,
        this.filters.startWord,
        this.filters.endWord,
        this.filters.minPoint,
        this.filters.maxPoint,
        this.filters.sortBy,
        this.filters.sortDirection
      )
      .subscribe({
        next: (res) => {
          if (res.succeeded && res.result) {
            this.totalItems = res.totalRecord;
            this.totalPages = Math.ceil(this.totalItems / this.pageSize);
            this.historyItem = res.result;
            this.isLoading = false;
          }
        },
        error: (error) => {
          this.isLoading = false;
          console.log(error);
        },
      });
  }

  onPageChange(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.updatePagedItems();

    const minPage = Math.min(...this.visiblePages);
    const maxPage = Math.max(...this.visiblePages);

    if (this.currentPage == this.totalPages) {
      this.pagerStart = this.totalPages;
      this.shiftPageGroup(0);
    } else if (this.currentPage == 1) {
      this.pagerStart = 1;
      this.shiftPageGroup(0);
    } else if (this.currentPage < minPage) {
      this.shiftPageGroup(-1);
    } else if (this.currentPage > maxPage) {
      this.shiftPageGroup(1);
    }
  }

  updateVisiblePages(): void {
    const pages: number[] = [];
    const maxVisible = 5;
    let start = Math.max(1, this.currentPage - Math.floor(maxVisible / 2));
    let end = Math.min(this.totalPages, start + maxVisible - 1);

    if (end - start < maxVisible - 1) {
      start = Math.max(1, end - maxVisible + 1);
    }

    for (let i = start; i <= end; i++) {
      pages.push(i);
    }

    this.visiblePages = pages;
  }

  shiftPageGroup(direction: number): void {
    const newStart = this.pagerStart + direction * this.maxVisible;
    if (newStart < 1 || newStart > this.totalPages) return;

    this.pagerStart = Math.max(1, newStart);
    this.pagerEnd = Math.min(
      this.totalPages,
      this.pagerStart + this.maxVisible - 1
    );
    this.visiblePages = [];

    for (let i = this.pagerStart; i <= this.pagerEnd; i++) {
      this.visiblePages.push(i);
    }
  }

  applyFilters() {
    this.currentPage = 1;
    this.pointService
      .getVowelBonusScoreHistoriesByFilter(
        this.pageSize,
        this.currentPage,
        this.filters.startWord,
        this.filters.endWord,
        this.filters.minPoint,
        this.filters.maxPoint,
        this.filters.sortBy,
        this.filters.sortDirection
      )
      .subscribe({
        next: (res) => {
          if (res.succeeded && res.result) {
            this.totalItems = res.totalRecord;
            this.totalPages = Math.ceil(this.totalItems / this.pageSize);
            this.historyItem = res.result;
            this.isLoading = false;
          }
        },
        error: (error) => {
          this.isLoading = false;
          console.log(error);
        },
      });

    this.filterDialog.onCloseDialog();
  }

  resetFilters() {
    this.filters = {
      startWord: null,
      endWord: null,
      minPoint: null,
      maxPoint: null,
      startDate: null,
      endDate: null,
      sortBy: 'createdDate',
      sortDirection: 'desc',
    };
  }

  onEdit(item: VowelBonusScoreHistory): void {
    console.log(item)
  }

  onDelete(item: VowelBonusScoreHistory): void {
    if (confirm(`Are you sure to delete "${item.word}"?`)) {
    }
  }

  startEdit(item: VowelBonusScoreHistory): void {
    item.isEditing = true;
    item.editingWord = item.word;
  }

  saveEdit(item: VowelBonusScoreHistory): void {
      console.log(item)
    item.word = item.editingWord;
    item.isEditing = false;
  }

  cancelEdit(item: VowelBonusScoreHistory): void {
    item.isEditing = false;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
