# 📚 Library Search API (.NET 8)

This is a simple ASP.NET Core Web API built with .NET 8. It allows clients to search for books using basic scoring rules based on title, author, or ISBN.

---

## 🔧 Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/library-search-api.git
cd library-search-api
```bash

### 2. Clone the Repository
dotnet run --project LibraryApi

The API will be available at:

https://localhost:7291 (HTTPS)

http://localhost:5096 (HTTP)

Note: The ports may vary depending on your launch profile.

API Usage
Search for Books
Send a GET request to the /Library endpoint with a searchTerm query parameter.

Example Request

GET /Library?searchTerm=tolkien

Example Response (200 OK)
[
  {
    "title": "The Hobbit",
    "author": "J.R.R. Tolkien",
    "isbn": "9780261103344"
  }
]
Other Possible Responses
404 Not Found — No books matched the search term.

204 No Content — Search was canceled (e.g., via CancellationToken).

500 Internal Server Error — An unhandled error occurred on the server.

Running Unit Tests
The tests are written using xUnit and Moq. To run all tests in the solution, execute the following command:
dotnet test