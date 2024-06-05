using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp.Authenticators;
using RestSharp;
using Shop.Application.OrderStatuses.Dtos;
using Microsoft.Extensions.Options;
using Shop.PanelAdmin.Config;

namespace Shop.PanelAdmin.Pages.OrderStatuses
{
    public class DetailsModel(IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        public OrderStatusDto OrderStatus { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var token = HttpContext.Session.GetString("AuthToken");

            if(token == null)
            {
                return Unauthorized();
            }
            
            var options = new RestClientOptions()
            {
                Authenticator = new JwtAuthenticator(token),
                BaseUrl = new Uri(shopAPIConfig.Value.URL)
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/orderStatuses/{id}");
            request.AddHeader("content-type", "application/json");
            var response = await client.ExecuteGetAsync<OrderStatusDto>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                return NotFound();
            }

            OrderStatus = response.Data;

            return Page();
        }
    }
}
