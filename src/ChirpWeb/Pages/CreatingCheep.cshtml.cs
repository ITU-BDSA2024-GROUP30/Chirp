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
            CheepText = "";
        }
        [BindProperty]
        [Required(ErrorMessage = "Please enter a message for your Cheep.")]
        [StringLength(160, ErrorMessage = "Cheep cannot exceed 160 characters.")]
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
                //Console.WriteLine($"Attempting to create Cheep by userId: {userId}, userName: {userName}");
                Author author = await _userManager.FindByNameAsync(User.Identity.Name);
                
								// Create the Cheep object (should be moved to CheepService and from there to CheepRepository)
                var newCheep = new Cheep
                {
                    CheepId = await _repository.GenerateNextCheepIdAsync(),
                    Text = CheepText,
                    TimeStamp = DateTime.Now,
                    Author = author
                };

                // Save the new Cheep using the CheepRepository (should be CheepService instead)
                await _repository.AddCheepAsync(newCheep);

                //After creating the cheep, redirect to Public Timeline
                return LocalRedirect("/");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while submitting your Cheep.");
                return Page();
            }
        }
    }
}