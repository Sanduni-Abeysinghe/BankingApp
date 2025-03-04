# Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY BankingApp.csproj . 
RUN dotnet restore "BankingApp.csproj"
COPY . . 
RUN dotnet build "BankingApp.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "BankingApp.csproj" -c Release -o /app/publish

# Final stage with the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published output from the previous stage
COPY --from=publish /app/publish .

# Set the entry point to the .dll file
ENTRYPOINT ["dotnet", "BankingApp.dll"]
