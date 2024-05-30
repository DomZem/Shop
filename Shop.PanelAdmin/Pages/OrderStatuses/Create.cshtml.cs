using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp.Authenticators;
using RestSharp;
using Shop.Application.OrderStatuses.Commands.CreateOrderStatus;

namespace Shop.PanelAdmin.Pages.OrderStatuses
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateOrderStatusCommand OrderStatus { get; set; } = default!;

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
                BaseUrl = new Uri($"https://localhost:7270")
            };

            var client = new RestClient(options);
            var request = new RestRequest($"api/orderStatuses");
            request.AddHeader("content-type", "application/json");
            request.AddBody(OrderStatus);
            var response = await client.ExecutePostAsync(request);

            if(response.IsSuccessful)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
