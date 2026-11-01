This project is a small browser-based game prototype.

# Tech Stack
- C#
- Blazor WebAssembly
- ASP.NET Core Web API
- EF Core (SQLite)
- BackgroundService

# Architecture

The project is split into three main parts:

- `BlazorFrontend/` – client-side web application used to render the game UI and communicate with the API.
- `GameAPI/` – backend API responsible for authentication, game logic, database access, and background workers.
- `Shared/` – common DTOs and shared contracts used by both the frontend and backend.