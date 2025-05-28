using Library.Infrastructure.Repository;
using System;

namespace Library.Tests.Infrastructure;

public class BookRepositoryTests
{
	[Fact]
	public void GetAllBooks_ReturnsNonNullList()
	{
		var repo = new BookRepository();

		var books = repo.GetAllBooks();

		Assert.NotNull(books);
		Assert.All(books, book => Assert.NotNull(book));
	}
}
