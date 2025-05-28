using Library.Domain.Interfaces;

namespace Library.Application.Interfaces
{
	public interface IBookSearchService
	{
		IList<ILibraryBook> SearchBooks(string? searchTerm, CancellationToken cancellationToken = default);
	}
}
