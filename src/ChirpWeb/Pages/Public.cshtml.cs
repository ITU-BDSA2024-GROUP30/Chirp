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
	public string? LoggedInAuthorUsername;

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

	public string GetLoggedInUser(){
		if(User.Identity.Name == null){
			throw new ArgumentNullException(User.Identity.Name);
		}
		return User.Identity.Name;
	}

	public async Task<ActionResult> OnPostAsync(string AuthorToFollowUsername)
	{
		if (!await IsFollowing(AuthorToFollowUsername)){
		await _AuthorService.FollowAuthor(GetLoggedInUser(), AuthorToFollowUsername);
		return RedirectToPage();
		} else {
		await _AuthorService.UnfollowAuthor(GetLoggedInUser(), AuthorToFollowUsername);
		return RedirectToPage();
		}
		
	}

	public async Task<Boolean> IsFollowing(string AuthorToFollowUnfollowUsername)
	{
		return await _AuthorService.IsFollowing(GetLoggedInUser(), AuthorToFollowUnfollowUsername);
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