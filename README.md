# 🌸 Hobby Floral E-Commerce WebApp

This is a full-stack e-commerce application built with **Blazor WebAssembly** for a floral business. It supports client-side shopping experiences, secure authentication, and robust backend integration using .NET technologies and Azure services.

---

## 🖥️ Frontend

- **Blazor WebAssembly**: Client-side web framework using C# instead of JavaScript
- **MudBlazor**: Material Design component library for Blazor
- **LocalStorage API**: Manages cart and session data for guest users
- **JWT Authentication**: Token-based authentication and secure access control

---

## 🛠️ Backend

- **.NET 6 API**: High-performance web API framework
- **Entity Framework Core**: ORM for database operations
- **SQL Server**: Relational database for structured data persistence
- **Azure Blob Storage**: Stores product images in the cloud
- **Stripe Integration**: Handles secure payment processing

---

## 🏗️ Architectural Overview

### Layered Architecture

#### Presentation Layer (Client)
- Blazor WebAssembly SPA
- UI components powered by MudBlazor
- Client-side service-based state management
- Mobile-responsive design

#### API Layer (Server)
- RESTful API endpoints
- Controller-based routing
- Middleware for authentication
- Input/request validation

#### Service Layer
- Encapsulation of business logic
- Data validation and transformation
- Cross-cutting concerns (logging, caching)
- Integration with Stripe and Azure services

#### Data Access Layer
- Entity Framework Core `DbContext`
- Repository pattern implementation
- Database seeding and migrations
- Query performance optimizations

#### Database Layer
- SQL Server (Azure-hosted with local dev options)
- Normalized relational schema
- Use of indexes and foreign key constraints

---

## 🔑 Key Architectural Features

### Clean Separation of Concerns
- Clear boundaries between layers
- Interface-driven dependency injection
- High testability and maintainability

### Hybrid Storage Strategy
- LocalStorage support for guest users
- Persistent DB storage for authenticated users
- Sync mechanism between client and server state

### Shared DTOs
- Common data transfer objects between client and server
- Centralized validation logic
- Ensures type-safety across app boundaries

### Secure Authentication Flow
- JWT token issuance and validation
- Role-based access control
- Secure password management

### Scalable Cloud Integration
- Azure SQL Database for relational data
- Azure Blob Storage for images
- Environment-specific configuration options

---

## 🚧 Project Status

This project is under active development as part of a capstone initiative. Contributions, suggestions, and testing feedback are welcome.
# 🌸 Hobby Floral E-Commerce WebApp

This is a full-stack e-commerce application built with **Blazor WebAssembly** for a floral business. It supports client-side shopping experiences, secure authentication, and robust backend integration using .NET technologies and Azure services.

---

## 🖥️ Frontend

- **Blazor WebAssembly**: Client-side web framework using C# instead of JavaScript
- **MudBlazor**: Material Design component library for Blazor
- **LocalStorage API**: Manages cart and session data for guest users
- **JWT Authentication**: Token-based authentication and secure access control

---

## 🛠️ Backend

- **.NET 6 API**: High-performance web API framework
- **Entity Framework Core**: ORM for database operations
- **SQL Server**: Relational database for structured data persistence
- **Azure Blob Storage**: Stores product images in the cloud
- **Stripe Integration**: Handles secure payment processing

---

## 🏗️ Architectural Overview

### Layered Architecture

#### Presentation Layer (Client)
- Blazor WebAssembly SPA
- UI components powered by MudBlazor
- Client-side service-based state management
- Mobile-responsive design

#### API Layer (Server)
- RESTful API endpoints
- Controller-based routing
- Middleware for authentication
- Input/request validation

#### Service Layer
- Encapsulation of business logic
- Data validation and transformation
- Cross-cutting concerns (logging, caching)
- Integration with Stripe and Azure services

#### Data Access Layer
- Entity Framework Core `DbContext`
- Repository pattern implementation
- Database seeding and migrations
- Query performance optimizations

#### Database Layer
- SQL Server (Azure-hosted with local dev options)
- Normalized relational schema
- Use of indexes and foreign key constraints

---

## 🔑 Key Architectural Features

### Clean Separation of Concerns
- Clear boundaries between layers
- Interface-driven dependency injection
- High testability and maintainability

### Hybrid Storage Strategy
- LocalStorage support for guest users
- Persistent DB storage for authenticated users
- Sync mechanism between client and server state

### Shared DTOs
- Common data transfer objects between client and server
- Centralized validation logic
- Ensures type-safety across app boundaries

### Secure Authentication Flow
- JWT token issuance and validation
- Role-based access control
- Secure password management

### Scalable Cloud Integration
- Azure SQL Database for relational data
- Azure Blob Storage for images
- Environment-specific configuration options

---

## 🚧 Project Status

This project is under active development as part of a capstone initiative. Contributions, suggestions, and testing feedback are welcome.
