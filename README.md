# SignUp API Project

A clean architecture implementation of a user signup system following Domain-Driven Design (DDD) and Test-Driven Development (TDD) principles.

## 🚀 Tech Stack

- **Backend Framework**: .NET 9
- **Database**: Microsoft SQL Server
- **Testing**:
  - NUnit (Testing Framework)
  - FluentAssertions (Assertion Library)
  - NSubstitute (Mocking Framework)
- **Architecture**:
  - Clean Architecture
  - Domain-Driven Design (DDD)
  - Test-Driven Development (TDD)

## 📁 Project Structure

```
SignUp-App/
├── src/
│   ├── Api/                 # Web API layer
│   ├── Application/         # Application layer
│   ├── Domain/             # Domain layer
│   └── Infrastructure/     # Infrastructure layer
└── tests/
    └── UserManagement/
        ├── ApiTests/
        ├── ApplicationTests/
        ├── DomainTests/
        └── InfrastructureTests/
```

## 🎯 Features

- User signup endpoint (`POST /signup`)
- Email validation with uniqueness check
- Password strength validation
- Comprehensive unit tests
- Clean Architecture implementation

## ⚙️ Prerequisites

- .NET 9 SDK
- SQL Server
- Visual Studio 2022 or VS Code or Rider

## 🏃‍♂️ Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/ManarGamal885/signup-app.git
   cd SignUp-App
   ```

2. **Update database connection**
   - Navigate to `src/Api/appsettings.json`
   - Update the connection string for your SQL Server instance

3. **Run database migrations**
   ```bash
   cd Infrastructure/Migrations
   dotnet ef database update
   ```

4. **Run the API**
   ```bash
   dotnet run
   ```

5. **Run tests**
   ```bash
   dotnet test
   ```

## 🔗 API Endpoints

### POST /signup

Creates a new user account.

#### Request Body
```json
{
    "email": "user@example.com",
    "fullName": "John Doe",
    "password": "SecurePassword123!",
    "confirmPassword": "SecurePassword123!"
}
```

#### Validation Rules

**Email Requirements:**
- Must be unique in the system
- Must follow a valid email format

**Password Requirements:**
- Minimum 8 characters
- At least one uppercase letter
- At least one lowercase letter
- At least one number
- Optional special character

## 🧪 Testing

The project follows Test-Driven Development with comprehensive test coverage:

```bash
# Run all tests
dotnet test

# Run a specific test project
dotnet test tests/UserManagement/[TestProject]
```

## 🏗️ Architecture

Built following Clean Architecture principles:

1. **Domain Layer**
   - Business entities
   - Domain logic
   - Interfaces

2. **Application Layer**
   - Use cases
   - Application services
   - DTOs

3. **Infrastructure Layer**
   - Database context
   - Repositories
   - External services

4. **API Layer**
   - Controllers
   - DTOs
   - Middleware

## 📖 User Story

**As a new user, I want to sign up with a unique email address, my full name, and a secure password (entered twice for confirmation) so that I can create an account and access the system with confidence in my account's security.**

### Acceptance Criteria

1. **Signup Form Fields:**
   - Email Address (unique, valid format)
   - Full Name
   - Password
   - Confirm Password

2. **Email Validation:**
   - Valid format (user@example.com)
   - Uniqueness check
   - Error messages for invalid/duplicate emails

3. **Password Requirements:**
   - Minimum 8 characters
   - At least one uppercase letter
   - At least one lowercase letter
   - At least one numeric digit
   - Optional special character
   - Error messages for complexity issues

4. **Password Confirmation:**
   - Must match the original password
   - Error message for mismatch

5. **User Feedback:**
   - Immediate feedback on errors
   - Success confirmation message
