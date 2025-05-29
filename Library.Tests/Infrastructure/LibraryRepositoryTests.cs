using Library.Infrastructure.Repository;

namespace Library.Tests.Infrastructure;

public class LibraryRepositoryTests
{
	[Fact]
	public void GetAllBooks_ReturnsNonNullList()
	{
		var repo = new LibraryRepository();

		var books = repo.GetAllBooks();

		Assert.NotNull(books);
		Assert.All(books, book => Assert.NotNull(book));
	}
}
