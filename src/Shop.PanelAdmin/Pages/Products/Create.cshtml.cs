using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp.Authenticators;
using RestSharp;
using Shop.Application.Products.Commands.CreateProduct;
using Shop.Application.ProductCategories.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Shop.PanelAdmin.Config;

namespace Shop.PanelAdmin.Pages.Products
{
    public class CreateModel(IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        [BindProperty]
        public CreateProductCommand Product { get; set; } = default!;

        public List<SelectListItem> ProductCategories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("AuthToken");

            if (token == null)
            {
                return Unauthorized();
            }
                        
            var options = new RestClientOptions()
            {
                Authenticator = new JwtAuthenticator(token),
                BaseUrl = new Uri(shopAPIConfig.Value.URL)
            };

            var client = new RestClient(options);
            var request = new RestRequest("/api/productCategories");
            request.AddHeader("content-type", "application/json");
            var response = await client.ExecuteGetAsync<List<ProductCategoryDto>>(request);

            if (response.IsSuccessful)
            {
                ProductCategories = response.Data.Select(pc => new SelectListItem() { Value = pc.Id.ToString(), Text = $"{pc.Name}" }).ToList();  
            }

            return Page();
        }

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
                BaseUrl = new Uri(shopAPIConfig.Value.URL)
            };

            var client = new RestClient(options);
            var request = new RestRequest("/api/products");
            request.AddHeader("content-type", "application/json");    
            request.AddBody(Product);
            var response = await client.ExecutePostAsync(request);

            if (response.IsSuccessful)
            {
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError(string.Empty, response.Content);

            return Page();
        }
    }
}
