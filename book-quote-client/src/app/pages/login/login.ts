import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { finalize, timeout } from 'rxjs/operators';
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
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  login(): void {
    if (this.isLoading) {
      return;
    }

    this.errorMessage = '';

    if (!this.username || !this.username.trim() || !this.password) {
      this.errorMessage = 'Please enter both username and password.';
      return;
    }

    this.isLoading = true;
    this.authService.login(this.username, this.password)
      .pipe(
        timeout(8000),
        finalize(() => {
          this.isLoading = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe({
        next: (response) => {
          this.authService.saveToken(response.token);
          this.router.navigate(['/books']);
        },
        error: (err) => {
          if (err?.name === 'TimeoutError') {
            this.errorMessage = 'Server is not responding. Please try again.';
          } else if (err?.status === 401 || err?.status === 400) {
            this.errorMessage = 'Invalid username or password.';
          } else {
            this.errorMessage = this.extractErrorMessage(err) || 'Invalid username or password.';
          }
          this.cdr.detectChanges();
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