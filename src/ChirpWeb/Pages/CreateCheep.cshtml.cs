using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using ChirpCore.Domain;
using ChirpRepositories;

namespace ChirpWeb.Pages
{
    public class CreateCheepModel : PageModel
    {
        private readonly ICheepRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [BindProperty]
        public string CheepText { get; set; } = string.Empty;

        public CreateCheepModel(ICheepRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(CheepText))
            {
                ModelState.AddModelError(string.Empty, "Message cannot be empty.");
                return Page();
            }

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            await _repository.CreateCheep(int.Parse(userId), CheepText);

            return RedirectToPage("/Index"); // Adjust maybe
        }
    }
}
