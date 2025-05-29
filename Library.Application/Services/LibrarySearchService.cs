using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Interfaces;

namespace Library.Application.Services;

public class LibrarySearchService(ILibraryRepository libraryRepository, IScoreStrategy scoreStrategy)
	: ILibrarySearchService
{
	public IList<ILibraryBook> SearchBooks(string? searchTerm, CancellationToken cancellationToken = default)
	{
		var books = libraryRepository.GetAllBooks();

		if (string.IsNullOrWhiteSpace(searchTerm))
		{
			return books;
		}
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
