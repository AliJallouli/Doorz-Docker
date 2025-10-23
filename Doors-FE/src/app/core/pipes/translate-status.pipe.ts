import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'translateStatus',
  standalone: true
})
export class TranslateStatusPipe implements PipeTransform {
  private readonly statusTranslations: { [key: string]: string } = {
    PENDING: 'En attente',
    APPROVED: 'Approuvée',
    REJECTED: 'Rejetée'
  };

  transform(value: string | undefined): string {
    if (!value) {
      return '-';
    }
    return this.statusTranslations[value.toUpperCase()] || value;
  }
}
