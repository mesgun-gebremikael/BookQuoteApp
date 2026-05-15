import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BookService } from '../../services/book.service';
import { Book } from '../../models/book';

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './books.html',
  styleUrls: ['./books.scss']
})
export class Books implements OnInit {
  books: Book[] = [];
  isFormVisible = false;
  isEditing = false;
  editingBookId: number | null = null;
  formData = {
    title: '',
    author: '',
    publishedDate: ''
  };
  errorMessage = '';
  successMessage = '';

  constructor(private bookService: BookService, private cd: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.errorMessage = '';
    this.bookService.getBooks().subscribe({
      next: (books) => {
        this.books = books;
        this.errorMessage = '';
        this.cd.detectChanges();
      },
      error: () => {
        this.errorMessage = 'Failed to load books';
      }
    });
  }

  onAddNew(): void {
    this.isFormVisible = true;
    this.isEditing = false;
    this.editingBookId = null;
    this.formData = { title: '', author: '', publishedDate: '' };
    this.errorMessage = '';
    this.successMessage = '';
  }

  onEdit(book: Book): void {
    this.isFormVisible = true;
    this.isEditing = true;
    this.editingBookId = book.id;
    this.formData = {
      title: book.title,
      author: book.author,
      publishedDate: book.publishedDate
    };
    this.errorMessage = '';
    this.successMessage = '';
  }

  onSave(): void {
    if (!this.formData.title || !this.formData.author || !this.formData.publishedDate) {
      this.errorMessage = 'All fields are required';
      return;
    }

    if (this.isEditing && this.editingBookId !== null) {
      this.bookService.updateBook(this.editingBookId, this.formData).subscribe({
        next: (updated) => {
          const idx = this.books.findIndex(b => b.id === this.editingBookId);
          if (idx !== -1) {
            this.books[idx] = updated;
          }
          this.errorMessage = '';
          this.successMessage = 'Book updated successfully';
          this.resetForm();
          this.loadBooks();
        },
        error: () => {
          this.errorMessage = 'Failed to update book';
        }
      });
    } else {
      this.bookService.createBook(this.formData).subscribe({
        next: (created) => {
          this.books = [created, ...this.books];
          this.errorMessage = '';
          this.successMessage = 'Book created successfully';
          this.resetForm();
          this.loadBooks();
        },
        error: () => {
          this.errorMessage = 'Failed to create book';
        }
      });
    }
  }

  resetForm(): void {
    this.isFormVisible = false;
    this.isEditing = false;
    this.editingBookId = null;
    this.formData = { title: '', author: '', publishedDate: '' };
  }

  onCancel(): void {
    this.resetForm();
    this.errorMessage = '';
    this.successMessage = '';
  }

  onDelete(id: number): void {
    if (!confirm('Are you sure you want to delete this book?')) {
      return;
    }

    this.errorMessage = '';

    this.bookService.deleteBook(id).subscribe({
      next: () => {
        this.books = this.books.filter(b => b.id !== id);
        this.successMessage = 'Book deleted successfully';
        this.cd.detectChanges();
      },
      error: () => {
        this.errorMessage = 'Failed to delete book';
      }
    });
  }
}
