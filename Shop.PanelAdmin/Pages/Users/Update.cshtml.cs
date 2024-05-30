using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using RestSharp.Authenticators;
using Shop.Application.Users.Commands.UpdateUser;
using Shop.Application.Users.Dtos;

namespace Shop.PanelAdmin.Pages.Users
{
    public class UpdateModel(ILogger<UpdateModel> logger) : PageModel
    {
        [BindProperty]
        public UpdateUserCommand User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var options = new RestClientOptions()
            {
                BaseUrl = new Uri($"https://localhost:7270")
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/identity/users/{id}");
            request.AddHeader("content-type", "application/json");
            var response = await client.ExecuteGetAsync<UserDto>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                return NotFound();
            }

            var user = response.Data;

            User = new UpdateUserCommand()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var token = HttpContext.Session.GetString("AuthToken");

            if (token == null)
            {
                return Unauthorized();
            }
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var options = new RestClientOptions()
            {
                Authenticator = new JwtAuthenticator(token),
                BaseUrl = new Uri($"https://localhost:7270")
            };

            var client = new RestClient(options);
            var request = new RestRequest($"api/identity/users/{id}");
            request.AddHeader("content-type", "application/json");
            request.AddBody(User);
            var response = await client.ExecutePutAsync(request);

            if (response.IsSuccessful)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
