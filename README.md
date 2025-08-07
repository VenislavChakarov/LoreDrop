# LoreDrop

## Overview
LoreDrop is a web application built with ASP.NET Core 8.0 and Entity Framework Core. It provides a platform for managing and sharing lore, stories, or structured content. The project uses SQL Server as its database and includes user authentication and role management via ASP.NET Identity.

## Features
- User authentication and roles
- Content management
- Modular architecture (Data, Services, Web, ViewModels, Infrastructure)
- Seeded test users and admin

## Tech Stack
- **Backend:** ASP.NET Core 8.0
- **ORM:** Entity Framework Core 8
- **Database:** SQL Server (configurable)
- **Authentication:** ASP.NET Identity

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- SQL Server (local or remote)

### Setup
1. **Clone the repository:**
   ```bash
   git clone <your-repo-url>
   cd LoreDrop/LoreDrop
   ```
2. **Change the database connection string:**
   - Open `LoreDrop.Web/appsettings.json`.
   - Locate the `DefaultConnection` string under `ConnectionStrings`.
   - Replace the default values with your SQL Server details:
     ```json
     "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DB;User Id=YOUR_USER;Password=YOUR_PASSWORD;..."
     ```
   - **Important:** Do not use the default credentials in production.
3. **Apply database migrations:**
   ```bash
   dotnet ef database update --project LoreDrop.Web
   ```
4. **Run the application:**
   ```bash
   dotnet run --project LoreDrop.Web
   ```
5. **Access the app:**
   - Open your browser at [https://localhost:5001](https://localhost:5001) or the port shown in the console.

### Default Test Users
- User: `testuser@loredrop.com` / `TestUser123!`
- Admin: `admin@loredrop.com` / `Admin123!`

---

# LoreDrop (Български)

## Обща информация
LoreDrop е уеб приложение, изградено с ASP.NET Core 8.0 и Entity Framework Core. Проектът използва SQL Server за база данни и включва удостоверяване и управление на роли чрез ASP.NET Identity.

## Основни функции
- Удостоверяване и роли на потребители
- Управление на съдържание
- Модулна архитектура (Data, Services, Web, ViewModels, Infrastructure)
- Тестови потребители и администратор

## Технологии
- **Backend:** ASP.NET Core 8.0
- **ORM:** Entity Framework Core 8
- **База данни:** SQL Server (конфигурируема)
- **Удостоверяване:** ASP.NET Identity

## Начало

### Необходими инструменти
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- SQL Server (локален или отдалечен)

### Инсталация
1. **Клонирайте репозиторито:**
   ```bash
   git clone <your-repo-url>
   cd LoreDrop/LoreDrop
   ```
2. **Променете връзката към базата данни:**
   - Отворете `LoreDrop.Web/appsettings.json`.
   - Намерете `DefaultConnection` под `ConnectionStrings`.
   - Заменете стойностите с вашите данни за SQL Server:
     ```json
     "DefaultConnection": "Server=ВАШИЯТ_СЪРВЪР;Database=ВАШАТА_БД;User Id=ВАШИЯТ_ПОТРЕБИТЕЛ;Password=ВАШАТА_ПАРОЛА;..."
     ```
   - **Важно:** Не използвайте стандартните данни за достъп в продукция.
3. **Приложете миграциите:**
   ```bash
   dotnet ef database update --project LoreDrop.Web
   ```
4. **Стартирайте приложението:**
   ```bash
   dotnet run --project LoreDrop.Web
   ```
5. **Достъп до приложението:**
   - Отворете браузър на [https://localhost:5001](https://localhost:5001) или порта, показан в конзолата.

### Тестови потребители
- Потребител: `testuser@loredrop.com` / `TestUser123!`
- Админ: `admin@loredrop.com` / `Admin123!`