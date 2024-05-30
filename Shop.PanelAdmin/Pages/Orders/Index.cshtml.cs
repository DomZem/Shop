using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using Shop.Application.Orders.Dtos;
using Shop.PanelAdmin.Config;

namespace Shop.PanelAdmin.Pages.Orders
{
    public class IndexModel(ILogger<IndexModel> logger, IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        public List<OrderDto> Orders { get; private set; }

        public string? ErrorMessage = string.Empty;

        public async Task OnGetAsync()
        {
            var token = HttpContext.Session.GetString("AuthToken");

            if(token != null)
            {
                var options = new RestClientOptions("http://example.com")
                {
                    Authenticator = new JwtAuthenticator(token),
                    BaseUrl = new Uri($"https://localhost:7270")
                };

                var client = new RestClient(options);
                var request = new RestRequest($"api/orders");
                request.AddHeader("content-type", "application/json");
                var response = await client.ExecuteGetAsync<List<OrderDto>>(request);

                if(response.IsSuccessful)
                {
                    Orders = response.Data;
                    logger.LogInformation($"orders length: {response.Data.Count}");
                }
                else
                {
                    ErrorMessage = "asda";
                }
            }
            else
            {
                ErrorMessage = "asda";
            }


            //Orders = await client.GetAsync<List<OrderDto>>(request);
        }
    }
}
