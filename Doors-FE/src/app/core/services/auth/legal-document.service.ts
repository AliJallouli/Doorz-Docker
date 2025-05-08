import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import {ApiResponse} from '../../../models/auth.models';
import {LegalDocumentDTO, LegalDocumentTypeDTO} from '../../../models/legal-document.models';

@Injectable({
  providedIn: 'root'
})
export class LegalDocumentService {
  private readonly apiUrl = `${environment.apiUrl}/legals`;

  constructor(private http: HttpClient) {}

  /**
   * Récupère le document actif par type
   */
  getActiveDocument(documentTypeName: string): Observable<ApiResponse<LegalDocumentDTO>> {
    return this.http.get<ApiResponse<LegalDocumentDTO>>(`${this.apiUrl}/document/${documentTypeName}`);
  }

  /**
   * Récupère la liste des types de documents légaux traduits
   */
  getAllDocumentTypes(): Observable<ApiResponse<LegalDocumentTypeDTO[]>> {
    return this.http.get<ApiResponse<LegalDocumentTypeDTO[]>>(`${this.apiUrl}/document-types`);
  }
}
