# 🎬 Movie Management System

A web-based application for managing movies, directors, user favorites, and comments. Built using **ASP.NET Core**, **Entity Framework**, **SQL Server**, and **MongoDB**.

---

## 📌 Features

- 🔍 **Search & Filter** movies by title, genre, or director  
- 🧑‍💼 **Admin Panel** to add, update, and delete movies and directors  
- ⭐ **User Favorites**: Add or remove movies from your favorites list  
- 💬 **Comment System**: Users can leave comments on movies (stored in **MongoDB**)  
- 🔐 **Authentication & Authorization** using ASP.NET Identity  
- 📦 RESTful API integration for dynamic front-end and data exchange  

---

## 🧰 Tech Stack

| Technology       | Usage                             |
|------------------|-----------------------------------|
| ASP.NET Core     | Backend API and MVC architecture  |
| Entity Framework | ORM for SQL Server database       |
| SQL Server       | Relational database for core data |
| MongoDB          | NoSQL storage for user comments   |
| Bootstrap        | Responsive UI                     |
| JavaScript/jQuery| Frontend interaction              |
| Docker           | Local database containers         |

---


## 🖼️ Screenshots

### 🎥 1. Movie List Page

Shows the main movie list with search and filter functionality.

![Movie List](https://github.com/babek17/MovieManagement/blob/aa7896aebc1655c666275cb35d4ba38b23458f3b/movie-list.png)

---

### 📄 2. Movie Details Page

Displays full movie information, user comments, and the "Add to Favorites" button.

![Movie Details](https://github.com/babek17/MovieManagement/blob/2271a1a2f187d6053d2bab1f657b0d2c563998ed/movie-details-1.png)
![Movie Details](https://github.com/babek17/MovieManagement/blob/2271a1a2f187d6053d2bab1f657b0d2c563998ed/movie-details-2.png)


---

### ➕ 3. Add New Movie (Admin Panel)

Used by admins to create new movies, including title, genre, director, and image upload.

![Add Movie](https://github.com/babek17/MovieManagement/blob/2271a1a2f187d6053d2bab1f657b0d2c563998ed/add-movie-1.png)
![Add Movie](https://github.com/babek17/MovieManagement/blob/2271a1a2f187d6053d2bab1f657b0d2c563998ed/add-movie-2.png)


---

### 🎬 4. Add or Search Directors

Shows dynamic director search or modal to create a new director if not found.

![Director Search](https://github.com/babek17/MovieManagement/blob/2271a1a2f187d6053d2bab1f657b0d2c563998ed/add-director.png)

---

### ⭐ 5. Favorites Page

Displays the list of movies a user has added to their favorites.

![Favorites Page](https://github.com/babek17/MovieManagement/blob/2271a1a2f187d6053d2bab1f657b0d2c563998ed/watchlist.png)

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [Visual Studio or VS Code](https://visualstudio.microsoft.com/)
- [MongoDB Compass (optional)](https://www.mongodb.com/products/compass)

### Run Locally

```bash
# Clone the repository
git clone https://github.com/your-username/movie-management.git
cd movie-management

# Start SQL Server and MongoDB via Docker
docker-compose up -d

# Apply Entity Framework Core migrations
dotnet ef database update

# Run the project
dotnet run
