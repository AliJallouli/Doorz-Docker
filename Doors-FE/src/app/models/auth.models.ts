// src/app/models/auth.models.ts

// Interface pour la requête de connexion envoyée à l'API
// src/app/models/auth.models.ts
import {LegalConsentDTO} from './legal-document.models';

export interface LoginRequest {
  Email: string;    // Changé de "username" à "email"
  Password: string;
}


// Interface pour la réponse de l'API après une connexion réussie
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
  key: string; // Utilisé pour la traduction du message de succès côté frontend
}

export interface ValidatePasswordResetTokenRequestDTO {
  token: string;
}

export interface ValidatePasswordResetTokenResponseDTO {
  key: string;
  email: string;
}

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
