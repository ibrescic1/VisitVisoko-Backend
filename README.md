# VisitVisoko - Backend API

RESTful API for the **VisitVisoko** tourism platform, built to manage locations, categories, and user authentication.

---

## Key Features

- **Location Management:** Full CRUD operations for tourist attractions, landmarks, and cultural sites.
- **Search & Filtering:** Advanced filtering by categories, text search, and paginated responses for optimized data retrieval.
- **Authentication & Authorization:** Secure JWT (JSON Web Token) authentication mechanism.
- **Database Management:** Object-relational mapping via Entity Framework Core with PostgreSQL.
- **API Documentation:** Interactive Swagger / OpenAPI documentation built-in for rapid testing.

---

## Tech Stack

- **Framework:** .NET 10 Web API
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core
- **Authentication:** JWT (JSON Web Tokens)
- **Tooling:** Swagger / OpenAPI, .NET User Secrets

---

## Project Structure

```text
VisitVisoko-Backend/
├── VisitVisoko.API/            # Entry point, Controllers, Middlewares & Config
├── VisitVisoko.Core/           # Domain Entities, Interfaces & DTOs
├── VisitVisoko.Infrastructure/ # EF Core DB Context, Migrations & Repository Services
└── VisitVisoko.sln             # Visual Studio Solution File
```

---

## Getting Started

### Prerequisites

- .NET 10 SDK
- PostgreSQL

---

### Installation & Setup

#### 1. Clone the Repository

```bash
git clone https://github.com/ibrescic1/VisitVisoko-Backend.git
cd VisitVisoko-Backend
```

#### 2. Configure Local Secrets

Sensitive configuration is managed using **.NET User Secrets** instead of storing credentials directly in `appsettings.json`.

Initialize User Secrets:

```bash
dotnet user-secrets init --project VisitVisoko.API
```

Set the PostgreSQL connection string:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=VisitVisokoDb;Username=postgres;Password=your_password" --project VisitVisoko.API
```

Set the JWT secret key:

```bash
dotnet user-secrets set "Jwt:Key" "YOUR_LONG_JWT_SECRET_KEY_HERE" --project VisitVisoko.API
```

> **Note:** Replace the placeholder values with your local database credentials and a secure JWT secret.

---

#### 3. Apply Database Migrations

Run the following command to create or update the database:

```bash
dotnet ef database update --project VisitVisoko.Infrastructure --startup-project VisitVisoko.API
```

---

#### 4. Run the Application

```bash
dotnet run --project VisitVisoko.API
```

The API will be available at:

```text
http://localhost:5086
```

> The port may differ depending on your local configuration.

---

## API Endpoints

**GET** `/api/locations`  
Get paginated list of locations. Supports `page`, `pageSize`, `search`, and `categoryId`.

**GET** `/api/locations/{id}`  
Get detailed information for a specific location.

**POST** `/api/locations`  
Create a new location.

**PUT** `/api/locations/{id}`  
Update an existing location.

**DELETE** `/api/locations/{id}`  
Delete a location.

**GET** `/api/categories`  
Get all location categories.

**POST** `/api/auth/login`  
Authenticate a user/admin and receive a JWT token.

---

## API Documentation

The project includes interactive **Swagger / OpenAPI** documentation for exploring and testing API endpoints.

After starting the application, open:

```text
http://localhost:5086/swagger/index.html
```

Swagger allows you to:

- Explore available endpoints
- View request and response models
- Test API endpoints directly
- Authenticate using JWT Bearer tokens
- Inspect API responses

---

## Authentication

The API uses **JWT Bearer Authentication** for securing protected endpoints.

Protected endpoints require **Admin** authorization.

After logging in through:

```text
POST /api/auth/login
```

the returned JWT token can be used to access protected endpoints through the Swagger **Authorize** button.

---

## Database

The application uses:

- **PostgreSQL** as the relational database
- **Entity Framework Core** for object-relational mapping
- **EF Core Migrations** for database schema management

Database configuration is stored securely using **.NET User Secrets** during local development.