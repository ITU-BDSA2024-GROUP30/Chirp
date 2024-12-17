using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChirpCore.DTOs;
using ChirpServices;

using Microsoft.AspNetCore.Identity;
using ChirpCore.Domain;


namespace ChirpWeb.Pages;

public class AboutMeModel : PageModel
{
	private readonly ICheepService _CheepService;
	private readonly IAuthorService _AuthorService;
	public required List<CheepDTO> Cheeps { get; set; }
    public required List<string> Follows { get; set; }
	public int currentPage;
	public readonly SignInManager<Author> _signInManager;
	public AboutMeModel(ICheepService CheepService, IAuthorService AuthorService, SignInManager<Author> signInManager)
	{
		_CheepService = CheepService;
		_AuthorService = AuthorService;
		_signInManager = signInManager;
	}

	public Boolean IsLoggedIn()
	{
		Boolean IsLoggedIn = _signInManager.IsSignedIn(User);
		
		return IsLoggedIn;
	}

	public string GetLoggedInUser()
	{
		if (User.Identity?.Name! == null)
		{
			throw new ArgumentNullException(User.Identity?.Name);
		}
		return User.Identity.Name;
	}

	public async Task<ActionResult> OnGetAsync()
	{
		currentPage = 1;
		Cheeps = await _CheepService.GetCheepsFromAuthorAsync(GetLoggedInUser(), currentPage);
		Follows = await _AuthorService.ReturnFollowListAsync(GetLoggedInUser());

		if (currentPage < 1)
		{
			currentPage = 1;
		}
		return Page();
	}

	public async Task<IActionResult> OnPostAsync(string Username) {
		var WasForgettingOfCheepsSuccessful = await _CheepService.ForgetCheepsAsync(GetLoggedInUser());
		
		if (!WasForgettingOfCheepsSuccessful){
			Console.WriteLine("Unable to forget user cheeps! Try again");
		}

		var WasForgettingOfAuthorSuccessful = await _AuthorService.ForgetAuthorAsync(GetLoggedInUser());
		if (!WasForgettingOfCheepsSuccessful){
			Console.WriteLine("Unable to forget user! Try again");
		}

		await _signInManager.SignOutAsync();

		return Redirect("~/");
		
	}

}
