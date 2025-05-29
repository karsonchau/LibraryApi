using Library.Application.Interfaces;
using Library.Domain.Interfaces;

namespace Library.Infrastructure.Repository;
public class LibraryRepository : ILibraryRepository
{
    public IList<ILibraryBook> GetAllBooks()
    {
        var libraryData = new LibraryData();
        return libraryData.Books;
    }
}
