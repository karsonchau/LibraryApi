using Library.Domain.Interfaces;

namespace Library.Domain.Entities
{
	public class LibraryBook : ILibraryBook
	{
		public Guid BookId { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public LibraryBookCategory Category { get; set; }
		public string Isbn { get; set; }
		public DateTime PublishedDate { get; set; }
		public string LentToCustomerId { get; set; }
		public DateTime? DueDate { get; set; }
	}
}
