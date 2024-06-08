using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RestSharp;
using Shop.Application.Products.Dtos;
using Shop.PanelAdmin.Config;

namespace Shop.PanelAdmin.Pages.Products
{
    public class IndexModel(IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        public List<ProductDto> Products { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var client = new RestClient(shopAPIConfig.Value.URL);
            var request = new RestRequest("/api/products") { RequestFormat = DataFormat.Json };

            var response = await client.ExecuteGetAsync<List<ProductDto>>(request);

            if(!response.IsSuccessStatusCode || response.Data == null)
            {
                return BadRequest();
            }

            Products = response.Data;

            return Page();
        }
    }
}
