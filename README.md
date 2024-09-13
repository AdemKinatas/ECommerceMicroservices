# E-Commerce Basket and Order Microservices Project

## Purpose

The goal of this project is to develop a microservices-based architecture for managing the basket and order processes of an e-commerce site. The project includes creating .NET Web API services that communicate through message queues and perform specific operations such as adding items to the basket and sending orders.

## Architecture

The architecture is designed using microservices with the following key components:

- **Gateway API**: Handles communication with the UI and routes requests to the appropriate services via message queues.
- **Basket Service**: Manages the basket operations, storing data in Redis, and communicating with other services through message queues.
- **Order Service**: Processes order data, stores it in the database, and communicates status updates back to the Basket Service.

## Endpoints

### 1. Add Basket

- **Description**: Adds items to the basket.
- **Process**: 
  - The Gateway API sends a request via message queue to the Basket Service.
  - The Basket Service stores the basket information in Redis.
- **Request Model**:
  - `ProductName` (string)
  - `Quantity` (int)
  - `Price` (decimal)

### 2. Send Order

- **Description**: Sends the order from the basket.
- **Process**: 
  - The Gateway API sends a request via message queue to the Basket Service.
  - The Basket Service retrieves the basket data from Redis and sends it to the Order Service via message queue.
  - The Order Service saves the order data in the database.
  - Upon successful order creation, the Order Service sends a confirmation back to the Basket Service, which then deletes the basket from Redis.
  - The basket in Redis has an expiry time (TTL) of 2 minutes and will be automatically deleted if no action is taken within this time.

## Requirements

- The project must be developed using .NET Core or .NET 7.0 and above.
- RabbitMQ is used for the message queue structure, with MassTransit as the Bus/Client.
- Logging should preferably use NLog. Custom Appenders can be added based on preference.
- Use gRPC protocol for communication (MassTransit gRPC client can be utilized).
- Utilize modern and up-to-date libraries.
- NoSQL databases are preferred; Redis is used for basket storage.
- The project should be containerized using Docker, with a Docker Compose file for setup.
- A log management system like Graylog, Splunk, or ELK should be implemented.
- Validation should be handled using FluentValidation.
- Load testing should be conducted for up to 100,000 orders, and results should be recorded in a text file or console output.

## Additional Setup

1. **Docker**: Ensure Docker and Docker Compose are installed. Use the provided `docker-compose.yml` file to set up the services.
2. **Message Queue**: RabbitMQ should be set up with the necessary configurations for MassTransit communication.
3. **Logging System**: Integrate the preferred logging system (Graylog, Splunk, or ELK) for tracking and monitoring logs.
4. **Load Testing**: Execute load tests for performance validation, aiming to handle 100,000 concurrent orders.

## Getting Started

1. Clone the repository.
2. Navigate to the project directory.
3. Set up the environment variables as required.
4. Run `docker-compose up` to start all services.
5. Use a tool like Postman to test the endpoints.

---

Let me know if you need further adjustments or additional sections!
