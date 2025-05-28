using System;
using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Domain.Interfaces;
using Moq;

namespace Library.Tests.Application;

public class BookSearchServiceTests
{
    private readonly Mock<IBookRepository> _bookRepoMock = new();
    private readonly Mock<IScoreStrategy> _scoreStrategyMock = new();

    private BookSearchService CreateServiceWithBooks(params ILibraryBook[] books)
    {
        _bookRepoMock.Setup(repo => repo.GetAllBooks()).Returns(books.ToList());
        return new BookSearchService(_bookRepoMock.Object, _scoreStrategyMock.Object);
    }

    [Fact]
    public void SearchBooks_EmptySearchTerm_ReturnsAllBooks()
    {
        var books = new[] { MockBook("Book A"), MockBook("Book B") };
        var service = CreateServiceWithBooks(books);

        var results = service.SearchBooks("");

        Assert.Equal(books, results);
    }

	[Fact]
	public void SearchBooks_NullSearchTerm_ReturnsAllBooks()
	{
		var books = new[] { MockBook("Book A"), MockBook("Book B") };
		var service = CreateServiceWithBooks(books);

		var results = service.SearchBooks(null);

		Assert.Equal(books, results);
	}

	[Fact]
	public void SearchBooks_WhiteSpaceSearchTerm_ReturnsAllBooks()
	{
		var books = new[] { MockBook("Book A"), MockBook("Book B") };
		var service = CreateServiceWithBooks(books);

		var results = service.SearchBooks(" ");

		Assert.Equal(books, results);
	}

	[Fact]
    public void SearchBooks_FiltersOutZeroScoreBooks()
    {
        var book1 = MockBook("Book A");
        var book2 = MockBook("Book B");

        var service = CreateServiceWithBooks(book1, book2);
        _scoreStrategyMock.Setup(s => s.ComputeScore(book1, "query")).Returns(0);
        _scoreStrategyMock.Setup(s => s.ComputeScore(book2, "query")).Returns(500);

        var results = service.SearchBooks("query");

        Assert.Single(results);
        Assert.Contains(book2, results);
    }

    [Fact]
    public void SearchBooks_SortsByScoreAndTitle()
    {
        var book1 = MockBook("Alpha");
        var book2 = MockBook("Beta");
        var book3 = MockBook("Beta");

        var service = CreateServiceWithBooks(book1, book2, book3);

        _scoreStrategyMock.Setup(s => s.ComputeScore(book1, "query")).Returns(500);
        _scoreStrategyMock.Setup(s => s.ComputeScore(book2, "query")).Returns(1000);
        _scoreStrategyMock.Setup(s => s.ComputeScore(book3, "query")).Returns(1000);

        var results = service.SearchBooks("query");

        Assert.Equal(new[] { book2, book3, book1 }.OrderByDescending(b => _scoreStrategyMock.Object.ComputeScore(b, "query"))
            .ThenBy(b => b.Title, StringComparer.OrdinalIgnoreCase), results);
    }

    [Fact]
    public void SearchBooks_SameScore_SortsAlphabeticallyByTitle()
    {
        var bookA = MockBook("Alpha");
        var bookC = MockBook("Charlie");
        var bookB = MockBook("Bravo");

        var service = CreateServiceWithBooks(bookA, bookC, bookB);

        _scoreStrategyMock.Setup(s => s.ComputeScore(It.IsAny<ILibraryBook>(), "query")).Returns(100);

        var results = service.SearchBooks("query");

        var expectedOrder = new[] { bookA, bookB, bookC };
        Assert.Equal(expectedOrder.Select(b => b.Title), results.Select(b => b.Title));
    }

    [Fact]
    public void SearchBooks_ThrowsIfCancelled()
    {
        var book = MockBook("Some Book");
        var service = CreateServiceWithBooks(book);

        _scoreStrategyMock.Setup(s => s.ComputeScore(It.IsAny<ILibraryBook>(), It.IsAny<string>())).Returns(100);

        using var cts = new CancellationTokenSource();
        cts.Cancel();

        Assert.Throws<OperationCanceledException>(() =>
        {
            service.SearchBooks("test", cts.Token);
        });
    }

    private ILibraryBook MockBook(string title, string author = "Author", string isbn = "123")
    {
        var mock = new Mock<ILibraryBook>();
        mock.SetupGet(b => b.Title).Returns(title);
        mock.SetupGet(b => b.Author).Returns(author);
        mock.SetupGet(b => b.Isbn).Returns(isbn);
        return mock.Object;
    }
}
