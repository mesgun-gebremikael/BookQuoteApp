# BookQuote - Responsive CRUD Web Application

A modern web application for managing a personal collection of books and quotes. Built as a coding test and practice project to demonstrate full-stack development with Angular and .NET.

## Features

- **User Authentication**: Secure registration and login using JWT tokens
- **Protected Routes**: Access to books and quotes pages requires authentication
- **Books Management**: Create, read, update, and delete books (user-specific data)
- **Quotes Management**: Create, read, update, and delete quotes linked to books (user-specific data)
- **User Isolation**: Each logged-in user sees only their own books and quotes
- **Responsive Design**: Works seamlessly on desktop, tablet, and mobile devices using Bootstrap
- **Dark/Light Mode**: Toggle between dark and light themes for comfortable viewing
- **Form Validation**: Clear error messages for registration and login validation

## Tech Stack

### Frontend
- **Framework**: Angular 21.2
- **Styling**: Bootstrap 5.3, SCSS
- **HTTP Client**: Angular HttpClient with JWT interceptors
- **Forms**: Reactive and Template-driven forms
- **State Management**: Component-level state with RxJS

### Backend
- **Framework**: .NET 9 Web API
- **Authentication**: JWT (JSON Web Tokens)
- **Database**: In-memory storage (can be extended to SQL Server, PostgreSQL, etc.)
- **CORS**: Enabled for frontend communication

## Project Structure

```
BookQuoteTest/
├── book-quote-client/          # Angular frontend application
│   ├── src/
│   │   ├── app/
│   │   │   ├── pages/          # Page components (login, register, books, quotes)
│   │   │   ├── services/       # Auth service, Book service
│   │   │   ├── guards/         # Route guards for protected pages
│   │   │   ├── models/         # TypeScript interfaces
│   │   │   └── app.ts          # Root component
│   │   ├── main.ts             # Entry point
│   │   └── styles.scss         # Global styles
│   ├── package.json            # Frontend dependencies
│   └── angular.json            # Angular CLI config
├── BookQuoteApi/               # .NET backend API
│   ├── Controllers/            # API controllers (Auth, Books, Quotes)
│   ├── Services/               # Business logic
│   ├── Models/                 # Database models
│   ├── Dtos/                   # Data transfer objects
│   └── Program.cs              # Application startup
└── README.md                   # This file
```

## How to Run

### Backend Setup

1. **Navigate to backend folder**:
   ```bash
   cd BookQuoteApi
   ```

2. **Restore NuGet packages**:
   ```bash
   dotnet restore
   ```

3. **Run the backend server** (default port: 5138):
   ```bash
   dotnet run
   ```

   The API will be available at `http://localhost:5138`

### Frontend Setup

1. **Navigate to frontend folder**:
   ```bash
   cd book-quote-client
   ```

2. **Install npm dependencies**:
   ```bash
   npm install
   ```

3. **Run the development server**:
   ```bash
   npm start
   ```

   The application will be available at `http://localhost:4200`

4. **Build for production**:
   ```bash
   npm run build
   ```

## How to Use and Test

### Registration

1. Open the app at `http://localhost:4200`
2. Click "Register here" link on the login page
3. Fill in the registration form:
   - **Username**: Any non-empty string (e.g., `john_doe`, `user123`)
   - **Password**: Must include:
     - At least 8 characters
     - One uppercase letter
     - One lowercase letter
     - One number
     - One special character (e.g., `MyPass@2024`)
4. Confirm your password and click "Register"
5. You'll be redirected to login

### Test Credentials

Use these credentials to quickly test the app:
- **Username**: `mesgun`
- **Password**: `Test@1234`

### Login

1. Enter your username and password
2. Click "Login"
3. If successful, you'll be redirected to the Books page
4. If authentication fails, you'll see a clear error message

### Managing Books

1. After logging in, navigate to the Books page
2. Click "Add Book" to create a new book entry
3. Fill in the title and author, then save
4. Your books appear only to you in your personal collection
5. Edit or delete books as needed

### Managing Quotes

1. Go to the "My Quotes" page
2. Click "Add Quote" to create a new quote
3. Select a book from your collection and add the quote text
4. Your quotes are linked to your books and visible only to you
5. Edit or delete quotes as needed

### Dark/Light Mode

- Click the dark/light mode toggle button in the navbar
- Your preference is saved and persists between sessions

## Notes

- **User-Specific Data**: All books and quotes are stored separately per logged-in user. One user cannot see another user's data.
- **JWT Authentication**: The login process returns a JWT token that is stored locally and sent with every protected API request
- **Protected Routes**: If you try to access books or quotes without logging in, you'll be redirected to the login page
- **Error Handling**: The app displays clear, user-friendly error messages for invalid inputs and failed requests
- **Responsive Layout**: The Bootstrap-based design adapts automatically to different screen sizes

## Future Improvements

- **Database Integration**: Replace in-memory storage with a real database (SQL Server, PostgreSQL, etc.)
- **Search and Filtering**: Add search functionality for books and quotes
- **Export/Import**: Allow users to export their books and quotes to CSV or JSON
- **Ratings and Reviews**: Add rating and review features for books
- **Social Features**: Share quotes with other users or make them public
- **Advanced Validation**: Server-side validation for duplicate book titles per user
- **Email Verification**: Require email confirmation during registration
- **Password Reset**: Add "Forgot Password" functionality
- **User Profile**: Allow users to update their profile information
- **Pagination**: Handle large collections of books and quotes with pagination
- **Performance**: Add caching, lazy loading, and API response optimization
- **Testing**: Add unit tests and e2e tests for both frontend and backend
- **CI/CD Pipeline**: Set up automated testing and deployment

## License

This is a practice/test project. Feel free to use and modify as needed.
