# GPS Backend API (.NET 8)

This is a simple backend API built with .NET 8 that reads GPS tracking data from a JSON file and processes it. 
It calculates metrics like Maximum speed, VMG, and heart rate, then returns everything through an API.

---

## What this project does

- Reads GPS data from a JSON file
- Calculates:
  - Maximum Speed Over Ground (SOG)
  - Maximum Velocity Made Good (VMG)
  - Maximum Heart Rate (HR)
- Prepares data for charts
- Returns all results through an API

---

##  Project structure

Controllers/ → Handles API requests
Services/ → Contains logic (calculations, processing)
Repositories/ → Reads data from JSON file
DTOs/ → Data models
Configuration/ → App settings (file path, etc.)


---

## ⚙️ Setup

### Requirements
- .NET 8 SDK

### Run the project

```bash
dotnet run


