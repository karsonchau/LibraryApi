using Library.Application.Interfaces;
using Library.Domain.Interfaces;

namespace Library.Infrastructure.Repository
{
	public class BookRepository : IBookRepository
	{
		public IList<ILibraryBook> GetAllBooks()
		{
			var libaryData = new LibraryData();
			return libaryData.Books;
		}
	}
}
