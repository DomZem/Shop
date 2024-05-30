using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp.Authenticators;
using RestSharp;
using Shop.Application.OrderStatuses.Dtos;
using Shop.Application.Users.Dtos;
using Shop.Application.Orders.Commands.UpdateOrder;
using Shop.Application.Products.Dtos;
using Shop.Application.Orders.Dtos;

namespace Shop.PanelAdmin.Pages.Orders
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public UpdateOrderCommand Order { get; set; } = default!;

        public List<SelectListItem> Products { get; set; }

        public List<SelectListItem> OrderStatuses { get; set; }

        public List<SelectListItem> Users { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
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
                BaseUrl = new Uri($"https://localhost:7270")
            };

            var client = new RestClient(options);

            var orderRequest = new RestRequest($"api/orders/{id}");
            orderRequest.AddHeader("content-type", "application/json");

            var productsRequest = new RestRequest("api/products");
            productsRequest.AddHeader("content-type", "application/json");

            var orderStatusesRequest = new RestRequest("api/orderStatuses");
            orderStatusesRequest.AddHeader("content-type", "application/json");

            var usersRequest = new RestRequest("api/identity/users");
            usersRequest.AddHeader("content-type", "application/json");

            var orderResponse = await client.ExecuteGetAsync<OrderDetailsDto>(orderRequest);
            var productsResponse = await client.ExecuteGetAsync<List<ProductDto>>(productsRequest);
            var orderStatusesResponse = await client.ExecuteGetAsync<List<OrderStatusDto>>(orderStatusesRequest);
            var usersResponse = await client.ExecuteGetAsync<List<UserDto>>(usersRequest);

            if (productsResponse.Data == null || orderStatusesResponse.Data == null || usersResponse.Data == null || orderResponse.Data == null)
            {
                return NotFound();
            }

            var order = orderResponse.Data;

            Order = new UpdateOrderCommand() 
            { 
                Street = order.Street,
                City = order.City,
                Country = order.Country,
                Id = order.Id,
                PostalCode = order.PostalCode,
                OrderedById = order.OrderedBy.Id,
                OrderStatusId = order.OrderStatus.Id,
                ProductId = order.Product.Id,
                ProductQuantity = order.ProductQuantity 
            };

            Products = productsResponse.Data.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = $"{p.Name} - $ {p.Quantity}" }).ToList();
            OrderStatuses = orderStatusesResponse.Data.Select(os => new SelectListItem() { Value = os.Id.ToString(), Text = os.Name }).ToList();
            Users = usersResponse.Data.Select(u => new SelectListItem() { Value = u.Id.ToString(), Text = u.Email }).ToList();

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
            var request = new RestRequest($"api/orders/{id}");
            request.AddHeader("content-type", "application/json");
            request.AddBody(Order);
            var response = await client.ExecutePutAsync(request);

            if (response.IsSuccessful)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
