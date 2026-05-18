import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

interface Quote {
  id: number;
  text: string;
  author: string;
}

@Component({
  selector: 'app-quotes',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './quotes.html',
  styleUrls: ['./quotes.scss']
})
export class Quotes implements OnInit {
  quotes: Quote[] = [];
  isFormVisible = false;
  isEditing = false;
  editingQuoteId: number | null = null;
  formData = {
    text: '',
    author: ''
  };
  errorMessage = '';
  successMessage = '';
  nextId = 1;
  private quoteStorageKey = '';

  constructor(private cd: ChangeDetectorRef, private authService: AuthService) {}

  ngOnInit(): void {
    this.quoteStorageKey = this.authService.getUserStorageKey('quotes');
    this.loadQuotes();
  }

  loadQuotes(): void {
    const stored = localStorage.getItem(this.quoteStorageKey);
    if (stored) {
      this.quotes = JSON.parse(stored);
      this.nextId = Math.max(...this.quotes.map(q => q.id), 0) + 1;
      this.cd.detectChanges();
    }
  }

  saveQuotes(): void {
    localStorage.setItem(this.quoteStorageKey, JSON.stringify(this.quotes));
  }

  onAddNew(): void {
    this.isFormVisible = true;
    this.isEditing = false;
    this.editingQuoteId = null;
    this.formData = { text: '', author: '' };
    this.errorMessage = '';
    this.successMessage = '';
  }

  onEdit(quote: Quote): void {
    this.isFormVisible = true;
    this.isEditing = true;
    this.editingQuoteId = quote.id;
    this.formData = {
      text: quote.text,
      author: quote.author
    };
    this.errorMessage = '';
    this.successMessage = '';
  }

  onSave(): void {
    if (!this.formData.text || !this.formData.author) {
      this.errorMessage = 'All fields are required';
      return;
    }

    if (this.isEditing && this.editingQuoteId !== null) {
      const index = this.quotes.findIndex(q => q.id === this.editingQuoteId);
      if (index !== -1) {
        this.quotes[index] = { ...this.quotes[index], ...this.formData };
        this.errorMessage = '';
        this.successMessage = 'Quote updated successfully';
      }
    } else {
      const newQuote: Quote = {
        id: this.nextId++,
        ...this.formData
      };
      this.quotes = [newQuote, ...this.quotes];
      this.errorMessage = '';
      this.successMessage = 'Quote created successfully';
    }
    this.saveQuotes();
    this.resetForm();
    this.cd.detectChanges();
  }

  resetForm(): void {
    this.isFormVisible = false;
    this.isEditing = false;
    this.editingQuoteId = null;
    this.formData = { text: '', author: '' };
  }

  onCancel(): void {
    this.resetForm();
    this.errorMessage = '';
    this.successMessage = '';
  }

  onDelete(id: number): void {
    if (!confirm('Are you sure you want to delete this quote?')) {
      return;
    }
    this.quotes = this.quotes.filter(q => q.id !== id);
    this.saveQuotes();
    this.errorMessage = '';
    this.successMessage = 'Quote deleted successfully';
    this.cd.detectChanges();
  }
}