import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book } from '../models/book';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'http://localhost:5138/api/Book';

  constructor(private http: HttpClient) { }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl);
  }
}
