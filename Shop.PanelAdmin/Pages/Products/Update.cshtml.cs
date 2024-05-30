using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp;
using RestSharp.Authenticators;
using Shop.Application.ProductCategories.Dtos;
using Shop.Application.Products.Commands.UpdateProduct;

namespace Shop.PanelAdmin.Pages.Products
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public UpdateProductCommand Product { get; set; } = default!;

        public List<SelectListItem> ProductCategories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var token = HttpContext.Session.GetString("AuthToken");

            if (token == null)
            {
                return Unauthorized();
            }
            
            if (id == null)
            {
                return NotFound();  
            }

            var options = new RestClientOptions()
            {
                Authenticator = new JwtAuthenticator(token),
                BaseUrl = new Uri($"https://localhost:7270")
            };

            var client = new RestClient(options);
            
            var productCategoriesRequest = new RestRequest("api/productCategories");
            productCategoriesRequest.AddHeader("content-type", "application/json");

            var productRequest = new RestRequest($"api/products/{id}");
            productRequest.AddHeader("content-type", "application/json");

            var productCategoriesResponse = await client.ExecuteGetAsync<List<ProductCategoryDto>>(productCategoriesRequest);
            var productResponse = await client.ExecuteGetAsync<UpdateProductCommand>(productRequest);

            if(productCategoriesResponse.Data == null || productResponse.Data == null)
            {
                return NotFound();
            }
           
            Product = productResponse.Data;
            ProductCategories = productCategoriesResponse.Data.Select(pc => new SelectListItem() { Value = pc.Id.ToString(), Text = $"{pc.Name}" }).ToList();
        
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
                BaseUrl = new Uri($"https://localhost:7270")
            };

            var client = new RestClient(options);
            var request = new RestRequest($"api/products/{id}");
            request.AddHeader("content-type", "application/json");
            request.AddBody(Product);
            var response = await client.ExecutePutAsync(request);

            if (response.IsSuccessful)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
