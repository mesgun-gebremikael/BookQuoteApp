import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrls: ['./login.scss']
})
export class Login {
  username = '';
  password = '';
  errorMessage = '';
  isLoading = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  login(): void {
    this.errorMessage = '';

    if (!this.username || !this.username.trim() || !this.password) {
      this.errorMessage = 'Please enter both username and password.';
      return;
    }

    this.isLoading = true;
    this.authService.login(this.username, this.password)
      .pipe(
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe({
        next: (response) => {
          this.authService.saveToken(response.token);
          this.router.navigate(['/books']);
        },
        error: (err) => {
          this.errorMessage = this.extractErrorMessage(err) || 'Wrong username or password.';
        }
      });
  }

  private extractErrorMessage(error: any): string {
    if (!error) {
      return '';
    }

    if (error.error?.message) {
      return error.error.message;
    }

    if (error.error?.errors) {
      const values = Object.values(error.error.errors).flat();
      return values.filter(Boolean).join(' ');
    }

    if (typeof error.error === 'string') {
      return error.error;
    }

    return error.message || error.statusText || '';
  }
}