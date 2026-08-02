# TransmetroConecta - Auth Server

Auth Server is the central authentication and authorization microservice for the TransmetroConecta platform. Built with **.NET 8** and **Entity Framework Core**, it manages user registration, secure login via JWT, password recovery, and transactional authorizations.

It is part of the larger TransmetroConecta ecosystem and is containerized for easy deployment alongside other microservices via Docker.

## Tech Stack

- **Framework**: .NET 8.0 (C#)
- **Database**: PostgreSQL (Entity Framework Core 8.0)
- **Authentication**: JWT (JSON Web Tokens)
- **Validation**: FluentValidation
- **API Documentation**: Swagger / OpenAPI (Swashbuckle)
- **Containerization**: Docker & Docker Compose

## Folder Structure

```text
Auth-Server
├── Docker General            # General Docker configuration and scripts
├── Postman                   # Postman collections for API testing
├── TransmetroConecta.Auth    # .NET 8 Solution containing the application
│   ├── TransmetroConecta.Auth.API            # Presentation Layer (Controllers, Middlewares)
│   ├── TransmetroConecta.Auth.Application    # Application Layer (DTOs, Interfaces, Services)
│   ├── TransmetroConecta.Auth.Domain         # Domain Layer (Entities, Value Objects)
│   └── TransmetroConecta.Auth.Infrastructure # Infrastructure Layer (EF Core, Repositories)
├── docker-compose.yml        # Orchestration for the entire TransmetroConecta platform
└── README.md
```

## API Endpoints

### Authentication (`/api/Auth`)
- `POST /api/Auth/register` - Registers a new user.
- `POST /api/Auth/login` - Authenticates a user and returns a JWT token.
- `POST /api/Auth/recover-password` - Requests a password reset token.
- `POST /api/Auth/reset-password` - Resets the password using a valid token.
- `GET /api/Auth/users` - Retrieves all registered users (Requires `Admin` role).
- `POST /api/Auth/register-admin` - Registers a new administrator (Requires `Admin` role).

### Transactions (`/api/Transaction`)
- `POST /api/Transaction/recharge` - Processes a balance recharge for a user (Requires valid token).
- `POST /api/Transaction/purchase-card` - Initial purchase of a "Tarjeta Ciudadana" (Requires valid token).

## Getting Started

### Running with Docker Compose

The `Auth-Server` directory contains a comprehensive `docker-compose.yml` that orchestrates the entire TransmetroConecta platform, including databases (PostgreSQL, MongoDB), microservices, and frontends.

To spin up the ecosystem:

```bash
docker-compose up --build -d
```

The Auth Server will be available at: `http://localhost:8080`

### Running Locally (Without Docker)

1. Ensure you have **.NET 8 SDK** installed.
2. Update the database connection string in `TransmetroConecta.Auth.API/appsettings.json` to point to a running PostgreSQL instance.
3. Apply Entity Framework migrations (if necessary).
4. Run the API:
   ```bash
   cd TransmetroConecta.Auth/TransmetroConecta.Auth.API
   dotnet run
   ```

Swagger documentation will be accessible at: `http://localhost:<port>/swagger`