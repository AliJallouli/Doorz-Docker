import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class SearchMemoryService {
  private cache: Record<string, any[]> = {};
  private readonly storageKey = 'SEARCH_CACHE';

  constructor() {
    const stored = sessionStorage.getItem(this.storageKey);
    if (stored) {
      try {
        this.cache = JSON.parse(stored);
        console.log('♻️ Cache restauré depuis sessionStorage', this.cache);
      } catch (e) {
        console.warn('❌ Cache corrompu, ignore sessionStorage');
        this.cache = {};
      }
    }
  }

  setResults(type: string, results: any[]) {
    if (!results || results.length === 0) return;

    this.cache[type] = results;
    this.persist();
    console.log(`💾 Cache enregistré pour ${type} →`, results);
  }

  getResults(type: string): any[] {
    const results = this.cache[type] || [];
    console.log(`📦 getResults(${type}) →`, results);
    return results;
  }
  hasResults(type: string): boolean {
    return Array.isArray(this.cache[type]) && this.cache[type].length > 0;
  }


  clear(type: string): void {
    delete this.cache[type];
    this.persist();
  }

  clearAll(): void {
    this.cache = {};
    this.persist();
  }

  private persist() {
    sessionStorage.setItem(this.storageKey, JSON.stringify(this.cache));
  }
}
