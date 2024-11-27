using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChirpCore.DTOs;
using Microsoft.AspNetCore.Identity;
using ChirpCore.Domain;

namespace ChirpWeb.Pages;

public class PublicModel : PageModel
{
	private readonly SignInManager<Author> _signInManager;
	private readonly ICheepService _CheepService;
	private readonly IAuthorService _AuthorService;
	public required List<CheepDTO> Cheeps { get; set; }
	public int currentPage;
	public string? LoggedInAuthorUsername;

	public PublicModel(ICheepService CheepService, IAuthorService AuthorService)
	{
		_CheepService = CheepService;
		_AuthorService = AuthorService;
		IsLoggedIn();
	}

	public void IsLoggedIn()
	{
		bool IsLoggedIn = ChirpWeb.Areas.Identity.Pages.Login.SignInManager.IsSignedIn(User);
		if (IsLoggedIn)
		{
			LoggedInAuthorUsername = User.Identity.Name;
		}
	}

	public void FollowAuthorClick(string AuthorToFollowUsername)
	{
		_AuthorService.FollowAuthor(LoggedInAuthorUsername, AuthorToFollowUsername);
	}

	public void UnfollowAuthor(string AuthorToUnfollowUsername)
	{
		_AuthorService.UnfollowAuthor(LoggedInAuthorUsername, AuthorToUnfollowUsername);
	}

	public bool IsFollowing(string AuthorToFollowUnfollowUsername)
	{
		_AuthorService.IsFollowing(LoggedInAuthorUsername, AuthorToFollowUnfollowUsername);
	}

	public ActionResult OnGet(int pageNumber = 1)
	{
		currentPage = pageNumber;
		Cheeps = _CheepService.GetCheeps(currentPage);
		if (currentPage < 1)
		{
			currentPage = 1;
		}

		return Page();

	}


}