# Francisco Iturburu Daux Challenge

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue)
[![Deploy on Render](https://img.shields.io/badge/Deploy-Render-blue)](https://daux-challenge.onrender.com/login)

This project is a .NET 8 ASP.NET Core application built to handle a mock authentication challenge. The app is deployed on Render and accessible at [https://daux-challenge.onrender.com/login](https://daux-challenge.onrender.com/login).

## Project Structure

The solution consists of two main projects:

-   **Francisco_Iturburu_Daux_Challenge**: Contains the main application logic with controllers, views, and models.
-   **Services**: Contains helper classes, middlewares, interfaces, and services to support the application.

## Technologies Used

-   **ASP.NET Core 8.0**
-   **.NET 8**
-   **Docker** (for containerization and deployment)
-   **Render** (for hosting)

## Getting Started

### Prerequisites

-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   Optional: [Docker](https://www.docker.com/)

### Installation

1. **Clone the repository**:

    ```bash
    git clone https://github.com/yourusername/Daux_Challenge.git
    cd Daux_Challenge
    ```

2. **Restore dependencies**:

    ```bash
    dotnet restore
    ```

3. **Build the solution**:

    ```bash
    dotnet build
    ```

3. **Run the application**:

    ```bash
    dotnet run --project Francisco_Iturburu_Daux_Challenge
    ```

### Docker Setup

1.  **Build the Docker image**:

     ```bash
    docker build -t daux-challenge .
    ```

2.  **Run the container**:

    ```bash
    docker run -d -p 5143:5143 daux-challenge
    ```

### Configuration

The application requires an API base URL specified in appsettings.json or as an environment variable ApiBaseUrl.

```JSON
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ApiBaseUrl": "https://daux.com.ar/api"
}

```

## License

[MIT](https://choosealicense.com/licenses/mit/)
