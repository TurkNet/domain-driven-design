# Domain Driven Design

This is a sample project that focuses on implementing the best practices of domain driven design pattern.

## What is Domain Driven Design

Domain-Driven Design (DDD) is a software development approach that helps developers create applications aligned with the business needs and terminology of domain experts, users, and stakeholders. It proposes that the structure and language of the software code should closely match the business domain, allowing the software to focus on business requirements, reducing complexity, and encouraging collaboration across domains.

## Benefits
- Improved communication
- Increased productivity
- Improved maintainability
- Scalability
- Flexibility

## Getting Started
1. Create docker image
   ```bash
    docker run --name my-postgres -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=123456 -e POSTGRES_DB=postgres -p 5432:5432 -d postgres 
    ```
2. Apply migration
  ```bash
    dotnet ef database update --project Tn.Inventory.Infrastructure --startup-project Tn.Inventory.Presentation
  ```
3. Run the project
  ```bash
    dotnet run --project Tn.Inventory.Presentation
  ```
