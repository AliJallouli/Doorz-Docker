export interface CreateContactMessageDTO {
  fullName: string;
  email: string;
  subject: string;
  message: string;
  languageId: number;
  contactMessageTypeId: number;
  phone?: string;
}

export interface ContactMessageTypeDTO {
  contactMessageTypeId: number;
  keyName: string;
  name: string;
  description?: string;
  priority: number;
  isActive: boolean;
}
