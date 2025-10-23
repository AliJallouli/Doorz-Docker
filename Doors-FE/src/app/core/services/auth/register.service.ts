import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  ApiResponse,
  LoginResponse,
  RegisterPublicRequestDTO,
  RoleIdRequestDTO,
  RoleIdResponseDTO
} from '../../models/auth.models';
import {RegisterFromInviteRequestDTO} from '../../models/invite.models';
import { environment } from '../../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private readonly apiUrl = `${environment.apiUrl}/register`;
  private readonly apiUrlrole = `${environment.apiUrl}/auth`;

  constructor(private http: HttpClient) {}

  registerPublic(payload: RegisterPublicRequestDTO): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<LoginResponse>>(`${this.apiUrl}/register-public`, payload, { withCredentials: true });
  }
  getRoleId(request: RoleIdRequestDTO): Observable<ApiResponse<RoleIdResponseDTO>> {
    return this.http.post<ApiResponse<RoleIdResponseDTO>>(`${this.apiUrlrole}/get-role-id`, request);
  }
  registerAdminFromInvite(payload: RegisterFromInviteRequestDTO): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<any>>(`${this.apiUrl}/register-admin-from-invite`, payload, { withCredentials: true });
  }

  registerColleagueFromInvite(payload: RegisterFromInviteRequestDTO): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<any>>(`${this.apiUrl}/register-colleague-from-invite`, payload, { withCredentials: true });
  }



}
