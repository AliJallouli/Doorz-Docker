import { Injectable, OnDestroy } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { LegalDocumentDTO } from '../../models/legal-document.models';
import {LegalDocumentService} from '../auth/legal-document.service';

@Injectable({
  providedIn: 'root',
})
export class FloatingWindowService implements OnDestroy {
  private windowElement: HTMLElement | null = null;
  private contentBody: HTMLElement | null = null;
  private scrollButton: HTMLElement | null = null;
  private scrollTimeout: number | null = null;
  private overlayElement: HTMLElement | null = null;
  private currentDocName: string | null = null; // Stocker le nom du document
  private readonly langChangeSubscription: Subscription | null = null; // Abonnement au changement de langue

  constructor(
    private translate: TranslateService,
    private legalService: LegalDocumentService // Pour recharger le document
  ) {
    // Écouter les changements de langue
    this.langChangeSubscription = this.translate.onLangChange.subscribe(() => {
      console.log('Langue changée:', this.translate.currentLang);
      if (this.currentDocName && this.windowElement) {
        this.reloadDocument(this.currentDocName);
      }
    });
  }

  openWindow(legalDocument: LegalDocumentDTO, docName: string) {
    this.closeWindow();
    this.currentDocName = docName; // Stocker le nom du document
    this.renderWindow(legalDocument);
  }

  private reloadDocument(docName: string) {
    this.legalService.getActiveDocument(docName).subscribe({
      next: (res) => {
        if (!res.data) {
          console.error('Document non trouvé pour:', docName);
          return;
        }
        console.log('Document rechargé:', res.data);
        this.renderWindow(res.data);
      },
      error: (err) => {
        console.error('Erreur lors du rechargement du document:', err);
        this.renderErrorWindow();
      },
    });
  }

  private renderWindow(legalDocument: LegalDocumentDTO) {
    // Sauvegarder la position de défilement actuelle (si existe)
    let scrollPosition = 0;
    if (this.contentBody) {
      scrollPosition = this.contentBody.scrollTop;
    }

    // Fermer la fenêtre existante sans supprimer overlay
    if (this.windowElement) {
      this.windowElement.remove();
      this.windowElement = null;
      this.contentBody = null;
      this.scrollButton = null;
    }

    // Extraire le titre, les clauses, la version et la date de publication
    const title = legalDocument.documentTypeLabel || this.translate.instant('LEGAL.DEFAULT_TITLE');
    const clauses = legalDocument.clauses || [];
    const version = legalDocument.version || 'N/A';
    const locale = this.translate.currentLang === 'en' ? 'en-US' : 'fr-FR'; // Ajuster selon tes langues
    const publishedAt = legalDocument.publishedAt
      ? new Date(legalDocument.publishedAt).toLocaleDateString(locale, {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
      })
      : 'N/A';

    // Créer l'overlay si non existant
    if (!this.overlayElement) {
      const overlayEl = document.createElement('div');
      overlayEl.className = 'floating-window-overlay';
      Object.assign(overlayEl.style, {
        position: 'fixed',
        top: '0',
        left: '0',
        width: '100%',
        height: '100%',
        backgroundColor: 'rgba(0, 0, 0, 0.5)',
        zIndex: '999',
      });
      // Fermer la fenêtre en cliquant sur l'overlay
      overlayEl.onclick = () => this.closeWindow();
      document.body.appendChild(overlayEl);
      this.overlayElement = overlayEl;
    }

    // Créer la fenêtre flottante
    const windowEl = document.createElement('div');
    windowEl.className = 'floating-window';
    Object.assign(windowEl.style, {
      position: 'fixed',
      top: '50%',
      left: '50%',
      transform: 'translate(-50%, -50%)',
      zIndex: '1000',
      backgroundColor: '#ffffff',
      borderRadius: '12px',
      boxShadow: '0 4px 16px rgba(0, 0, 0, 0.1)',
      maxWidth: '600px',
      width: '90%',
      maxHeight: '80vh',
      overflow: 'hidden',
      animation: 'floatIn 0.3s ease-out',
    });

    // Créer le conteneur de contenu
    const contentEl = document.createElement('div');
    contentEl.className = 'floating-window-content';
    Object.assign(contentEl.style, {
      padding: '1.5rem',
      color: '#333',
      boxSizing: 'border-box',
      display: 'flex',
      flexDirection: 'column',
      maxHeight: '80vh',
    });

    // Créer le titre principal
    const titleEl = document.createElement('h3');
    titleEl.textContent = title;
    Object.assign(titleEl.style, {
      textAlign: 'center',
      marginBottom: '0.5rem',
      fontWeight: '700',
      color: '#264653',
      fontSize: 'clamp(1rem, 3.5vw, 1.25rem)',
      lineHeight: '1.4',
    });

    // Créer l'élément pour version et date de publication
    const metaEl = document.createElement('p');
    metaEl.textContent = `${this.translate.instant('LEGAL.VERSION')} ${version} | ${this.translate.instant('LEGAL.PUBLISHED_ON')} ${publishedAt}`;
    Object.assign(metaEl.style, {
      textAlign: 'center',
      marginBottom: '1rem',
      fontWeight: '400',
      color: '#666',
      fontSize: 'clamp(0.75rem, 2.5vw, 0.875rem)',
      lineHeight: '1.4',
    });

    // Créer le corps défilant
    const bodyEl = document.createElement('div');
    bodyEl.className = 'content-body';
    this.contentBody = bodyEl;
    Object.assign(bodyEl.style, {
      fontSize: 'clamp(0.875rem, 3.5vw, 0.9375rem)',
      lineHeight: '1.6',
      color: '#333',
      maxHeight: 'calc(80vh - 4rem)',
      overflowY: 'auto',
      paddingRight: '0.5rem',
      flexGrow: '1',
      position: 'relative',
    });

    // Trier les clauses par orderIndex
    const sortedClauses = clauses.sort((a, b) => a.orderIndex - b.orderIndex);

    // Ajouter les clauses
    sortedClauses.forEach((clause) => {
      const clauseTitle = document.createElement('h5');
      clauseTitle.textContent = `${clause.orderIndex}. ${clause.title}`;
      Object.assign(clauseTitle.style, {
        marginTop: '1rem',
        marginBottom: '0.5rem',
        fontWeight: '600',
        color: '#264653',
        fontSize: 'clamp(0.875rem, 3.5vw, 0.9375rem)',
      });

      const clauseContent = document.createElement('p');
      clauseContent.textContent = clause.content;
      Object.assign(clauseContent.style, {
        marginBottom: '1rem',
        fontSize: 'clamp(0.875rem, 3.5vw, 0.9375rem)',
      });

      bodyEl.appendChild(clauseTitle);
      bodyEl.appendChild(clauseContent);
    });

    // Créer le bouton de défilement
    const scrollButton = document.createElement('button');
    scrollButton.className = 'btn btn-scroll';
    scrollButton.innerHTML = '▼';
    Object.assign(scrollButton.style, {
      position: 'sticky',
      bottom: '0',
      padding: '0.5rem 1rem',
      height: '32px',
      fontSize: 'clamp(0.75rem, 3vw, 0.875rem)',
      lineHeight: '1.2',
      fontWeight: '600',
      borderRadius: '6px',
      cursor: 'pointer',
      transition: 'opacity 0.2s ease, transform 0.2s ease, background-color 0.2s ease',
      width: '100%',
      backgroundColor: '#2a9d8f',
      color: 'white',
      border: 'none',
      margin: '0.5rem 0',
      zIndex: '10',
      boxSizing: 'border-box',
      opacity: '1',
      transform: 'scale(1)',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
    });
    scrollButton.onmouseover = () => {
      scrollButton.style.backgroundColor = '#21867a';
      scrollButton.style.transform = 'scale(1) translateY(-1px)';
    };
    scrollButton.onmouseout = () => {
      scrollButton.style.backgroundColor = '#2a9d8f';
      scrollButton.style.transform = 'scale(1)';
    };
    scrollButton.onclick = () => {
      if (this.contentBody) {
        this.contentBody.scrollBy({ top: 100, behavior: 'smooth' });
      }
    };
    this.scrollButton = scrollButton;
    bodyEl.appendChild(scrollButton);

    // Créer le bouton de fermeture
    const closeButton = document.createElement('button');
    closeButton.className = 'btn';
    closeButton.textContent = this.translate.instant('LEGAL.CLOSE');
    Object.assign(closeButton.style, {
      padding: '0.5rem 1rem',
      height: '32px',
      fontSize: 'clamp(0.75rem, 3vw, 0.875rem)',
      lineHeight: '1.2',
      fontWeight: '600',
      borderRadius: '6px',
      cursor: 'pointer',
      transition: 'background-color 0.2s ease, transform 0.2s ease',
      width: '100%',
      backgroundColor: '#2a9d8f',
      color: 'white',
      border: 'none',
      margin: '0.5rem 0 1rem',
      zIndex: '10',
      boxSizing: 'border-box',
      opacity: '1',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
    });
    closeButton.onmouseover = () => {
      closeButton.style.backgroundColor = '#21867a';
      closeButton.style.transform = 'translateY(-1px)';
    };
    closeButton.onmouseout = () => {
      closeButton.style.backgroundColor = '#2a9d8f';
      closeButton.style.transform = 'none';
    };
    closeButton.onclick = () => this.closeWindow();
    bodyEl.appendChild(closeButton);

    // Gérer la visibilité du bouton de défilement
    const updateScrollButtonVisibility = () => {
      if (this.contentBody && this.scrollButton) {
        const isScrollable = this.contentBody.scrollHeight > this.contentBody.clientHeight;
        const isAtBottom =
          this.contentBody.scrollTop + this.contentBody.clientHeight >= this.contentBody.scrollHeight - 5;
        if (isScrollable && !isAtBottom) {
          this.scrollButton.style.opacity = '1';
          this.scrollButton.style.transform = 'scale(1)';
        } else {
          this.scrollButton.style.opacity = '0';
          this.scrollButton.style.transform = 'scale(0.95)';
        }
      }
    };

    const debouncedUpdateVisibility = () => {
      if (this.scrollTimeout) {
        clearTimeout(this.scrollTimeout);
      }
      this.scrollTimeout = window.setTimeout(updateScrollButtonVisibility, 50);
    };

    setTimeout(updateScrollButtonVisibility, 100);
    bodyEl.onscroll = debouncedUpdateVisibility;

    // Ajouter les éléments à la fenêtre
    contentEl.appendChild(titleEl);
    contentEl.appendChild(metaEl);
    contentEl.appendChild(bodyEl);
    windowEl.appendChild(contentEl);
    document.body.appendChild(windowEl);
    this.windowElement = windowEl;

    // Restaurer la position de défilement
    if (this.contentBody) {
      this.contentBody.scrollTop = scrollPosition;
    }

    // Injecter les styles responsives
    this.injectStyles();
  }

  private renderErrorWindow() {
    const errorDoc: LegalDocumentDTO = {
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
    };
    this.renderWindow(errorDoc);
  }

  closeWindow() {
    if (this.windowElement) {
      this.windowElement.remove();
      this.windowElement = null;
    }
    if (this.overlayElement) {
      this.overlayElement.remove();
      this.overlayElement = null;
    }
    this.contentBody = null;
    this.scrollButton = null;
    this.currentDocName = null;
    console.log('Fenêtre fermée');
  }

  ngOnDestroy(): void {
    if (this.langChangeSubscription) {
      this.langChangeSubscription.unsubscribe();
    }
    this.closeWindow();
  }

  private injectStyles() {
    const styleEl = document.createElement('style');
    styleEl.textContent = `
      @keyframes floatIn {
        from { opacity: 0; transform: translate(-50%, -45%); }
        to { opacity: 1; transform: translate(-50%, -50%); }
      }

      .floating-window::-webkit-scrollbar {
        display: none;
      }
      .floating-window {
        -ms-overflow-style: none;
        scrollbar-width: none;
      }

      .content-body::-webkit-scrollbar {
        display: none;
      }
      .content-body {
        -ms-overflow-style: none;
        scrollbar-width: none;
      }

      @media (max-width: 768px) {
        .floating-window {
          width: 95% !important;
          max-width: 500px !important;
          box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1) !important;
        }
        .floating-window-content {
          padding: 1.25rem !important;
        }
        .content-body {
          max-height: calc(50vh - 4rem) !important;
        }
        h3 {
          marginBottom: 0.5rem !important;
          lineHeight: 1.3 !important;
        }
        .btn-close, .btn-scroll {
          padding: 0.4rem 1rem !important;
          height: 28px !important;
        }
      }

      @media (max-width: 576px) {
        .floating-window {
          width: 95% !important;
          max-width: 400px !important;
        }
        .floating-window-content {
          padding: 1rem !important;
        }
        .content-body {
          max-height: calc(45vh - 3.5rem) !important;
          padding-right: 0.25rem !important;
        }
        h3 {
          fontSize: clamp(0.875rem, 3.5vw, 1rem) !important;
        }
        h5 {
          fontSize: clamp(0.875rem, 3.5vw, 0.9375rem) !important;
          fontWeight: 600 !important;
        }
        .btn-close, .btn-scroll {
          padding: 0.3rem 0.75rem !important;
          fontSize: clamp(0.75rem, 3vw, 0.875rem) !important;
          lineHeight: 1.2 !important;
          height: 24px !important;
          margin: 0.5rem 0 !important;
        }
      }

      @media (min-width: 992px) {
        .floating-window {
          padding: 2rem !important;
        }
        .floating-window-content {
          padding: 2rem !important;
        }
      }
    `;
    document.head.appendChild(styleEl);
  }
}
