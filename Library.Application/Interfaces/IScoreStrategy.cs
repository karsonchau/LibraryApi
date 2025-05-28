using Library.Domain.Interfaces;

namespace Library.Application.Interfaces;

public interface IScoreStrategy
{
	int ComputeScore(ILibraryBook book, string searchTerm);
}