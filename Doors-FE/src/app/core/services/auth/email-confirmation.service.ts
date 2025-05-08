import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';

import {
  ConfirmEmailRequestDTO,
  ConfirmEmailResponseDTO,
  ResendConfirmationRequestDTO
} from '../../../models/invite.models';
import {Observable} from 'rxjs';
import {ApiResponse} from '../../../models/auth.models';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmailConfirmationService {
  private readonly apiUrl = `${environment.apiUrl}/emailconfirmation`;

  constructor(private http: HttpClient) { }
  confirmEmail(request: ConfirmEmailRequestDTO): Observable<ApiResponse<ConfirmEmailResponseDTO>> {
    return this.http.post<ApiResponse<ConfirmEmailResponseDTO>>(`${this.apiUrl}/confirm-email`, request);
  }
  validateToken(request: ConfirmEmailRequestDTO): Observable<ApiResponse<ConfirmEmailResponseDTO>> {
    return this.http.post<ApiResponse<ConfirmEmailResponseDTO>>(`${this.apiUrl}/validate-email-comfirmation-token`, request);
  }
  resendConfirmationEmail(request: ResendConfirmationRequestDTO): Observable<ApiResponse<void>> {
    return this.http.post<ApiResponse<void>>(`${this.apiUrl}/resend-email-confirmation`, request);
  }
  resendOtpCode(): Observable<ApiResponse<void>> {
    return this.http.post<ApiResponse<void>>(`${this.apiUrl}/resend-confirmation_email_otp`, {});
  }

  resendOtpCodeWithEmail(email: string): Observable<ApiResponse<void>> {
    return this.http.post<ApiResponse<void>>(`${this.apiUrl}/resend-otp-from-email`, { email });
  }


}
