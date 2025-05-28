using Library.Domain.Interfaces;

namespace Library.Domain.Entities
{
	public class ScoredBook
	{
		public ILibraryBook Book { get; }
		public int Score { get; }

		public ScoredBook(ILibraryBook book, int score)
		{
			Book = book;
			Score = score;
		}
	}
}
