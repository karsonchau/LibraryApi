using Library.Infrastructure.Scoring;
using Moq;
using Library.Domain.Interfaces;

namespace Library.Tests.Infrastructure
{
    public class BasicScoringStrategyTests
    {
        private readonly BasicScoringStrategy _strategy = new();

        [Fact]
        public void ComputeScore_ExactTitleMatch_Returns1000()
        {
            var book = MockBook("The Hobbit", "J.R.R. Tolkien", "1234567890");
            var score = _strategy.ComputeScore(book.Object, "The Hobbit");

            Assert.Equal(1000, score);
        }

        [Fact]
        public void ComputeScore_TitleContainsTerm_Returns500()
        {
            var book = MockBook("The Hobbit", "J.R.R. Tolkien", "1234567890");
            var score = _strategy.ComputeScore(book.Object, "hobbit");

            Assert.Equal(500, score);
        }

        [Fact]
        public void ComputeScore_AuthorContainsTerm_Returns100()
        {
            var book = MockBook("Some Book", "J.R.R. Tolkien", "1234567890");
            var score = _strategy.ComputeScore(book.Object, "tolkien");

            Assert.Equal(100, score);
        }

        [Fact]
        public void ComputeScore_IsbnContainsTerm_Returns50()
        {
            var book = MockBook("Some Book", "Author Name", "1234567890");
            var score = _strategy.ComputeScore(book.Object, "456");

            Assert.Equal(50, score);
        }

        [Fact]
        public void ComputeScore_NoMatch_Returns0()
        {
            var book = MockBook("Some Book", "Author Name", "1234567890");
            var score = _strategy.ComputeScore(book.Object, "nonsense");

            Assert.Equal(0, score);
        }

        [Fact]
        public void ComputeScore_NullFields_DoesNotThrow()
        {
            var mock = MockBook(null, null, null);

            var score = _strategy.ComputeScore(mock.Object, "anything");

            Assert.Equal(0, score);
        }

        private Mock<ILibraryBook> MockBook(string title, string author, string isbn)
        {
            var mock = new Mock<ILibraryBook>();
            mock.SetupGet(b => b.Title).Returns(title);
            mock.SetupGet(b => b.Author).Returns(author);
            mock.SetupGet(b => b.Isbn).Returns(isbn);
            return mock;
        }
    }
}