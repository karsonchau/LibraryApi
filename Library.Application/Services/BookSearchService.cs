using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Interfaces;

namespace Library.Application.Services;

public class BookSearchService : IBookSearchService
{
	private readonly IBookRepository bookRepository;
	private readonly IScoreStrategy scoreStrategy;

	public BookSearchService(IBookRepository bookRepository, IScoreStrategy scoreStrategy)
	{
		this.bookRepository = bookRepository;
		this.scoreStrategy = scoreStrategy;
	}

	public IList<ILibraryBook> SearchBooks(string? searchTerm, CancellationToken cancellationToken = default)
	{
		var books = bookRepository.GetAllBooks();

		if (string.IsNullOrWhiteSpace(searchTerm))
		{
			return books;
		}

		string lowerTerm = searchTerm.ToLowerInvariant();

		var results = books
			.AsParallel()
			.WithCancellation(cancellationToken)
			.Select(book =>
			{
				cancellationToken.ThrowIfCancellationRequested();
				int score = scoreStrategy.ComputeScore(book, searchTerm);
				return new ScoredBook(book, score);
			})
			.Where(scored => scored.Score > 0)
			.OrderByDescending(scored => scored.Score)
			.ThenBy(scored => scored.Book.Title, StringComparer.OrdinalIgnoreCase)
			.Select(scored => scored.Book)
			.ToList();
		return results;
	}
}
