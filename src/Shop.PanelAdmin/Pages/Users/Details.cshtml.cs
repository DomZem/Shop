using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RestSharp;
using Shop.Application.Users.Dtos;
using Shop.PanelAdmin.Config;

namespace Shop.PanelAdmin.Pages.Users
{
    public class DetailsModel(IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
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
    }
}
