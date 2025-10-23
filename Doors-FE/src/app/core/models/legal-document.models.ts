export interface LegalDocumentTypeDTO {
  documentTypeId: number;
  name: string;
  label: string;
  description?: string;
  languageCode: string;
}
export interface LegalClauseDTO {
  title: string;
  content: string;
  orderIndex: number;
}

export interface LegalDocumentDTO {
  documentId: number;
  version: string;
  publishedAt: string; // ISO string
  isActive: boolean;
  documentTypeName: string;
  documentTypeLabel: string;
  languageCode: string;
  clauses: LegalClauseDTO[];
}

export interface LegalConsentDTO {
  documentId: number;
}
