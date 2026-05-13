import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Books } from './pages/books/books';

export const routes: Routes = [
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: 'books', component: Books },
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];