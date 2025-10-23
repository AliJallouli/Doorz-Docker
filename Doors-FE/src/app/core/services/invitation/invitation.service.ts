// src/app/core/services/entity.service.ts
import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {ApiResponse} from '../../models/auth.models';
import {
  CreateInvitationRequestDto, EntityTypeDto, InvitationRequestDto, PagedResult,
  ValidateInvitationTokenRequestDTO,
  ValidateInvitationTokenResponseDTO
} from '../../models/invite.models';
import { environment } from '../../../../environments/environment';

export interface Role {
  roleId: number;
  name: string;
  entityTypeId: number;
}
export interface InstitutionType { // Ajout d'une interface pour les types d'institution
  institutionTypeId: number;
  name: string;
}

export interface EntityType {
  entityTypeId: number;
  name: string;
  nameTranslation: string;
}

@Injectable({
  providedIn: 'root'
})
export class InvitationService {
  private readonly apiUrl = environment.apiUrl;
  private readonly baseUrl = `${environment.apiUrl}/invitations`;
  private readonly adminInvitUrl = `${environment.apiUrl}/admin-invite`;

  constructor(private http: HttpClient) {}

  getEntityTypes(): Observable<ApiResponse<EntityTypeDto[]>> {
    return this.http.get<ApiResponse<EntityTypeDto[]>>(`${this.apiUrl}/entitytypes`).pipe(
      catchError(error => {
        console.error('Erreur lors du chargement des types d\'entités:', error);
        return throwError(() => new Error('Échec du chargement des types d\'entités'));
      })
    );
  }

  getInstitutionTypes(): Observable<InstitutionType[]> { // Typage ajusté
    return this.http.get<InstitutionType[]>(`${this.apiUrl}/institutiontypes`);
  }

  getRolesByEntityTypeName(entityTypeName: string): Observable<Role[]> {
    return this.http.get<Role[]>(`${this.apiUrl}/roles`, {
      params: { entityTypeName }
    });
  }
  inviteCompany(companyData: { Name: string; InvitationEmail: string; CompanyNumber: string }): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/admin-invite/company`, companyData);
  }

  inviteInstitution(institutionData: { Name: string; InstitutionTypeId: number; InvitationEmail: string }): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/admin-invite/institution`, institutionData);
  }

  inviteColleague(colleagueData: { Email: string; RoleId: number }): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/collegue-invite/colleague-invitation`, colleagueData);
  }


validateToken(payload: ValidateInvitationTokenRequestDTO): Observable<ApiResponse<ValidateInvitationTokenResponseDTO>> {
  return this.http.get<ApiResponse<ValidateInvitationTokenResponseDTO>>(
    `${this.baseUrl}/validate`,
    { params: { invitationToken: payload.invitationToken } } // Envoie comme query param
  );
}

  submitInvitationRequest(playload:CreateInvitationRequestDto): Observable<ApiResponse<string>> {
    return this.http.post<any>(`${this.baseUrl}/request`, playload);
  }


  getInvitationRequests(
    page: number = 1,
    pageSize: number = 4,
    status?: string,
    entityTypeName?: string
  ): Observable<ApiResponse<PagedResult<InvitationRequestDto>>> {
    let url = `${this.adminInvitUrl}/invitation-requests?page=${page}&pageSize=${pageSize}`;
    if (status) url += `&status=${encodeURIComponent(status)}`;
    if (entityTypeName) url += `&entityTypeName=${encodeURIComponent(entityTypeName)}`;
    return this.http.get<ApiResponse<PagedResult<InvitationRequestDto>>>(url, { withCredentials: true }).pipe(
      catchError(error => {
        console.error('Erreur API:', error);
        return throwError(() => new Error('Échec du chargement des demandes d\'invitation'));
      })
    );
  }

  processInvitationRequest(request: {
    invitationRequestId: number;
    action: 'APPROVED' | 'REJECTED';
    rejectionReason?: string;
  }): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<any>>(`${this.adminInvitUrl}/from-request`, request).pipe(
      catchError(error => {
        console.error('Erreur lors du traitement de la demande:', error);
        return throwError(() => new Error('Échec du traitement de la demande'));
      })
    );
  }


}
