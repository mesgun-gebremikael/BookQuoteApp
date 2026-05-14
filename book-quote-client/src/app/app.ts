import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { CommonModule, NgClass } from '@angular/common';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive, NgClass],
  templateUrl: './app.html',
  styleUrls: ['./app.scss']
})
export class App {
  isDarkMode = false;
  isNavbarCollapsed = true;

  constructor(
    private router: Router,
    private authService: AuthService
  ) {
    this.isDarkMode = localStorage.getItem('theme') === 'dark';
    this.updateBodyClass();
  }

  get showNav(): boolean {
    return !['/login', '/register'].includes(this.router.url);
  }

  toggleTheme(): void {
    this.isDarkMode = !this.isDarkMode;
    localStorage.setItem('theme', this.isDarkMode ? 'dark' : 'light');
    this.updateBodyClass();
  }

  toggleNavbar(): void {
    this.isNavbarCollapsed = !this.isNavbarCollapsed;
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  private updateBodyClass(): void {
    document.body.classList.toggle('dark-mode', this.isDarkMode);
  }
}
