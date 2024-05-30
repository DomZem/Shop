using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using Shop.Application.Products.Dtos;

namespace Shop.PanelAdmin.Pages.Products
{
    public class DetailsModel : PageModel
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
                BaseUrl = new Uri($"https://localhost:7270")
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
