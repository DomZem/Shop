using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp.Authenticators;
using RestSharp;
using Shop.Application.Orders.Commands.CreateOrder;
using Shop.Application.Products.Dtos;
using Shop.Application.Users.Dtos;
using Shop.Application.OrderStatuses.Dtos;
using Microsoft.Extensions.Options;
using Shop.PanelAdmin.Config;

namespace Shop.PanelAdmin.Pages.Orders
{
    public class CreateModel(IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        [BindProperty]
        public CreateOrderCommand Order { get; set; } = default!;

        public List<SelectListItem> Products { get; set; }

        public List<SelectListItem> OrderStatuses { get; set; }

        public List<SelectListItem> Users { get; set; }

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

            var productsRequest = new RestRequest("/api/products");
            productsRequest.AddHeader("content-type", "application/json");

            var orderStatusesRequest = new RestRequest("/api/orderStatuses");
            orderStatusesRequest.AddHeader("content-type", "application/json");

            var usersRequest = new RestRequest("/api/users");
            usersRequest.AddHeader("content-type", "application/json");

            var productsResponse = await client.ExecuteGetAsync<List<ProductDto>>(productsRequest);
            var orderStatusesResponse = await client.ExecuteGetAsync<List<OrderStatusDto>>(orderStatusesRequest);
            var usersResponse = await client.ExecuteGetAsync<List<UserDto>>(usersRequest);

            if (productsResponse.Data == null || orderStatusesResponse.Data == null || usersResponse.Data == null)
            {
                return NotFound();
            }

            Products = productsResponse.Data
                .Where(p => p.Quantity > 0)
                .Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = $"{p.Name} - $ {p.Price} | {p.Quantity} left" }).ToList();
            OrderStatuses = orderStatusesResponse.Data.Select(os => new SelectListItem() { Value = os.Id.ToString(), Text = os.Name }).ToList();
            Users = usersResponse.Data.Select(u => new SelectListItem() { Value = u.Id.ToString(), Text = u.Email }).ToList();

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
            var request = new RestRequest("/api/orders");
            request.AddHeader("content-type", "application/json");
            request.AddBody(Order);
            var response = await client.ExecutePostAsync(request);

            if (response.IsSuccessful)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                 ModelState.AddModelError(string.Empty, response.Content);
            }

            return Page();
        }
    }
}
