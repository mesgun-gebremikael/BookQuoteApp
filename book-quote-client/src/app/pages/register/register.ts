import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrls: ['./register.scss']
})
export class Register {
  username = '';
  password = '';
  confirmPassword = '';
  errorMessage = '';
  successMessage = '';
  isLoading = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  register(): void {
    this.errorMessage = '';
    this.successMessage = '';

    if (!this.username || !this.username.trim()) {
      this.errorMessage = 'Please enter a username.';
      return;
    }

    if (!this.password) {
      this.errorMessage = 'Please enter a password.';
      return;
    }

    if (!this.confirmPassword) {
      this.errorMessage = 'Please confirm your password.';
      return;
    }

    if (this.password.length < 8) {
      this.errorMessage = 'Password must be at least 8 characters.';
      return;
    }

    if (!/[A-Z]/.test(this.password)) {
      this.errorMessage = 'Password must contain at least one uppercase letter.';
      return;
    }

    if (!/[a-z]/.test(this.password)) {
      this.errorMessage = 'Password must contain at least one lowercase letter.';
      return;
    }

    if (!/[0-9]/.test(this.password)) {
      this.errorMessage = 'Password must contain at least one number.';
      return;
    }

    if (!/[^A-Za-z0-9]/.test(this.password)) {
      this.errorMessage = 'Password must contain at least one special character.';
      return;
    }

    if (this.password !== this.confirmPassword) {
      this.errorMessage = 'Passwords do not match.';
      return;
    }

    this.isLoading = true;
    this.authService.register(this.username, this.password, this.confirmPassword)
      .pipe(
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe({
        next: () => {
          this.successMessage = 'Registration successful. Please login.';
          this.router.navigate(['/login']);
        },
        error: (err) => {
          this.errorMessage = this.extractErrorMessage(err) || 'Registration failed. Check your username and password.';
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