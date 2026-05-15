import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, mapTo } from 'rxjs';
import { Book } from '../models/book';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'http://localhost:5138/api/Book';

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  private getAuthHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return token
      ? new HttpHeaders({ Authorization: `Bearer ${token}` })
      : new HttpHeaders();
  }

  getBooks(): Observable<Book[]> {
    const headers = this.getAuthHeaders();
    return this.http.get<Book[]>(this.apiUrl, { headers });
  }

  createBook(book: Omit<Book, 'id'>): Observable<Book> {
    const headers = this.getAuthHeaders();
    return this.http.post<Book>(this.apiUrl, book, { headers });
  }

  updateBook(id: number, book: Omit<Book, 'id'>): Observable<Book> {
    const headers = this.getAuthHeaders();
    return this.http.put<Book>(`${this.apiUrl}/${id}`, book, { headers });
  }

  deleteBook(id: number): Observable<void> {
    const headers = this.getAuthHeaders();
    return this.http.delete(`${this.apiUrl}/${id}`, {
      headers,
      responseType: 'text'
    }).pipe(mapTo(void 0));
  }
}
