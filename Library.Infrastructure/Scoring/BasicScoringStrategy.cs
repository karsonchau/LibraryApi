using Library.Application.Interfaces;
using Library.Domain.Interfaces;

namespace Library.Infrastructure.Scoring
{
	public class BasicScoringStrategy : IScoreStrategy
	{
		public int ComputeScore(ILibraryBook book, string searchTerm)
		{
			var term = searchTerm.ToLowerInvariant();
			var title = book.Title?.ToLowerInvariant() ?? "";
			var author = book.Author?.ToLowerInvariant() ?? "";
			var isbn = book.Isbn?.ToLowerInvariant() ?? "";

			if (title == term) return 1000;
			if (title.Contains(term)) return 500;
			if (author.Contains(term)) return 100;
			if (isbn.Contains(term)) return 50;

			return 0;
		}
	}
}
