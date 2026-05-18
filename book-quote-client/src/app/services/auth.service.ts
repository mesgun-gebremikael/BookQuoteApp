import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

interface LoginResponse {
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5138/api/Auth';
  private authState = new BehaviorSubject<boolean>(this.isLoggedIn());
  authChanges$ = this.authState.asObservable();

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, {
      username,
      password
    });
  }

  register(username: string, password: string, confirmPassword: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, {
      username,
      password,
      confirmPassword
    });
  }

  saveToken(token: string): void {
    localStorage.setItem('token', token);
    this.authState.next(true);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  private decodeToken(): any | null {
    const token = this.getToken();
    if (!token) {
      return null;
    }

    const payload = token.split('.')[1];
    if (!payload) {
      return null;
    }

    try {
      return JSON.parse(atob(payload));
    } catch {
      return null;
    }
  }

  getUserId(): number | null {
    const decoded = this.decodeToken();
    const idValue =
      decoded?.nameid ||
      decoded?.sub ||
      decoded?.id ||
      decoded?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];

    const id = Number(idValue);
    return Number.isInteger(id) ? id : null;
  }

  getUsername(): string | null {
    const decoded = this.decodeToken();
    return (
      decoded?.unique_name ||
      decoded?.name ||
      decoded?.preferred_username ||
      decoded?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] ||
      null
    );
  }

  getUserStorageKey(prefix: string): string {
    const userId = this.getUserId();
    const username = this.getUsername() || 'guest';
    return `${prefix}_${userId ?? 'guest'}_${username}`;
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout(): void {
    localStorage.removeItem('token');
    this.authState.next(false);
  }
}