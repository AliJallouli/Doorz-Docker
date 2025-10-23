

import {LegalConsentDTO} from './legal-document.models';

export interface LoginRequest {
  Email: string;
  Password: string;
  RememberMe: boolean;
}



export interface LoginResponse {
  accessToken: string;
  refreshToken: string;
}

export interface ApiResponse<T> {
  success?: {
    key: string;
    message: string;
  };
  data?: T;
  error?: {
    key: string;
    field?: string;
    message: string;
    extraData?: Record<string, any>;
  };
}

export interface LogoutRequest {
  RefreshToken?: string;
}

export interface RegisterPublicRequestDTO {
  firstName?: string;
  lastName?: string;
  email: string;
  password: string;
  roleId: number;
  legalConsents: LegalConsentDTO[];
}

export interface RoleIdResponseDTO {
  roleId: number;
}
export interface RoleIdRequestDTO {
  roleName: string;
  entityTypeName: string;
}
export interface PasswordResetRequestDTO {
  email: string;
}

export interface PasswordResetResponseDTO {
  key: string;
}

export interface ValidatePasswordResetTokenRequestDTO {
  token: string;
}

export interface ValidatePasswordResetTokenResponseDTO {
  key: string;
  email: string;
}

export type RoleName = 'student' | 'landlord' | 'company' | 'institution' | 'superadmin';
export interface ConfirmPasswordResetRequestDTO {
  email: string;
  token: string;
  newPassword: string;
}

export interface ConfirmPasswordResetResponseDTO {
  key: string;
}

export interface ValidatePasswordResetTokenAndOTPRequestDTO {
  Token: string;
  Otp: string;
}

export interface UpdatePasswordRequestDTO {
  currentPassword: string;
  newPassword: string;
}

export interface UpdateEmailRequestDto {
  newEmail: string;
  currentPassword: string;
}
export interface UpdateEmailResponseDto {
  key: string;
}
