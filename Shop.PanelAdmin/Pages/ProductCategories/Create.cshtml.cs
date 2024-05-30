using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.ProductCategories.Commands.CreateProductCategory;
using RestSharp;
using RestSharp.Authenticators;

namespace Shop.PanelAdmin.Pages.ProductCategories
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateProductCategoryCommand ProductCategory { get; set; } = default!;

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
            var request = new RestRequest($"api/productCategories");
            request.AddHeader("content-type", "application/json");
            request.AddBody(ProductCategory);
            var response = await client.ExecutePostAsync(request);

            if (response.IsSuccessful)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
