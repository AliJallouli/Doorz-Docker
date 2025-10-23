import {Component, OnInit} from '@angular/core';
import {NavigationEnd, Router, RouterOutlet} from '@angular/router';
import { NavbarComponent } from './layout/navbar/navbar.component';
import {FooterComponent} from './layout/footer/footer.component';
import {AuthService} from './core/services/auth/auth.service';
import {Title} from '@angular/platform-browser';
import {environment} from '../environments/environment';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent{
  authReady = false;

  constructor(private router: Router,
              private authService: AuthService,
              private titleService: Title) {

    const appName = environment.production
      ? 'DoorZ'
      : environment.docker
        ? 'DoorZ [Docker DEV]'
        : 'DoorZ [Local DEV]';

    this.titleService.setTitle(appName);

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.authService.updateLastPrivateRoute(event.urlAfterRedirects);
      }
    });
  }



}

