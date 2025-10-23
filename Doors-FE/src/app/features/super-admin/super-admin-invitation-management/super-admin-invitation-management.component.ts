import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DatePipe, NgClass, NgForOf, NgIf } from '@angular/common';
import { InvitationRequestDto } from '../../../core/models/invite.models';
import {EntityType, InvitationService} from '../../../core/services/invitation/invitation.service';
import { TranslateStatusPipe } from '../../../core/pipes/translate-status.pipe';
import {PaginationControlComponent} from '../../../shared/pagination/pagination-control/pagination-control.component';
import {
  PaginationNavigatorComponent
} from '../../../shared/pagination/pagination-navigator/pagination-navigator.component';

@Component({
  selector: 'app-super-admin-invitation-management',
  standalone: true,
  imports: [FormsModule, NgIf, NgForOf, NgClass, DatePipe, TranslateStatusPipe, PaginationControlComponent, PaginationNavigatorComponent],
  templateUrl: './super-admin-invitation-management.component.html',
  styleUrls: ['./super-admin-invitation-management.component.css']
})
export class SuperAdminInvitationManagementComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'name',
    'email',
    'entityTypeName',
    'institutionTypeName',
    'companyNumber',
    'status',
    'createdAt',
    'actions'
  ];
  dataSource: InvitationRequestDto[] = [];
  statusFilter: string = 'PENDING';
  entityTypeFilter: string = '';
  entityTypes: EntityType[] | undefined= [];
  isLoading: boolean = false;
  showRefusalModal: boolean = false;
  currentRequestId: number | null = null;
  rejectionReason: string = '';

  // Pagination
  currentPage: number = 1;
  pageSize: number = 10;
  totalPages: number = 1;
  totalItems: number = 0;
  pageSizeOptions: number[] = [1, 10, 20, 50];

  constructor(private invitationRequestService: InvitationService) {}

  ngOnInit(): void {
    this.loadEntityTypes();
    this.loadRequests();
  }

  loadEntityTypes(): void {
    this.invitationRequestService.getEntityTypes().subscribe({
      next: (response) => {
        console.log('Types d\'entités reçus :', response.data);
        this.entityTypes = response.data ?? [];
      },
      error: (error) => {
        console.error('Erreur lors du chargement des types d\'entités :', error);
        console.error('Détails de l\'erreur :', error.status, error.error);
      }
    });
  }

  loadRequests(): void {
    this.isLoading = true;
    this.invitationRequestService
      .getInvitationRequests(this.currentPage, this.pageSize, this.statusFilter, this.entityTypeFilter)
      .subscribe({
        next: (response) => {
          console.log('Demandes reçues :', response.data);
          this.dataSource = response.data?.data ?? [];
          this.totalPages = Math.ceil((response.data?.total ?? 0) / this.pageSize);
          this.totalItems = response.data?.total ?? 0;
          this.isLoading = false;
        },
        error: (error) => {
          console.error('Erreur lors du chargement des demandes :', error);
          console.error('Détails de l\'erreur :', error.status, error.error);
          alert('Échec du chargement des demandes');
          this.dataSource = [];
          this.totalPages = 1;
          this.totalItems = 0;
          this.isLoading = false;
        }
      });
  }

  applyFilter(): void {
    this.currentPage = 1;
    this.loadRequests();
  }

  onPageSizeChange(newSize: number): void {
    this.pageSize = newSize;
    this.currentPage = 1;
    this.loadRequests();
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadRequests();
    }
  }

  previousPage(): void {
    this.goToPage(this.currentPage - 1);
  }

  nextPage(): void {
    this.goToPage(this.currentPage + 1);
  }

  acceptRequest(id: number): void {
    this.processRequest(id, 'APPROVED');
  }

  refuseRequest(id: number): void {
    this.currentRequestId = id;
    this.showRefusalModal = true;
  }

  submitRefusal(): void {
    if (this.currentRequestId && this.rejectionReason) {
      this.processRequest(this.currentRequestId, 'REJECTED', this.rejectionReason);
      this.closeRefusalModal();
    }
  }

  closeRefusalModal(): void {
    this.showRefusalModal = false;
    this.rejectionReason = '';
    this.currentRequestId = null;
  }

  private processRequest(id: number, action: 'APPROVED' | 'REJECTED', rejectionReason?: string): void {
    this.isLoading = true;
    this.invitationRequestService
      .processInvitationRequest({ invitationRequestId: id, action, rejectionReason })
      .subscribe({
        next: () => {
          this.isLoading = false;
          alert(`Demande ${action === 'APPROVED' ? 'approuvée' : 'rejetée'} avec succès`);
          this.loadRequests();
        },
        error: (error) => {
          this.isLoading = false;
          alert(`Échec de ${action === 'APPROVED' ? "l'approbation" : 'le rejet'} de la demande`);
          console.error('Erreur lors du traitement de la demande :', error);
        }
      });
  }
}
