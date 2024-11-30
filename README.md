# FizzBuzz Game API

## Project Overview

The **FizzBuzz Game API** is a web application that allows users to create and play a customizable FizzBuzz-like game. Users can set their own rules, including the numbers and strings used for the game, and choose the range of numbers to play with. The backend is built with ASP.NET Web API, while the frontend utilizes React with TypeScript from Vite.

---

## Features

- Create customizable FizzBuzz games with user-defined rules.
- Players can enter their answers and get immediate feedback score.

---

## Technologies Used

- **Frontend:** React, TypeScript, Vite, Axios, Bootstrap
- **Backend:** ASP.NET Web API, Entity Framework
- **Database:** SQL Server
- **Development Tools:** Docker, Visual Studio, Node.js

---

## Setup

### Prerequisites

- .NET 8.0 SDK installed.
- SQL Server with a valid database configured based on the provided template `DB_Schema_Script.sql` in the `backend` folder.
- Node.js (with npm or yarn) installed for the `frontend`.

### Environment Configuration

1. Create an `appsettings.json` file in the project directory of `backend` with the provided in the `appsettings.json.example` to match your database connection strings.
2. Create a `.env` file in the project directory of `frontend` with the format provided in the `.env.example` file to match your backend's API URL.

### Installation

1. Clone the repository.
2. Set up the environment configuration as described above.
3. Install the required packages in the project (e.g., `dotnet restore`, `npm install` or `yarn install`).

---

## Running The Program

1. Run the `backend` folder by targeting the `.csproj` file or the `.sln` file using your preferred method (e.g., `dotnet run` or your IDE's play button).
2. Run the `frontend` folder by your preferred method and dependencies (e.g., npm run dev or yarn dev)
3. Explore the web interface through the provided navigation.

---

## Future Improvements

- **Docker Compose** setup is incomplete.
- **Repository Architecture** for separation of concerns and better maintainability.
- **Unit and Integration Tests** with .NET are not yet implemented.
- **Additional Side Functionalities** such as modifying, deleting, viewing, or choosing existing games.
