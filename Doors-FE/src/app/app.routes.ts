import { Routes } from '@angular/router';
import { LoginComponent } from './features/login/login.component';
import { SuperadminHomeComponent } from './features/superadmin-home/superadmin-home.component';
import { LandingpageComponent } from './features/landingpage/landingpage.component';
import { LandlordHomeComponent } from './features/landlord-home/landlord-home.component';
import { CompanyHomeComponent } from './features/company-home/company-home.component';
import { InstitutionHomeComponent } from './features/institution-home/institution-home.component';
import { InvitationDashboardComponent } from './features/shared/invitation-dashboard/invitation-dashboard.component';
import { AuthGuard } from './core/guards/auth.guard';
import { NoAuthGuard } from './core/guards/no-auth.guard';
import { RequestResetPasswordComponent } from './features/register/reset-password/request-reset-password/request-reset-password.component';
import { ResetPasswordFormComponent } from './features/register/reset-password/reset-password-form/reset-password-form.component';
import { StudentHomeComponent } from './features/student-home/student-home.component';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./features/landingpage/landingpage.component')
      .then(m => m.LandingpageComponent),
    canActivate: [NoAuthGuard]
  },
  {
    path: 'student',
    loadComponent: () => import('./features/landingpage/landingpage.component')
      .then(m => m.LandingpageComponent),
    canActivate: [NoAuthGuard]
  },
  {
    path: 'student/:section',
    loadComponent: () => import('./features/landingpage/landingpage.component')
      .then(m => m.LandingpageComponent),
    canActivate: [NoAuthGuard]
  },
  {
    path: 'professionnel',
    loadComponent: () => import('./features/landingpage/landingpage.component')
      .then(m => m.LandingpageComponent),
    canActivate: [NoAuthGuard]
  },
  {
    path: 'professionnel/:section',
    loadComponent: () => import('./features/landingpage/landingpage.component')
      .then(m => m.LandingpageComponent),
    canActivate: [NoAuthGuard]
  },
  {
    path: 'login',
    loadComponent: () => import('./features/login/login.component')
      .then(m => m.LoginComponent)
  },
  {
    path: 'student-home',
    loadComponent: () => import('./features/student-home/student-home.component')
      .then(m => m.StudentHomeComponent),
    canActivate: [AuthGuard],
    data: { roles: ['student'] }
  },
  {
    path: 'student-home/:section',
    loadComponent: () => import('./features/student-home/student-home.component')
      .then(m => m.StudentHomeComponent),
    canActivate: [AuthGuard],
    data: { roles: ['student'] }
  },
  {
    path: 'landlord-home',
    loadComponent: () => import('./features/landlord-home/landlord-home.component')
      .then(m => m.LandlordHomeComponent),
    canActivate: [AuthGuard],
    data: { roles: ['landlord'] }
  },
  {
    path: 'company-home',
    loadComponent: () => import('./features/company-home/company-home.component')
      .then(m => m.CompanyHomeComponent),
    canActivate: [AuthGuard],
    data: { roles: ['company'] }
  },
  {
    path: 'institution-home',
    loadComponent: () => import('./features/institution-home/institution-home.component')
      .then(m => m.InstitutionHomeComponent),
    canActivate: [AuthGuard],
    data: { roles: ['institution'] }
  },
  {
    path: 'superadmin-home',
    loadComponent: () => import('./features/superadmin-home/superadmin-home.component')
      .then(m => m.SuperadminHomeComponent),
    canActivate: [AuthGuard],
    data: { roles: ['superadmin'] }
  },
  {
    path: 'superadmin/invitations',
    loadComponent: () => import('./features/shared/invitation-dashboard/invitation-dashboard.component')
      .then(m => m.InvitationDashboardComponent),
    canActivate: [AuthGuard],
    data: { roles: ['superadmin'] }
  },
  {
    path: 'company/invitations',
    loadComponent: () => import('./features/shared/invitation-dashboard/invitation-dashboard.component')
      .then(m => m.InvitationDashboardComponent),
    canActivate: [AuthGuard],
    data: { roles: ['company'], subRoles: ['admin'] }
  },
  {
    path: 'institution/invitations',
    loadComponent: () => import('./features/shared/invitation-dashboard/invitation-dashboard.component')
      .then(m => m.InvitationDashboardComponent),
    canActivate: [AuthGuard],
    data: { roles: ['institution'], subRoles: ['admin'] }
  },
  {
    path: 'register/company/invite',
    loadComponent: () => import('./features/register/register-from-invite/register-from-invite.component')
      .then(m => m.RegisterFromInviteComponent)
  },
  {
    path: 'register/institution/invite',
    loadComponent: () => import('./features/register/register-from-invite/register-from-invite.component')
      .then(m => m.RegisterFromInviteComponent)
  },
  {
    path: 'register/company/colleague/invite',
    loadComponent: () => import('./features/register/register-from-invite/register-from-invite.component')
      .then(m => m.RegisterFromInviteComponent)
  },
  {
    path: 'register/institution/colleague/invite',
    loadComponent: () => import('./features/register/register-from-invite/register-from-invite.component')
      .then(m => m.RegisterFromInviteComponent)
  },
  {
    path: 'register/public/:roleName',
    loadComponent: () => import('./features/register/register-form-public/register-form-public.component')
      .then(m => m.RegisterFormPublicComponent)
  },
  {
    path: 'confirm-email',
    loadComponent: () => import('./features/register/confirm-email/confirm-email.component')
      .then(m => m.ConfirmEmailComponent)
  },
  {
    path: 'request-reset-password',
    loadComponent: () => import('./features/register/reset-password/request-reset-password/request-reset-password.component')
      .then(m => m.RequestResetPasswordComponent)
  },
  {
    path: 'reset-password',
    loadComponent: () => import('./features/register/reset-password/reset-password-form/reset-password-form.component')
      .then(m => m.ResetPasswordFormComponent)
  },
  {
    path: 'contact',
    loadComponent: () => import('./features/contact/contact-form/contact-form.component')
      .then(m => m.ContactFormComponent)
  },
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full'
  }
];
