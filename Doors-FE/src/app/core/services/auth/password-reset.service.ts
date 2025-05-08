import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';

import {
  ApiResponse, ConfirmPasswordResetRequestDTO, ConfirmPasswordResetResponseDTO,
  PasswordResetRequestDTO,
  PasswordResetResponseDTO, ValidatePasswordResetTokenAndOTPRequestDTO,
  ValidatePasswordResetTokenRequestDTO, ValidatePasswordResetTokenResponseDTO
} from '../../../models/auth.models';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PasswordResetService {
  private readonly apiUrl = `${environment.apiUrl}/password`;
  constructor(private http: HttpClient) { }


  requestPasswordReset(payload: PasswordResetRequestDTO): Observable<ApiResponse<PasswordResetResponseDTO>> {
    return this.http.post<ApiResponse<PasswordResetResponseDTO>>(`${this.apiUrl}/request-password-reset`, payload);
  }
  validateResetPasswordToken(request: ValidatePasswordResetTokenRequestDTO): Observable<ApiResponse<ValidatePasswordResetTokenResponseDTO>> {
    return this.http.post<ApiResponse<ValidatePasswordResetTokenResponseDTO>>(`${this.apiUrl}/validate-password-reset-token`, request);
  }
  resetPassword(request: ConfirmPasswordResetRequestDTO): Observable<ApiResponse<ConfirmPasswordResetResponseDTO>> {
    return this.http.post<ApiResponse<ConfirmPasswordResetResponseDTO>>(`${this.apiUrl}/confirm-reset-password`, request);
  }
  validateResetPasswordOtp(request: ValidatePasswordResetTokenAndOTPRequestDTO): Observable<ApiResponse<ValidatePasswordResetTokenResponseDTO>> {
    return this.http.post<ApiResponse<ValidatePasswordResetTokenResponseDTO>>(`${this.apiUrl}/validate-password-reset-otp`, request);
  }
}
