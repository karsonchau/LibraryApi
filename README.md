# 📚 Library Search API (.NET 8)

This is a simple ASP.NET Core Web API built with .NET 8. It allows clients to search for books using basic scoring rules based on title, author, or ISBN.

---

## 🔧 Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/LibraryApi.git
cd LibraryApi
```

### 2. Run the API
```bash
dotnet run --project LibraryApi
```

The API will be available at:

https://localhost:7291 (HTTPS)

http://localhost:5096 (HTTP)

Note: The ports may vary depending on your launch profile.

### API Usage
## Search for Books
Send a GET request to the /Library endpoint with a searchTerm query parameter.

Example Request
```http
GET /Library?searchTerm=dune
```

Example Response (200 OK)
```json
[
  {
    "bookId": "00000000-0000-0000-0000-000000000000",
    "title": "Dune",
    "author": "Frank Herbert",
    "category": 4,
    "isbn": "978-962-217-644-7",
    "publishedDate": "1932-04-22T00:00:00",
    "lentToCustomerId": "CUST005",
    "dueDate": "2025-05-06T16:00:25.3542974-07:00"
  },
]
```

## Other Possible Responses
- `404 Not Found` — No books matched the search term.
- `204 No Content` — Search was canceled (e.g., via `CancellationToken`).
- `500 Internal Server Error` — An unhandled error occurred on the server.

### Running Unit Tests
The tests are written using xUnit and Moq. To run all tests in the solution, execute the following command:
```bash
dotnet test
```