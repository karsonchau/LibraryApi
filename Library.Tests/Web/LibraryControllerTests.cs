using Library.Application.Interfaces;
using Library.Web.Controllers;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Library.Tests.Web;

public class LibraryControllerTests
{
	private readonly Mock<ILibrarySearchService> _serviceMock = new();
	private readonly Mock<ILogger<LibraryController>> _loggerMock = new();
	private readonly LibraryController _controller;

	public LibraryControllerTests()
	{
		_controller = new LibraryController(_serviceMock.Object, _loggerMock.Object);
	}

	[Fact]
	public void Get_WithResults_ReturnsOk()
	{
		var books = new List<ILibraryBook> { MockBook("Test") };
		_serviceMock.Setup(s => s.SearchBooks("query", It.IsAny<CancellationToken>())).Returns(books);

		var result = _controller.Get("query", default);

		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		Assert.Equal(books, okResult.Value);
	}

	[Fact]
	public void Get_WithNoResults_ReturnsNotFound()
	{
		_serviceMock.Setup(s => s.SearchBooks("query", It.IsAny<CancellationToken>())).Returns(new List<ILibraryBook>());

		var result = _controller.Get("query", default);

		Assert.IsType<NotFoundObjectResult>(result.Result);
	}

	[Fact]
	public void Get_WhenCancelled_ReturnsNoContent()
	{
		_serviceMock.Setup(s => s.SearchBooks(It.IsAny<string>(), It.IsAny<CancellationToken>())).Throws<OperationCanceledException>();

		var result = _controller.Get("query", new CancellationToken(true));

		Assert.IsType<StatusCodeResult>(result.Result);
		Assert.Equal(StatusCodes.Status204NoContent, ((StatusCodeResult)result.Result).StatusCode);
	}

	[Fact]
	public void Get_WhenExceptionThrown_ReturnsInternalServerError()
	{
		_serviceMock.Setup(s => s.SearchBooks(It.IsAny<string>(), It.IsAny<CancellationToken>())).Throws<Exception>();

		var result = _controller.Get("query", default);

		var statusResult = Assert.IsType<ObjectResult>(result.Result);
		Assert.Equal(StatusCodes.Status500InternalServerError, statusResult.StatusCode);
	}

	private ILibraryBook MockBook(string title)
	{
		var mock = new Mock<ILibraryBook>();
		mock.SetupGet(b => b.Title).Returns(title);
		return mock.Object;
	}
}
