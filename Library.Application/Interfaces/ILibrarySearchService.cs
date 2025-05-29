using Library.Domain.Interfaces;

namespace Library.Application.Interfaces;

public interface ILibrarySearchService
{
    IList<ILibraryBook> SearchBooks(string? searchTerm, CancellationToken cancellationToken = default);
}
