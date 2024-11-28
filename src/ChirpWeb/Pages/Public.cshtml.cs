using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChirpCore.DTOs;
using Microsoft.AspNetCore.Identity;
using ChirpCore.Domain;
using ChirpServices;

namespace ChirpWeb.Pages;

public class PublicModel : PageModel
{
	private readonly SignInManager<Author> _signInManager;
	private readonly ICheepService _CheepService;
	private readonly IAuthorService _AuthorService;
	public required List<CheepDTO> Cheeps { get; set; }
	public int currentPage;
	required public string LoggedInAuthorUsername;

	public PublicModel(
							IAuthorService AuthorService,
							ICheepService CheepService,
			SignInManager<Author> signInManager)
	{
		_AuthorService = AuthorService;
		_CheepService = CheepService;
		_signInManager = signInManager;
	}
	/*public PublicModel(ICheepService CheepService, IAuthorService AuthorService)
	{
		_CheepService = CheepService;
		_AuthorService = AuthorService;
		IsLoggedIn();
	}*/

	public Boolean IsLoggedIn()
	{
		Boolean IsLoggedIn = _signInManager.IsSignedIn(User);
		if (IsLoggedIn)
		{
			LoggedInAuthorUsername = User.Identity.Name;
			
		}
		return IsLoggedIn;
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
		return _AuthorService.IsFollowing(LoggedInAuthorUsername, AuthorToFollowUnfollowUsername);
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