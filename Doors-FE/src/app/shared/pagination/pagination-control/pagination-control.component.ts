import {Component, EventEmitter, Input, Output} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {NgForOf} from '@angular/common';

@Component({
  selector: 'app-pagination-control',
  imports: [
    FormsModule,
    NgForOf
  ],
  templateUrl: './pagination-control.component.html',
  styleUrl: './pagination-control.component.css'
})
export class PaginationControlComponent {
  @Input() pageSize: number = 10;
  @Input() pageSizeOptions: number[] = [1, 10, 20, 50];
  @Input() totalItems: number = 0;
  @Input() currentPage: number = 1;
  @Output() pageSizeChange = new EventEmitter<number>();

  onPageSizeChange(): void {
    this.pageSizeChange.emit(this.pageSize);
  }

  getResultRange(): string {
    if (this.totalItems === 0) {
      return 'Aucun résultat';
    }
    const start = (this.currentPage - 1) * this.pageSize + 1;
    const end = Math.min(this.currentPage * this.pageSize, this.totalItems);
    return `Affichage de ${start}-${end} sur ${this.totalItems} résultats`;
  }
}
