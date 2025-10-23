export interface User {
  userId: number;
  email: string;
  firstName?: string;
  lastName?: string;
  superRole: string;
  role?: string;
  entityName?: string;
  entityType?: string;
  isVerified: boolean;
}

export interface UpdateNameRequestDto {
  currentPassword: string;
  newFirstName: string;
  newLastName: string;
}

export interface UpdateNameResponseDto {
  key: string;
}
