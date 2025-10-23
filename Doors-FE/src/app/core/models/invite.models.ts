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

export interface CreateInvitationRequestDto {
  entityTypeName: string;
  name: string;
  invitationEmail: string;
  companyNumber?: string;
  institutionTypeId?: number;
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


export interface InvitationRequestDto {
  invitationRequestId: number;
  entityTypeId: number;
  entityTypeName: string;
  name: string;
  invitationEmail: string;
  companyNumber?: string;
  institutionTypeId?: number;
  institutionTypeName?: string;
  status: string;
  rejectionReason?: string;
  submittedIp?: string;
  userAgent?: string;
  createdAt: string;
  updatedAt: string;
}

export interface EntityTypeDto {
  entityTypeId: number;
  name: string;
  nameTranslation: string;
}

export interface PagedResult<T> {
  data: T[];
  page: number;
  pageSize: number;
  total: number;
}
