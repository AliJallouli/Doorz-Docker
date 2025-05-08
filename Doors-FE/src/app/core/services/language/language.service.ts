import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {ApiResponse} from '../../../models/auth.models';
import {environment} from '../../../../environments/environment';
import {TranslatedSpokenLanguageDTO} from '../../../models/language.models';

@Injectable({ providedIn: 'root' })
export class LanguageService {
  private readonly defaultLang = 'fr';
  private readonly storageKey = 'lang';
  private currentLangSubject = new BehaviorSubject<string>(this.getLanguage());
  private readonly apiUrl = `${environment.apiUrl}/languages`;
  currentLang$ = this.currentLangSubject.asObservable();

  constructor(private http: HttpClient) {}

  setLanguage(lang: string): void {
    localStorage.setItem(this.storageKey, lang);
    this.currentLangSubject.next(lang); // Notifie les abonnés du changement
  }

  getLanguage(): string {
    return localStorage.getItem(this.storageKey) || this.defaultLang;
  }

  getAvailableLanguages(): Observable<ApiResponse<TranslatedSpokenLanguageDTO[]>> {
    return this.http.get<ApiResponse<TranslatedSpokenLanguageDTO[]>>(
      `${this.apiUrl}/all`
    );
  }

}
