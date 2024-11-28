using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using ChirpCore.Domain;
using ChirpRepositories;

namespace ChirpWeb.Pages
{
    public class CreateCheepModel : PageModel
    {
        private readonly ICheepRepository _cheepRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        [BindProperty]
        public string CheepText { get; set; } = string.Empty;

        public CreateCheepModel(ICheepRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _cheepRepository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            // Mb initialise properties or handle logic for GET request
        }

        public async Task<IActionResult> OnPostAsync()
        {
            /*if (string.IsNullOrWhiteSpace(CheepText))
            {
                ModelState.AddModelError(string.Empty, "Message cannot be empty.");
                return Page();
            }

            // Get logged-in userID
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            // Create Cheep using the repository
            await _repository.CreateCheep(int.Parse(userId), CheepText);

            // Redirect to a different page after posting
            return RedirectToPage("/Index"); // Adjust maybe*/
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string userId = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
            string userName;

            if (userId != null)
            {
                // User is logged in
                userName = _httpContextAccessor.HttpContext.User.Identity?.Name ?? "Anonymous";
            }
            else
            {
                // User is anonymous
                userId = "0"; // Use a default ID for anonymous users (or handle this differently)
                userName = "Anonymous";
            }

            await _cheepRepository.CreateCheep(int.Parse(userId), userName, CheepText);

            return RedirectToPage("/Index");
        }

    }
}

