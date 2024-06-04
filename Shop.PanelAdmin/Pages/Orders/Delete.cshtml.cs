using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp.Authenticators;
using RestSharp;
using Shop.Application.Orders.Dtos;

namespace Shop.PanelAdmin.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public OrderDetailsDto Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var token = HttpContext.Session.GetString("AuthToken");

            if (token == null)
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            var options = new RestClientOptions()
            {
                Authenticator = new JwtAuthenticator(token),
                BaseUrl = new Uri($"https://localhost:7270")
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/orders/{id}");
            request.AddHeader("content-type", "application/json");
            var response = await client.ExecuteGetAsync<OrderDetailsDto>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                return NotFound();
            }

            Order = response.Data;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
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
                BaseUrl = new Uri($"https://localhost:7270")
            };

            var client = new RestClient(options);
            var request = new RestRequest($"api/orders/{id}");
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