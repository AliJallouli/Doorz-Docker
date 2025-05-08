// src/app/core/services/entity.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map, Observable} from 'rxjs';
import {ApiResponse} from '../../../models/auth.models';
import {
  RegisterFromInviteRequestDTO,
  ValidateInvitationTokenRequestDTO,
  ValidateInvitationTokenResponseDTO
} from '../../../models/invite.models';
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

  constructor(private http: HttpClient) {}

  getEntityTypes(): Observable<EntityType[]> {
    return this.http.get<EntityType[]>(`${this.apiUrl}/entitytypes`);
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




}
