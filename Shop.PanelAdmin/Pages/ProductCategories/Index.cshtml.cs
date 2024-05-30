using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using Shop.Application.ProductCategories.Dtos;
using Shop.PanelAdmin.Config;

namespace Shop.PanelAdmin.Pages.ProductCategories
{
    public class IndexModel(IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        public List<ProductCategoryDto> ProductCategories { get; private set; }

        public string? ErrorMessage = string.Empty;

        public async Task OnGetAsync()
        {
            var token = HttpContext.Session.GetString("AuthToken");

            if (token != null)
            {
                var options = new RestClientOptions()
                {
                    Authenticator = new JwtAuthenticator(token),
                    BaseUrl = new Uri($"https://localhost:7270")
                };

                var client = new RestClient(options);
                var request = new RestRequest($"api/productCategories");
                request.AddHeader("content-type", "application/json");
                var response = await client.ExecuteGetAsync<List<ProductCategoryDto>>(request);

                if (response.IsSuccessful)
                {
                    ProductCategories = response.Data;
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
        }
    }
}
