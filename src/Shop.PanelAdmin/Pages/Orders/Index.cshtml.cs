using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using Shop.Application.Orders.Dtos;
using Shop.PanelAdmin.Config;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace Shop.PanelAdmin.Pages.Orders
{
    public class IndexModel(IOptions<ShopAPIConfig> shopAPIConfig, PdfGenerator.PdfGeneratorClient pdfGeneratorClient) : PageModel
    {
        public List<OrderDto> Orders { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("AuthToken");
                
            if(token == null)
            {
                return Unauthorized();
            } 
            
            var options = new RestClientOptions()
            {
                Authenticator = new JwtAuthenticator(token),
                BaseUrl = new Uri(shopAPIConfig.Value.URL)
            };

            var client = new RestClient(options);
            var request = new RestRequest("/api/orders");
            request.AddHeader("content-type", "application/json");
            var response = await client.ExecuteGetAsync<List<OrderDto>>(request);

            if(!response.IsSuccessStatusCode || response.Data == null)
            {
                return BadRequest();
            }

            Orders = response.Data;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("AuthToken");

            if(token == null) 
            {
                return Unauthorized();
            }

            var options = new RestClientOptions()
            {
                Authenticator = new JwtAuthenticator(token),
                BaseUrl = new Uri(shopAPIConfig.Value.URL)
            };

            var client = new RestClient(options);
            var request = new RestRequest("/api/orders");
            request.AddHeader("content-type", "application/json");
            var response = await client.ExecuteGetAsync<List<OrderDto>>(request);

            if(!response.IsSuccessStatusCode || response.Data == null)
            {
                return BadRequest();
            }

            var htmlContent = GenerateHtmlContent(response.Data);

            var document = await pdfGeneratorClient.GenerateAsync(new HtmlDocumentRequest()
            {
                Content = htmlContent,
                Name = "Orders Summary"
            });

            var stream = new MemoryStream(document.Content.ToByteArray());

            return new FileStreamResult(stream, new MediaTypeHeaderValue("application/pdf"))
            {
                FileDownloadName = "orders-summary.pdf"
            };
        }

        private string GenerateHtmlContent(List<OrderDto> orders)
        {
            var sb = new StringBuilder();

            sb.Append("<h1>Orders Summary</h1>");
            sb.Append("<table border='1'>");
            sb.Append("<tr>");
            sb.Append("<th>ID</th>");
            sb.Append("<th>Ordered At</th>");
            sb.Append("<th>Product Name</th>");
            sb.Append("<th>Product Quantity</th>");
            sb.Append("<th>Order Status</th>");
            sb.Append("<th>Total Price</th>");
            sb.Append("</tr>");

            decimal totalPrice = 0;

            foreach (var order in orders)
            {
                sb.Append("<tr>");
                sb.Append($"<td>{order.Id}</td>");
                sb.Append($"<td>{order.OrderedAt}</td>");
                sb.Append($"<td>{order.Product.Name}</td>");
                sb.Append($"<td>{order.ProductQuantity}</td>");
                sb.Append($"<td>{order.OrderStatus.Name}</td>");
                sb.Append($"<td>{order.TotalPrice}</td>");
                sb.Append("</tr>");
                totalPrice += order.TotalPrice;
            }

            sb.Append("<tr>");
            sb.Append("<td colspan='5' style='text-align:right'><strong>Total:</strong></td>");
            sb.Append($"<td><strong>{totalPrice}</strong></td>");
            sb.Append("</tr>");

            sb.Append("</table>");

            return sb.ToString();
        }
    }
}
