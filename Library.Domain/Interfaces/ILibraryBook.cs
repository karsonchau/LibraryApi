using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
	public interface ILibraryBook
	{
		Guid BookId { get; set; }
		string Title { get; set; }
		string Author { get; set; }
		LibraryBookCategory Category { get; set; }
		string Isbn { get; set; }
		DateTime PublishedDate { get; set; }
		string LentToCustomerId { get; set; }
		DateTime? DueDate { get; set; }
	}
}
