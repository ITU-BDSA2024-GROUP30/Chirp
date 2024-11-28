using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChirpCore.DTOs;
using ChirpServices;

namespace ChirpWeb.Pages;

public class UserTimelineModel : PageModel
{
	private readonly ICheepService _CheepService;
	private readonly IAuthorService _AuthorService;
	public required List<CheepDTO> Cheeps { get; set; }
	public int currentPage;

	public UserTimelineModel(ICheepService CheepService, IAuthorService AuthorService)
	{
		_CheepService = CheepService;
		_AuthorService = AuthorService;
	}

	public ActionResult OnGet(string author, int pageNumber = 1)
	{
		currentPage = pageNumber;
		Cheeps = _CheepService.GetCheepsFromAuthor(author, currentPage);
		if (currentPage < 1)
		{
			currentPage = 1;
		}
		return Page();
	}

}
