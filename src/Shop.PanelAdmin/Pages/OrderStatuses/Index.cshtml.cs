using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using Shop.Application.OrderStatuses.Dtos;
using Shop.PanelAdmin.Config;

namespace Shop.PanelAdmin.Pages.OrderStatuses
{
    public class IndexModel(IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        public List<OrderStatusDto> OrderStatuses { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
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
            var request = new RestRequest("/api/orderStatuses");
            request.AddHeader("content-type", "application/json");
            var response = await client.ExecuteGetAsync<List<OrderStatusDto>>(request);

            if(!response.IsSuccessStatusCode || response.Data == null)
            {
                return BadRequest();
            }

            OrderStatuses = response.Data;

            return Page();
        }
    }
}
