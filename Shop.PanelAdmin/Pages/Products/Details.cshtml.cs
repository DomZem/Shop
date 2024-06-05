using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RestSharp;
using Shop.Application.Products.Dtos;
using Shop.PanelAdmin.Config;

namespace Shop.PanelAdmin.Pages.Products
{
    public class DetailsModel(IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        public ProductDto Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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
            var request = new RestRequest($"/api/products/{id}");
            request.AddHeader("content-type", "application/json");
            var response = await client.ExecuteGetAsync<ProductDto>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                return NotFound();
            }

            Product = response.Data;

            return Page();
        }
    }
}
