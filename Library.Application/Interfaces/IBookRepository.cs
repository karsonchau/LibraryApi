using Library.Domain.Interfaces;

namespace Library.Application.Interfaces
{
	public interface IBookRepository
	{
		IList<ILibraryBook> GetAllBooks();
	}
}
