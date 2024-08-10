# Quote Request System

This project is a full-stack web application for gathering and managing quote requests from users. It consists of a .NET 8 backend API and an Angular 18 frontend.

## Project Structure

```
QuoteRequestSystem/
├── backend/             # .NET 8 backend
├── frontend/            # Angular 18 frontend
├── LICENSE
└── README.md            # This file
```

## Technologies Used

- Backend:
    - .NET 8 with N-tier architecture
    - MySQL database
- Frontend:
    - Angular 18
    - NG Zorro UI library

## Getting Started

For detailed setup and running instructions, please refer to the README files in the [`backend`](command:_github.copilot.openRelativePath?%5B%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2FUsers%2Falptalhayazar%2FRiderProjects%2FQuoteRequestSystem%2Fbackend%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%5D "/Users/alptalhayazar/RiderProjects/QuoteRequestSystem/backend") and [`frontend`](command:_github.copilot.openRelativePath?%5B%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2FUsers%2Falptalhayazar%2FRiderProjects%2FQuoteRequestSystem%2Ffrontend%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%5D "/Users/alptalhayazar/RiderProjects/QuoteRequestSystem/frontend") directories.

## Features

## Docker Setup

This project includes Docker support for both the backend and frontend. You can use Docker Compose to build and run the entire application stack.

### Prerequisites

- Docker
- Docker Compose

### Running the Application

1. Build and start the containers:

    ```sh
    docker-compose up --build
    ```

2. Access the application:
    - Backend API: `http://localhost:5184`
    - Frontend: `http://localhost:4200`

### Stopping the Application

To stop the application, run:

```sh
docker-compose down
```

## Environment Variables
The following environment variables are used in the Docker setup:

ASPNETCORE_ENVIRONMENT: Set to Development for the backend.
ConnectionStrings__DefaultConnection: Connection string for the MySQL database.
API_URL: URL for the backend API used by the frontend.

## Features

- User registration and login
- Quote request submission
- Offer calculation based on package dimensions and shipping details
- Offer listing and management

## License

This project is licensed under the terms of the license file in the root directory.