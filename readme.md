# EVENTNET

A sample project that demonstrates Domain-Driven Design (DDD) architecture using modern technologies and best practices.
At its core, this sample project is an event management system that lets you create events and manage their attendees—both individual and business.
The goal is to learn and share knowledge and use it as reference for new projects.

## PRINCIPLES and PATTERNS

* DDD Architecture
* Clean Code
* SOLID Principles
* KISS Principle
* DRY Principle
* Fail Fast Principle
* Mediator Pattern
* Result Pattern
* Folder-by-Feature Structure
* Separation of Concerns

## BENEFITS

* Simple and evolutionary architecture.
* Avoid cyclical references.
* Avoid unnecessary dependency injection.
* Segregation by feature instead of technical type.
* Single responsibility for each request and response.
* Simplicity of unit testing.

## TECHNOLOGIES

* [.NET 9](https://dotnet.microsoft.com/download)
* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core)
* [C#](https://docs.microsoft.com/en-us/dotnet/csharp)
* [Angular 19](https://angular.dev)
* [UIkit](https://getuikit.com/docs/introduction)

## RUN

<details>
<summary>Command Line</summary>

#### Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download/dotnet)
* [PostgreSQL](https://www.postgresql.org/download/)
* [Node](https://nodejs.org)
* [Angular CLI](https://angular.dev/tools/cli)

#### Steps

1. Open directory **source\Web\Frontend** in command line and execute **npm run restore**.
2. Open directory **source\Web** in command line and execute **dotnet run**.
3. Open <https://localhost:8090>.

</details>

<details>
<summary>Visual Studio Code</summary>

#### Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download/dotnet)
* [PostgreSQL](https://www.postgresql.org/download/)
* [Node](https://nodejs.org)
* [Angular CLI](https://angular.dev/tools/cli)
* [Visual Studio Code](https://code.visualstudio.com)
* [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)

#### Steps

1. Open directory **source\Web\Frontend** in command line and execute **npm run restore**.
2. Open **source** directory in Visual Studio Code.
3. Press **F5**.

</details>

<details>
<summary>Visual Studio</summary>

#### Prerequisites

* [Visual Studio](https://visualstudio.microsoft.com)
* [Node](https://nodejs.org)
* [Angular CLI](https://angular.dev/tools/cli)

#### Steps

1. Open directory **source\Web\Frontend** in command line and execute **npm run restore**.
2. Open **source\EventNet.sln** in Visual Studio.
3. Set **EventNet.Web** as startup project.
4. Press **F5**.

</details>

<details>
<summary>Docker</summary>

#### Prerequisites

* [Docker](https://www.docker.com/get-started)

#### Steps

1. In a terminal open project root directory
2. Execute **docker compose up --detach --build --force-recreate --remove-orphans**.
3. Open <http://localhost:8090>.

</details>


## LAYERS

**Web:** Frontend and Backend.

**Application:** Flow control.

**Domain:** Business rules and domain logic.

**Model:** Data transfer objects.

**Infrastructure:** Data persistence.

## WEB

### FRONTEND

The frontend is built using Angular 19 along with UIkit for UI components and styling. The project follows the standard Angular structure, with dedicated services for data handling. These services are responsible for managing HTTP calls, error handling, and data transformations.

### Service

Services act as the interface between the frontend and the backend. They hold the logic that doesn’t belong in components, such as handling API endpoints, constructing requests, and processing responses.

### BACKEND

The backend is powered by ASP.NET Core and acts as the entry point for API requests as well as static assets. It handles interactions with the domain and application layers and is configured to serve the Angular frontend via a SPA proxy.

### Controller

Controllers are intentionally kept thin. They have no business logic, rules, or dependencies other than the mediator. Their primary role is to translate HTTP requests into application commands or queries and return the resulting responses.

### Swagger

Swagger is integrated into the backend to automatically generate API documentation and provide a UI for testing API endpoints.

## APPLICATION

The Application layer orchestrates the business flow without containing the core business rules. It acts as the glue between the user interface, domain, and infrastructure, ensuring that data moves seamlessly through the system.

### Request

Requests encapsulate the data needed to perform an action. They are simple DTOs carrying input data from the client to the application layer. For instance, AddEventRequest contains all necessary details to create a new event.

### Request Validator

Request Validators enforce generic, non-domain-specific rules. They perform basic validations—such as checking required fields and formats—without enforcing business rules, ensuring that the input data is structurally sound before it reaches the domain.

### Response

Responses are DTOs representing the outcome of a request. They encapsulate properties that are sent back to the client, including success indicators, data payloads, and error messages when necessary.

### Handler

Handlers coordinate the business flow by processing requests and returning responses. They orchestrate calls to factories, repositories, unit of work, services, or mediators, but deliberately avoid embedding business rules. Their role is strictly to direct the flow of data and actions within the application.

## DOMAIN

The domain layer represents the heart of the application. It models the business rules and processes that are intrinsic to the application. This layer is free from external dependencies, ensuring that the business logic remains clean and testable.

### Aggregate

An aggregate is a cluster of related entities that are treated as a single unit for data changes. It defines a consistency boundary where transactional invariants are maintained.

The Event class is the aggregate root that represents a domain concept with a well-defined boundary. It encapsulates child entities like Attendee (and its derived types) to ensure consistency.

All changes (like adding, updating, or removing attendees) occur via methods on the aggregate root, ensuring business rules are consistently applied.

Methods such as AddAttendee enforce rules (e.g., checking for duplicates) before modifying the collection.

### Entity

Entities are domain objects that have a unique identity, which persists over time, even if their properties change.

Changing properties is only allowed through internal business methods in the entity, not through direct access to the properties.

### Value Object

Value objects are immutable and defined solely by the values of their properties. They do not have a unique identity and should be replaced rather than modified.

The PersonalIdCode is a value object that validates the ID code (including checksum validation) upon creation and ensures immutability.

## MODEL

Models are lightweight Data Transfer Objects (DTOs) that carry data between different layers of our application. They are designed solely for transporting data and are devoid of business logic. For example, the EventModel captures the essential information about an event, including its basic properties and a list of associated attendees. Models make it easier to serialize and return data while keeping our domain layer focused on business rules.

## INFRASTRUCTURE

The Infrastructure layer encapsulates everything related to data persistence, configuration, and data access. It serves as the bridge between domain model and external systems such as databases. This layer leverages technologies like Entity Framework Core to map domain objects to a relational database, ensuring that the domain model remains clean and decoupled from persistence concerns.

### Context

The Context is the central point for configuring and representing our database. It handles setting up the connection, applying entity configurations, and seeding the initial data. Think of it as our gateway to the database, ensuring that all our domain entities are correctly mapped and ready for persistence.

### Entity Configuration

Entity Configuration defines how our domain objects translate into database tables and columns. It covers everything from setting table names and column types to configuring relationships and constraints. This ensures that each entity, including those with inheritance (like different Attendee types), is stored accurately and consistently. By using these configurations, we maintain control over the database schema without polluting our domain logic.

### Repository

Repositories serve as the bridge between our domain and the database. They extend generic repository capabilities with domain-specific methods, allowing you to query and transform data into models suitable for our application. By encapsulating data access logic, repositories keep our domain layer clean and focused on business rules, ensuring that all persistence concerns remain isolated in the infrastructure.
