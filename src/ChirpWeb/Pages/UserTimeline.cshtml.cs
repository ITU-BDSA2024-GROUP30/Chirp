﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChirpCore.DTOs;
using ChirpServices;

using Microsoft.AspNetCore.Identity;
using ChirpCore.Domain;


namespace ChirpWeb.Pages;

public class UserTimelineModel : PageModel
{
	private readonly ICheepService _CheepService;
	private readonly IAuthorService _AuthorService;
	public required List<CheepDTO> Cheeps { get; set; }
	public int currentPage;
	public readonly SignInManager<Author> _signInManager;
	public UserTimelineModel(ICheepService CheepService, IAuthorService AuthorService, SignInManager<Author> signInManager)
	{
		_CheepService = CheepService;
		_AuthorService = AuthorService;
		_signInManager = signInManager;
	}

	public string GetLoggedInUser()
	{
		//var IsLoggedIn = _signInManager.IsSignedIn(User);
		if (User.Identity?.Name! == null)
		{
			throw new ArgumentNullException(User.Identity?.Name);
		}
		return User.Identity.Name;
	}

	public async Task<ActionResult> OnGetAsync(string author, int pageNumber = 1)
	{
		currentPage = pageNumber;
		if (!_signInManager.IsSignedIn(User))
		{
			Cheeps = await _CheepService.GetCheepsFromAuthorAsync(author, currentPage);

		}
		else if (GetLoggedInUser().Equals(author))
		{
			Cheeps = await _CheepService.GetCheepsFromOtherAuthorAsync(author, currentPage);
		}
		else
		{
			Cheeps = await _CheepService.GetCheepsFromAuthorAsync(author, currentPage);
		}
		if (currentPage < 1)
		{
			currentPage = 1;
		}
		return Page();
	}

	public async Task<IActionResult> OnPostAsync() {
		var user = await _CheepService.GetCheepsFromAuthorAsync(User.Identity?.Name!, currentPage);
		if (user == null) {
			return NotFound($"Unable to forget others that you are not logged in to");
		}

		var resultOfForgetingThese = await _CheepService.forgetThese(user.);
		var resultOfForgetingThese2 = await _AuthorService.;
		await _signInManager.SignOutAsync();

		return Redirect("~/");
	}

}
