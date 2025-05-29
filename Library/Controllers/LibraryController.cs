using Library.Application.Interfaces;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class LibraryController(ILibrarySearchService libraryService, ILogger<LibraryController> logger) : ControllerBase
{
	[HttpGet(Name = "Search library")]
	public ActionResult<IEnumerable<ILibraryBook>> Get(string? searchTerm, CancellationToken cancellationToken)
	{
		try
		{
			var books = libraryService.SearchBooks(searchTerm, cancellationToken);
			if (books == null || !books.Any())
			{
				return NotFound($"No books found matching '{searchTerm}'.");
			} 
			return Ok(books);
		}
		catch (OperationCanceledException operationCanceledException)
		{
			logger.LogWarning(operationCanceledException, "Request was canceled.");
			return StatusCode(StatusCodes.Status204NoContent);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An unexpected error occurred while searching for books.");
			return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
		}
	}
}
