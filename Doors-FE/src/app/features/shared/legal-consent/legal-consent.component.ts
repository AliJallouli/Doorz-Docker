import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { LegalDocumentService } from '../../../core/services/auth/legal-document.service';
import { LegalDocumentDTO, LegalDocumentTypeDTO } from '../../../core/models/legal-document.models';
import { FloatingWindowService } from '../../../core/services/legal/floating-window.service';
import { forkJoin } from 'rxjs';
import { map } from 'rxjs/operators';

interface DocumentType {
  documentId: number;
  name: string;
}

@Component({
  selector: 'app-legal-consent',
  standalone: true,
  imports: [NgForOf, FormsModule, TranslateModule, NgClass, NgIf],
  templateUrl: './legal-consent.component.html',
  styleUrls: ['./legal-consent.component.css'],
})
export class LegalConsentComponent implements OnInit {
  @Input() source: string = '';
  documentTypes: LegalDocumentTypeDTO[] = [];
  selectedConsents: { [key: string]: boolean } = {};
  requiredDocumentTypes: string[] = [];
  selectedDocumentContent: string | null = null;
  hasInteracted: boolean = false;

  @Output() consentsChanged = new EventEmitter<{ [key: string]: boolean }>();
  @Output() documentTypesLoaded = new EventEmitter<{ [key: string]: DocumentType }>();

  modalContent: SafeHtml;
  private validationTriggered: boolean = false;

  constructor(
    private legalService: LegalDocumentService,
    private sanitizer: DomSanitizer,
    private floatingWindowService: FloatingWindowService,
    private translate: TranslateService
  ) {
    this.modalContent = this.sanitizer.bypassSecurityTrustHtml('');
  }

  ngOnInit(): void {
    this.loadDocumentTypes();
    this.translate.onLangChange.subscribe(() => {
      this.loadDocumentTypes(true);
    });
  }

  private loadDocumentTypes(preserveConsents: boolean = false): void {
    this.legalService.getAllDocumentTypes().subscribe({
      next: (res) => {
        if (res.success && res.data) {
          this.documentTypes = res.data;
          this.requiredDocumentTypes = this.source === 'contact-form'
            ? ['TermsOfService']
            : this.source === 'register-form'
              ? this.documentTypes.map(doc => doc.name)
              : [];

          const documentTypesMap: { [key: string]: DocumentType } = {};
          const newConsents: { [key: string]: boolean } = preserveConsents
            ? { ...this.selectedConsents }
            : {};

          const requests = this.documentTypes.map((doc) =>
            this.legalService.getActiveDocument(doc.name).pipe(
              map((response) => {
                const docName = doc.label || doc.name;
                return {
                  name: docName,
                  documentId: response.data?.documentId ?? 0,
                };
              })
            )
          );

          forkJoin(requests).subscribe((results) => {
            results.forEach(({ name, documentId }) => {
              const key = this.documentTypes.find(doc => doc.name === name || doc.label === name)?.name || name;
              if (!(key in newConsents)) {
                newConsents[key] = false;
              }
              documentTypesMap[key] = { documentId, name };
            });

            this.selectedConsents = newConsents;
            this.documentTypesLoaded.emit(documentTypesMap);
            this.consentsChanged.emit(this.selectedConsents);
          });
        } else {
          this.documentTypes = [];
          this.selectedConsents = {};
          this.documentTypesLoaded.emit({});
        }
      },
      error: () => {
        this.documentTypes = [];
        this.selectedConsents = {};
        this.documentTypesLoaded.emit({});
      },
    });
  }

  onCheckboxChange(documentTypeName: string): void {
    this.hasInteracted = true;
    this.consentsChanged.emit({ ...this.selectedConsents });
    const isChecked = this.selectedConsents[documentTypeName];

    if (isChecked) {
      this.legalService.getActiveDocument(documentTypeName).subscribe({
        next: (response) => {
          const clauses = response.data?.clauses ?? [];
          this.selectedDocumentContent = clauses
            .sort((a, b) => a.orderIndex - b.orderIndex)
            .map((c) => `🟢 ${c.title}\n${c.content}`)
            .join('\n\n');
        },
        error: () => {
          this.selectedDocumentContent = null;
        },
      });
    } else {
      this.selectedDocumentContent = null;
    }
  }

  isInvalid(documentTypeName: string): boolean {
    if (!this.validationTriggered) return false;
    return this.requiredDocumentTypes.includes(documentTypeName) && this.selectedConsents[documentTypeName] !== true;
  }

  triggerValidation(): void {
    this.hasInteracted = true;
    this.validationTriggered = true;
    this.consentsChanged.emit({ ...this.selectedConsents });
  }

  openFloatingWindow(docName: string): void {
    this.legalService.getActiveDocument(docName).subscribe({
      next: (res) => {
        if (!res.data) return;
        const document: LegalDocumentDTO = res.data;
        this.floatingWindowService.openWindow(document, docName);
      },
      error: () => {
        this.floatingWindowService.openWindow(
          {
            documentId: 0,
            version: '0',
            publishedAt: new Date().toISOString(),
            isActive: false,
            documentTypeName: 'error',
            documentTypeLabel: this.translate.instant('LEGAL.ERROR'),
            languageCode: this.translate.currentLang,
            clauses: [
              {
                title: this.translate.instant('LEGAL.ERROR'),
                content: this.translate.instant('LEGAL.ERROR_MESSAGE'),
                orderIndex: 0,
              },
            ],
          },
          'error'
        );
      },
    });
  }

  reset(): void {
    this.selectedConsents = {};
    this.hasInteracted = false;
    this.validationTriggered = false;
    this.selectedDocumentContent = null;
    this.documentTypes = [];
    this.requiredDocumentTypes = [];
    this.loadDocumentTypes();
  }
}
