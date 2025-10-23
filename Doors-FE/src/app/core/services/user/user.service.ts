import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { UpdateNameRequestDto, UpdateNameResponseDto, User } from '../../models/user.model';
import { UpdateEmailRequestDto, UpdateEmailResponseDto, UpdatePasswordRequestDTO, ApiResponse } from '../../models/auth.models';
import { environment } from '../../../../environments/environment';
import { LoggerService } from '../logger/logger.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly apiUserUrl = `${environment.apiUrl}/users`;
  private readonly apiEmailUserUrl = `${environment.apiUrl}/emailuser`;
  private readonly apiPasswordUrl = `${environment.apiUrl}/password`;
  private userSubject = new BehaviorSubject<User | null>(null);
  user$: Observable<User | null> = this.userSubject.asObservable();

  constructor(
    private http: HttpClient,
    private logger: LoggerService
  ) {}

  // Charger l'utilisateur initial
  loadUser(): void {
    this.getCurrentUser().subscribe({
      next: (response) => {
        this.userSubject.next(response.data ?? null);
        this.logger.log('Utilisateur chargé :', response.data);
      },
      error: (error) => {
        this.logger.error('Erreur lors du chargement de l’utilisateur', error);
        this.userSubject.next(null);
      }
    });
  }

  // Récupérer l'utilisateur depuis le backend
  getCurrentUser(): Observable<ApiResponse<User>> {
    return this.http.get<ApiResponse<User>>(`${this.apiUserUrl}/me`);
  }

  // Mettre à jour le nom
  updateName(request: UpdateNameRequestDto): Observable<ApiResponse<UpdateNameResponseDto>> {
    return this.http.put<ApiResponse<UpdateNameResponseDto>>(`${this.apiUserUrl}/update-name`, request).pipe(
      tap((response) => {
        const currentUser = this.userSubject.getValue();
        if (currentUser && response.data) {
          this.userSubject.next({
            ...currentUser,
            firstName: request.newFirstName,
            lastName:  request.newLastName
          });
          this.logger.log('État utilisateur mis à jour après changement de nom :', this.userSubject.getValue());
        }
      })
    );
  }

  // Mettre à jour l'email
  updateEmail(request: UpdateEmailRequestDto): Observable<ApiResponse<UpdateEmailResponseDto>> {
    return this.http.put<ApiResponse<UpdateEmailResponseDto>>(`${this.apiEmailUserUrl}/update-email`, request).pipe(
      tap((response) => {
        const currentUser = this.userSubject.getValue();
        if (currentUser && response.data) {
          this.userSubject.next({
            ...currentUser,
            email:  request.newEmail
          });
          this.logger.log('État utilisateur mis à jour après changement d’email :', this.userSubject.getValue());
        }
      })
    );
  }

  // Mettre à jour le mot de passe
  updatePassword(request: UpdatePasswordRequestDTO): Observable<ApiResponse<null>> {
    return this.http.put<ApiResponse<null>>(`${this.apiPasswordUrl}/update`, request).pipe(
      tap(() => {
        this.logger.log('Mot de passe mis à jour avec succès');
        // Pas de mise à jour de l'état utilisateur nécessaire, car le mot de passe n'est pas stocké dans User
      })
    );
  }

  // Mettre à jour manuellement l'utilisateur si nécessaire
  updateUser(user: User): void {
    this.userSubject.next(user);
    this.logger.log('Utilisateur mis à jour manuellement :', user);
  }

  getCurrentUserSync(): User | null {
    return this.userSubject.getValue();
  }
  // Réinitialiser l'utilisateur (par exemple, lors de la déconnexion)
  clearUser(): void {
    this.userSubject.next(null);
    this.logger.log('État utilisateur réinitialisé');
  }
}
