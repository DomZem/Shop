using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp.Authenticators;
using RestSharp;
using Shop.Application.Users.Commands.CreateUser;
using Microsoft.Extensions.Options;
using Shop.PanelAdmin.Config;

namespace Shop.PanelAdmin.Pages.Users
{
    public class CreateModel(IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        [BindProperty]
        public CreateUserCommand User { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
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
                BaseUrl = new Uri(shopAPIConfig.Value.URL)
            };

            var client = new RestClient(options);
            var request = new RestRequest("/api/users");
            request.AddHeader("content-type", "application/json");
            request.AddBody(User);
            var response = await client.ExecutePostAsync(request);

            if (response.IsSuccessful)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
