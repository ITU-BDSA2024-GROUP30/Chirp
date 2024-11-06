using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.UserFacade.Chirp.Infrastructure.Chirp.Services;

namespace Chirp.src.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepObject> Cheeps { get; set; }

    public PublicModel(ICheepService service)
    {
        _service = service;
    }

    public ActionResult OnGet()
    {
        Cheeps = _service.GetCheeps();
        return Page();
    }
}
