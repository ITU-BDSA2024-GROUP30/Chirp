using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using ChirpCore.Domain;
using ChirpRepositories;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ChirpWeb.Pages
{
    public class CreateCheepModel : PageModel
    {
        private readonly ICheepRepository _repository;
        private readonly UserManager<Author> _userManager;

        public CreateCheepModel(ICheepRepository repository, UserManager<Author> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        [BindProperty]
        [Required(ErrorMessage = "Please enter a message for your Cheep.")]
        [StringLength(280, ErrorMessage = "Cheep cannot exceed 280 characters.")]
        public string CheepText { get; set; }

        //currently chat
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                int userId = 0;
                string userName = "Anonymous";

                if (User.Identity.IsAuthenticated)
                {
                    userId = int.Parse(User.FindFirst("sub")?.Value ?? "0");
                    userName = User.Identity.Name ?? "Anonymous";
                }

                Console.WriteLine($"Attempting to create Cheep by userId: {userId}, userName: {userName}");
                Author author = await _userManager.FindByIdAsync(userId.ToString()) ?? await _userManager.FindByNameAsync(userName);
                // Create the Cheep object
                var newCheep = new Cheep
                {
                    CheepId = await _repository.GenerateNextCheepIdAsync(),
                    Text = CheepText,
                    TimeStamp = DateTime.UtcNow,
                    Author = author
                };

                // Save the Cheep using the repository
                await _repository.AddCheepAsync(newCheep);

                Console.WriteLine("Cheep created successfully!");

                return RedirectToPage("/Index"); // Redirect after success
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while submitting your Cheep.");
                return Page();
            }
        }

        /*public async Task<IActionResult> OnPostAsync()
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
                //userId = int.Parse(User.Identity.Name);
                userName = User.Identity.Name; // Or use User.Claims for more specific handling
            }

            try
            {
                // Call the repository to create the new Cheep
                //int cheepId = await _repository.CreateCheepAsync(userId, Text);
                await _repository.CreateCheepAsync(userId, userName, CheepText);
                return RedirectToPage("/Timeline"); // Redirect to the homepage

            }
            catch (Exception ex)
            {
                //ErrorMessage = $"Error creating Cheep: {ex.Message}";
                ModelState.AddModelError(string.Empty, $"Error creating Cheep: {ex.Message}");
                return Page();
            }
        }*/

    }
}