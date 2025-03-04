# BankingApp

Backend for a simple banking application built with ASP.NET Core.

To run the application, first, clone the repository using `git clone https://github.com/Sanduni-Abeysinghe/BankingApp.git` and navigate into the project directory with `cd BankingApp`. Restore dependencies with `dotnet restore`, build the project using `dotnet build`, and then start the application with `dotnet run`. The app will be available at **http://localhost:5000** (HTTP) or **https://localhost:5001** (HTTPS).

To run the application using Docker, build the Docker image with `docker build -t bankingapp .` and start a container using `docker run -p 8080:80 bankingapp`. The application can then be accessed at **http://localhost:8080**.
