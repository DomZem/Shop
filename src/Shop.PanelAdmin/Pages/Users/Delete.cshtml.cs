using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using Shop.Application.Users.Dtos;
using Shop.PanelAdmin.Config;

namespace Shop.PanelAdmin.Pages.Users
{
    public class DeleteModel(IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        public UserDto User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var options = new RestClientOptions()
            {
                BaseUrl = new Uri(shopAPIConfig.Value.URL)
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/users/{id}");
            request.AddHeader("content-type", "application/json");
            var response = await client.ExecuteGetAsync<UserDto>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                return NotFound();
            }

            User = response.Data;

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

            var options = new RestClientOptions()
            {
                Authenticator = new JwtAuthenticator(token),
                BaseUrl = new Uri(shopAPIConfig.Value.URL)
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/users/{id}");
            request.AddHeader("content-type", "application/json");
            var response = await client.ExecuteDeleteAsync(request);

            if (response.IsSuccessful)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
