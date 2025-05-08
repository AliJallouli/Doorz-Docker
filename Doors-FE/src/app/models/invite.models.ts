import {LegalConsentDTO} from './legal-document.models';

export interface ValidateInvitationTokenRequestDTO {
  invitationToken: string;
}
export interface ValidateInvitationTokenResponseDTO {
  invitationToken: string;
  email: string;
  roleId: number;
  entityId: number;
  entityTypeId: number;
  invitationType:string;
}

export interface RegisterFromInviteRequestDTO {
  firstName: string;
  lastName: string;
  invitationToken: string;
  email: string;
  roleId: number;
  entityId: number;
  entityTypeId: number;
  password: string;
  legalConsents: LegalConsentDTO[];
}

export interface ConfirmEmailRequestDTO {
  codeOtp: string;
  token: string;
}
export interface ConfirmEmailResponseDTO {
  key: string;
}

export interface ResendConfirmationRequestDTO {
  email: string;
}

