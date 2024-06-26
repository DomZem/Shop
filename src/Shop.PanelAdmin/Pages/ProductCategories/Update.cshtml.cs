using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp.Authenticators;
using RestSharp;
using Shop.Application.ProductCategories.Commands.UpdateProductCategory;
using Microsoft.Extensions.Options;
using Shop.PanelAdmin.Config;

namespace Shop.PanelAdmin.Pages.ProductCategories
{
    public class UpdateModel(IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        [BindProperty]
        public UpdateProductCategoryCommand ProductCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
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
                BaseUrl = new Uri(shopAPIConfig.Value.URL)
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/productCategories/{id}");
            request.AddHeader("content-type", "application/json");
            var response = await client.ExecuteGetAsync<UpdateProductCategoryCommand>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                return NotFound();
            }

            ProductCategory = response.Data;

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
            var request = new RestRequest($"api/productCategories/{id}");
            request.AddHeader("content-type", "application/json");
            request.AddBody(ProductCategory);
            var response = await client.ExecutePutAsync(request);

            if (response.IsSuccessful)
            {
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError(string.Empty, response.Content);

            return Page();
        }
    }
}
