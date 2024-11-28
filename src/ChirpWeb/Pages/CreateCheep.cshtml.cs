using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using ChirpCore.Domain;
using ChirpRepositories;
using System.ComponentModel.DataAnnotations;

namespace ChirpWeb.Pages
{
    public class CreateCheepModel : PageModel
    {
        private readonly ICheepRepository _repository;

        

        public CreateCheepModel(ICheepRepository repository)
        {
            _repository = repository;
        }
        [BindProperty]
        [Required(ErrorMessage = "Please enter a message for your Cheep.")]
        [StringLength(280, ErrorMessage = "Cheep cannot exceed 280 characters.")]
        public string CheepText { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {   
            // Validate input
            if (string.IsNullOrWhiteSpace(CheepText))
            {
                ModelState.AddModelError(string.Empty, "Message cannot be empty.");
                return Page();
            }

                // Get the user ID, or 0 for anonymous
            int userId = 0; // default for anonymous user
            string userName = "Anonymous";

            if (User.Identity.IsAuthenticated)
            {
                // Get the user ID if the user is authenticated (e.g., from a ClaimsPrincipal)
                userId = int.Parse(User.Identity.Name); // Assuming the user ID is stored as the Name claim
                userName = User.Identity.Name; // Or use User.Claims for more specific handling
            }

            try
            {
                // Call the repository to create the new Cheep
                //int cheepId = await _repository.CreateCheep(userId, Text);
                await _repository.CreateCheep(userId, userName, CheepText);
                return RedirectToPage("/Index"); // Redirect to the homepage or any page you want after creating the Cheep

            }
            catch (Exception ex)
            {
                //ErrorMessage = $"Error creating Cheep: {ex.Message}";
                ModelState.AddModelError(string.Empty, $"Error creating Cheep: {ex.Message}");
                return Page();
            }
        }

    }
}

