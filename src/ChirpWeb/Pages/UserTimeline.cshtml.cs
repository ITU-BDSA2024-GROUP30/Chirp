using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChirpCore.DTOs;


namespace ChirpWeb.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepService _service;
    public required List<CheepDTO> Cheeps { get; set; }
    public int currentPage;

    public UserTimelineModel(ICheepService service)
    {
        _service = service;
    }

    public ActionResult OnGet(string author, int pageNumber = 1)
    {
        currentPage = pageNumber;
        Cheeps = _service.GetCheepsFromAuthor(author, currentPage);
        if (currentPage < 1)
        {
            currentPage = 1;
        }
        return Page();
    }
}
