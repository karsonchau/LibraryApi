using Library.Domain.Interfaces;

namespace Library.Domain.Entities;
public class ScoredBook(ILibraryBook book, int score)
{
    public ILibraryBook Book { get; } = book;
    public int Score { get; } = score;
}
