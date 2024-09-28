Framework : .Net 8.0
Architecture: Onion Layer Architecture
Logging : log4net
Swagger
Repository factory is used to create the dynamic interface instance based on the API parameter

Onion Architecture

Layers in Onion Architecture:
Core Layer (Innermost Layer):
Contains the domain entities, business logic, and domain services.
It represents the heart of the application, encapsulating the fundamental business rules and models.
Infrastructure Layer:
Handles external concerns like database access, file I/O, or web API integration.
Dependencies point inwards toward the Core, allowing the Core to remain unaware of specific infrastructure implementations.
Application Layer:
Orchestrates communication between the Core and the external layers.
Contains application-specific logic, such as service interfaces or use cases.
Presentation Layer (Outermost Layer):
Includes UI components like web interfaces, APIs, or user-facing applications.
Should depend only on the Application layer, ensuring a separation between UI and core business logic.
Implementing Onion Architecture in ASP.NET Core 8.0:

ExtModule.API:Presentation Layer
Infrastructure :ExtModule.API.infra
Core:ExtModule.API.Core
Application: Extmodule.API.App

Prsentation: API
