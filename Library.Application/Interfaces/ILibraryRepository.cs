using Library.Domain.Interfaces;

namespace Library.Application.Interfaces;
public interface ILibraryRepository
{
    IList<ILibraryBook> GetAllBooks();
}
