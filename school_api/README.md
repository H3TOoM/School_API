# School Management System - API Documentation

## Executive Summary

This document provides a comprehensive technical audit, code review, and API documentation for the School Management System Web API built with .NET 8. The system has undergone SOLID refactoring to improve maintainability, testability, and scalability.

---

## Part 1: Code Review & Technical Audit

### 1.1 Code Quality Assessment

#### ✅ Strengths

| Area | Observation | Impact |
|------|-------------|--------|
| **Architecture** | Clear separation of concerns with distinct layers (API, Services, Repositories, Data) | High maintainability |
| **Dependency Injection** | Proper use of constructor injection throughout | High testability |
| **DTOs** | Well-structured Data Transfer Objects with validation attributes | Good data integrity |
| **Async/Await** | Consistent async/await pattern usage | Good performance |
| **Error Handling** | Global exception middleware with Strategy pattern | Clean error responses |

#### ⚠️ Code Smells & Improvements

| Issue | Location | Severity | Recommendation |
|-------|----------|----------|----------------|
| **AuditService** loads all records in memory | `AuditService.cs:60` | High | Implement server-side pagination with IQueryable before ToListAsync |
| **Magic strings** for roles | Multiple controllers | Medium | Use constants or enums for role names |
| **Token expiration** hardcoded | `TokenService.cs:50` | Low | Move to configuration settings |
| **Email service** uses simulated sending | `EmailService.cs` | Medium | Integrate with actual email provider (SendGrid, SES) |
| **BCrypt work factor** not configured | `AccountService.cs:33` | Low | Make work factor configurable |

### 1.2 Security Audit

#### ✅ Security Strengths

| Control | Implementation | Status |
|---------|----------------|--------|
| Password Hashing | BCrypt with salt | ✅ Secure |
| JWT Authentication | HS256 algorithm, proper claims | ✅ Secure |
| Role-based Authorization | `[Authorize(Roles = "...")]` | ✅ Implemented |
| Input Validation | DataAnnotations on DTOs | ✅ Present |
| HTTPS Redirection | `app.UseHttpsRedirection()` | ✅ Enabled |

#### ⚠️ Security Concerns

| Issue | Risk Level | Mitigation |
|-------|------------|------------|
| **JWT Key in appsettings.json** | High | Move to environment variables or Azure Key Vault |
| **CORS AllowAll policy** | Medium | Remove in production; use specific origins only |
| **No rate limiting** | Medium | Add `AddRateLimiter` middleware |
| **No request size limits** | Low | Configure `RequestSizeLimit` attribute on file uploads |
| **Console.WriteLine in catch** | Low | Use proper logging with `ILogger` in AuditService |

### 1.3 Performance Analysis

#### ✅ Performance Strengths

| Aspect | Implementation | Benefit |
|--------|----------------|---------|
| Async database operations | EF Core async methods | Non-blocking I/O |
| Pagination | Implemented in SearchService | Reduced memory usage |
| Includes for related data | `Include()` used properly | Fewer database round-trips |

#### ⚠️ Performance Issues

| Issue | Location | Impact | Solution |
|-------|----------|--------|----------|
| **Client-side pagination** | `AuditService.cs:118-125` | High | Move filtering/pagination to database level |
| **N+1 Query Risk** | `StudentService.cs` | Medium | Ensure eager loading with `Include()` |
| **Missing database indexes** | All entities | Medium | Add indexes on frequently queried fields |
| **TokenService loads all refresh tokens** | `TokenService.cs:85` | High | Add queryable repository method |

#### Recommended Database Indexes

```sql
-- Performance critical indexes
CREATE INDEX IX_Students_StudentClassId ON Students(StudentClassId);
CREATE INDEX IX_Students_ParentId ON Students(ParentId);
CREATE INDEX IX_Students_UserId ON Students(UserId);
CREATE INDEX IX_Teachers_DepartmentId ON Teachers(DepartmentId);
CREATE INDEX IX_Courses_SubjectId ON Courses(SubjectId);
CREATE INDEX IX_Schedules_StudentClassId ON Schedules(StudentClassId);
CREATE INDEX IX_Attendances_StudentId_Date ON Attendances(StudentId, Date);
CREATE INDEX IX_Grades_StudentId_CourserId ON Grades(StudentId, CourserId);
CREATE INDEX IX_RefreshTokens_Token ON RefreshTokens(Token);
CREATE INDEX IX_Users_Email ON Users(Email);
```

### 1.4 SOLID Principles Compliance (Post-Refactoring)

| Principle | Status | Implementation |
|-----------|--------|----------------|
| **SRP** | ✅ Compliant | Validation layer separated, configuration extracted |
| **OCP** | ✅ Compliant | Exception handlers use Strategy pattern, Specifications for search |
| **LSP** | ✅ Compliant | Repository error handling is consistent |
| **ISP** | ✅ Compliant | `IReadOnlyRepository<T>` for read-only needs |
| **DIP** | ✅ Compliant | Services depend on interfaces, not concrete implementations |

---

## Part 2: Technical Documentation

### 2.1 Architecture Overview

```
┌─────────────────────────────────────────────────────────┐
│                     API Layer                          │
│  ┌─────────────┐ ┌─────────────┐ ┌──────────────────┐  │
│  │ Controllers │ │ Middleware  │ │ Global Exception │  │
│  │             │ │             │ │     Handler    │  │
│  └─────────────┘ └─────────────┘ └──────────────────┘  │
└─────────────────────────────────────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────────┐
│                 Application Layer                        │
│  ┌─────────────┐ ┌─────────────┐ ┌──────────────────┐  │
│  │  Services   │ │ Validators  │ │ Specifications   │  │
│  │             │ │             │ │ & Sort Strategies│  │
│  └─────────────┘ └─────────────┘ └──────────────────┘  │
└─────────────────────────────────────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────────┐
│                  Infrastructure Layer                    │
│  ┌─────────────┐ ┌─────────────┐ ┌──────────────────┐  │
│  │Repositories │ │   Unit of   │ │   DbContext &    │  │
│  │             │ │    Work     │ │   EF Core        │  │
│  └─────────────┘ └─────────────┘ └──────────────────┘  │
└─────────────────────────────────────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────────┐
│                     Data Layer                           │
│              SQL Server Database                          │
└─────────────────────────────────────────────────────────┘
```

### 2.2 Design Patterns Used

| Pattern | Purpose | Files |
|---------|---------|-------|
| **Repository** | Abstract data access | `IRepository<T>`, `MainRepository<T>` |
| **Unit of Work** | Transaction management | `IUnitOfWork`, `UnitOfWork` |
| **Strategy** | Pluggable exception handling | `IExceptionHandler` implementations |
| **Specification** | Composable query filters | `ISpecification<T>`, `StudentSpecifications` |
| **Options Pattern** | Type-safe configuration | `JwtSettings`, `CorsSettings` |
| **DTO Pattern** | Data transfer between layers | All `*Dto.cs` files |

### 2.3 Project Structure

```
school_api/
├── Configuration/           # Configuration classes & extensions
│   ├── JwtSettings.cs
│   ├── CorsSettings.cs
│   └── ServiceCollectionExtensions.cs
├── Controllers/            # API Controllers (21 controllers)
├── Data/
│   ├── AppDbContext.cs   # EF Core DbContext
│   └── Models/           # Domain entities (16 entities)
├── DTOs/                 # Data Transfer Objects
├── Middleware/
│   ├── GlobalExceptionMiddleware.cs
│   └── ExceptionHandlers/# Strategy pattern implementations
├── Repoistories/
│   ├── Base/             # Repository interfaces
│   ├── MainRepoistory.cs
│   └── UnitOfWork.cs
├── Services/
│   ├── Base/             # Service interfaces
│   ├── Specifications/   # Specification pattern
│   ├── Sorting/          # Sort strategy pattern
│   └── *Service.cs       # Business logic
├── Validation/           # Validation layer
└── Program.cs
```

---

## Part 3: API Reference

### 3.1 Authentication Endpoints

#### POST /api/Account/Register
**Description:** Register a new user account

**Request Body:**
```json
{
  "name": "John Doe",
  "email": "john.doe@example.com",
  "password": "SecurePassword123!",
  "role": "Student"
}
```

**Success Response (200):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "refreshToken": "dGhpcyBpcyBhIHJlZnJlc2g...",
  "expiresAt": "2024-01-01T12:30:00Z",
  "user": {
    "id": 1,
    "name": "John Doe",
    "email": "john.doe@example.com",
    "role": "Student"
  }
}
```

**Error Responses:**
- `400 Bad Request` - Invalid input data
- `409 Conflict` - Email already exists

---

#### POST /api/Account/Login
**Description:** Authenticate user and receive JWT token

**Request Body:**
```json
{
  "email": "john.doe@example.com",
  "password": "SecurePassword123!"
}
```

**Success Response (200):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "refreshToken": "dGhpcyBpcyBhIHJlZnJlc2g...",
  "expiresAt": "2024-01-01T12:30:00Z",
  "user": { /* user details */ }
}
```

**Error Responses:**
- `400 Bad Request` - Invalid input
- `401 Unauthorized` - Invalid credentials

---

### 3.2 Student Management Endpoints

#### GET /api/Student
**Description:** Get all students

**Headers:**
```
Authorization: Bearer {jwt_token}
```

**Success Response (200):**
```json
[
  {
    "id": 1,
    "name": "Jane Smith",
    "dateOfBirth": "2005-03-15T00:00:00",
    "email": "jane.smith@school.edu",
    "phoneNumber": "+1234567890",
    "userId": 5,
    "parent": { "id": 2, "name": "Mary Smith" },
    "studentClass": { "id": 1, "name": "Class 10A" }
  }
]
```

---

#### GET /api/Student/{id}
**Description:** Get student by ID

**Parameters:**
- `id` (path, required): Student ID

**Success Response (200):**
```json
{
  "id": 1,
  "name": "Jane Smith",
  "dateOfBirth": "2005-03-15T00:00:00",
  "email": "jane.smith@school.edu",
  "phoneNumber": "+1234567890",
  "userId": 5,
  "parent": { "id": 2, "name": "Mary Smith" },
  "studentClass": { "id": 1, "name": "Class 10A" }
}
```

**Error Responses:**
- `401 Unauthorized` - Missing/invalid token
- `404 Not Found` - Student not found

---

#### POST /api/Student
**Description:** Create a new student

**Authorization:** Admin, Manager

**Request Body:**
```json
{
  "name": "Jane Smith",
  "dateOfBirth": "2005-03-15T00:00:00",
  "email": "jane.smith@school.edu",
  "phoneNumber": "+1234567890",
  "userId": 5,
  "parentId": 2,
  "studentClassId": 1
}
```

**Validation Rules:**
- Name: Required, max 100 characters
- Email: Valid email format, max 255 characters
- DateOfBirth: Required
- UserId: Required, positive integer
- StudentClassId: Required, positive integer

**Success Response (201):**
```json
{
  "id": 1,
  "name": "Jane Smith",
  "dateOfBirth": "2005-03-15T00:00:00",
  "email": "jane.smith@school.edu",
  "userId": 5,
  "parent": { "id": 2, "name": "Mary Smith" },
  "studentClass": { "id": 1, "name": "Class 10A" }
}
```

**Error Responses:**
- `400 Bad Request` - Validation failed
- `401 Unauthorized` - Not authenticated
- `403 Forbidden` - Insufficient permissions

---

#### PUT /api/Student/{id}
**Description:** Update student information

**Authorization:** Admin, Manager

**Request Body:**
```json
{
  "name": "Jane Doe",
  "email": "jane.doe@school.edu",
  "phoneNumber": "+9876543210",
  "parentId": 3,
  "studentClassId": 2
}
```

**Success Response (200):**
```json
{
  "id": 1,
  "name": "Jane Doe",
  "email": "jane.doe@school.edu",
  "phoneNumber": "+9876543210",
  "userId": 5,
  "parent": { "id": 3, "name": "Bob Smith" },
  "studentClass": { "id": 2, "name": "Class 10B" }
}
```

---

#### DELETE /api/Student/{id}
**Description:** Delete a student

**Authorization:** Admin only

**Success Response (204):** No content

**Error Responses:**
- `401 Unauthorized` - Not authenticated
- `403 Forbidden` - Not an admin
- `404 Not Found` - Student not found

---

### 3.3 Search Endpoints

#### GET /api/Search/students
**Description:** Search and filter students with pagination

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| `searchTerm` | string | Search in name or email |
| `departmentId` | int | Filter by department |
| `studentClassId` | int | Filter by class |
| `parentId` | int | Filter by parent |
| `dateOfBirthFrom` | date | Min date of birth |
| `dateOfBirthTo` | date | Max date of birth |
| `sortBy` | string | Sort field (name, dateofbirth) |
| `sortDescending` | bool | Sort direction |
| `page` | int | Page number (default: 1) |
| `pageSize` | int | Items per page (default: 10) |

**Success Response (200):**
```json
{
  "data": [ /* array of StudentReadDto */ ],
  "totalCount": 150,
  "page": 1,
  "pageSize": 10
}
```

---

### 3.4 Standard HTTP Status Codes

| Code | Meaning | When Used |
|------|---------|-----------|
| `200 OK` | Success | GET, PUT operations |
| `201 Created` | Resource created | POST operations |
| `204 No Content` | Success, no body | DELETE operations |
| `400 Bad Request` | Invalid input | Validation failures |
| `401 Unauthorized` | Not authenticated | Missing/invalid JWT |
| `403 Forbidden` | No permission | Insufficient role |
| `404 Not Found` | Resource missing | Invalid ID |
| `409 Conflict` | Business rule violation | Duplicate email |
| `500 Internal Server Error` | Server error | Unhandled exceptions |

---

## Part 4: Setup & Configuration Guide

### 4.1 Prerequisites

- .NET 8.0 SDK
- SQL Server (Express or higher)
- Visual Studio 2022 or VS Code

### 4.2 Database Configuration

1. **Update Connection String**
   Edit `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=SchoolDB;Trusted_Connection=True;TrustServerCertificate=True"
   }
   ```

2. **Run Migrations**
   ```bash
   dotnet ef database update
   ```

3. **Seed Initial Data** (Optional)
   ```bash
   dotnet run --seed
   ```

### 4.3 Security Configuration

1. **Generate JWT Secret Key**
   ```powershell
   # Generate a 256-bit key
   [Convert]::ToBase64String((New-Object byte[] 32 | ForEach-Object { Get-Random -Maximum 256 }))
   ```

2. **Move JWT Key to Environment Variable** (Production)
   ```powershell
   $env:JWT_KEY="your-generated-key"
   ```

3. **Configure CORS Origins**
   Edit `appsettings.json`:
   ```json
   "Cors": {
     "AllowedOrigins": [
       "https://your-frontend-domain.com"
     ]
   }
   ```

### 4.4 Running the Application

```bash
# Restore packages
dotnet restore

# Build
dotnet build

# Run
dotnet run

# Access Swagger UI
# https://localhost:7001/swagger (or your configured port)
```

### 4.5 Environment-Specific Configuration

Create `appsettings.Production.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Jwt": {
    "Key": "${JWT_KEY}",
    "Issuer": "SchoolAPI-Production",
    "Audience": "SchoolAPI-Production"
  },
  "Cors": {
    "AllowedOrigins": [
      "https://school.yourdomain.com"
    ]
  }
}
```

---

## Part 5: Testing Strategy

### 5.1 Unit Testing Approach

```csharp
// Example: StudentService Unit Test
public class StudentServiceTests
{
    private readonly Mock<IMainRepository<Student>> _repoMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly StudentService _service;

    public StudentServiceTests()
    {
        _repoMock = new Mock<IMainRepository<Student>>();
        _uowMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _service = new StudentService(
            _repoMock.Object, 
            _uowMock.Object, 
            _mapperMock.Object,
            Mock.Of<IValidator<StudentCreateDto>>(),
            Mock.Of<IValidator<StudentUpdateDto>>());
    }

    [Fact]
    public async Task GetStudentByIdAsync_ExistingId_ReturnsStudent()
    {
        // Arrange
        var student = new Student { Id = 1, Name = "Test" };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(student);
        _mapperMock.Setup(m => m.Map<StudentReadDto>(student))
            .Returns(new StudentReadDto { Id = 1, Name = "Test" });

        // Act
        var result = await _service.GetStudentByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
}
```

### 5.2 Integration Testing

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

---

## Appendix: Entity Relationship Diagram

```
User (1) ─────── (1) Student
    │                │
    │           (N) Attendance
    │                │
    │           (N) Grade
    │
    ├─────── (1) Teacher ───── (1) Department
    │                │
    │           (N) Course ──── (1) Subject
    │                │
    │           (N) Schedule ── (1) StudentClass
    │                              │
    │                         (N) Student
    │
    ├─────── (1) Staff ───── (1) Department
    │
    ├─────── (1) Manager ──── (1) Department
    │
    └─────── (1) Parent ───── (N) Student
```

---

**Document Version:** 1.0  
**Last Updated:** April 2026  
**Author:** Senior Software Architect Review
