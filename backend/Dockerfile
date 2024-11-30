# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the solution file if applicable
COPY *.sln .  

# Copy csproj and restore as distinct layers
COPY ./backend/FizzBuzzAPI/*.csproj ./FizzBuzzAPI/
RUN dotnet restore ./FizzBuzzAPI/FizzBuzzAPI.csproj

# Copy the entire backend directory
COPY ./backend/FizzBuzzAPI/ ./FizzBuzzAPI/ 

# Build the application
RUN dotnet publish ./FizzBuzzAPI/FizzBuzzAPI.csproj -c Release -o /app/publish

# Use the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

# Copy the published output
COPY --from=build /app/publish .  

# Expose the port the app runs on
EXPOSE 8080 

# Start the application
ENTRYPOINT ["dotnet", "FizzBuzzAPI.dll"]  