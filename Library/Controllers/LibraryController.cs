using Library.Application.Interfaces;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class LibraryController: ControllerBase
{
	private readonly IBookSearchService libraryService;
	public LibraryController(IBookSearchService libraryService)
	{
		this.libraryService = libraryService;
	}

	[HttpGet(Name = "Search library")]
	public ActionResult<IEnumerable<ILibraryBook>> Get(string? searchTerm, CancellationToken cancellationToken)
	{
		try
		{
			var books = libraryService.SearchBooks(searchTerm);
			if (books == null || !books.Any())
			{
				return NotFound($"No books found matching '{searchTerm}'.");
			} 
			return Ok(books);
		}
		catch (OperationCanceledException)
		{
			return StatusCode(StatusCodes.Status204NoContent);
		}
		catch (Exception _)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
		}
	}
}
