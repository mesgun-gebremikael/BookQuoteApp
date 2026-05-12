import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  username = '';
  password = '';
  errorMessage = '';

  constructor(private authService: AuthService) {}

  login(): void {
    this.authService.login(this.username, this.password).subscribe({
      next: (response) => {
        this.authService.saveToken(response.token);
        alert('Login successful');
      },
      error: () => {
        this.errorMessage = 'Wrong username or password';
      }
    });
  }
}