import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Books } from './pages/books/books';

export const routes: Routes = [
  { path: 'login', component: Login },
  { path: 'books', component: Books },
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];