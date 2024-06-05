using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using Shop.Application.Users.Dtos;
using Shop.PanelAdmin.Config;

namespace Shop.PanelAdmin.Pages.Users
{
    public class IndexModel(ILogger<IndexModel> logger, IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        public List<UserDto> Users { get; private set; }

        public string? ErrorMessage = string.Empty;

        public async Task OnGetAsync()
        {
            var token = HttpContext.Session.GetString("AuthToken");

            if (token != null)
            {
                var options = new RestClientOptions()
                {
                    Authenticator = new JwtAuthenticator(token),
                    BaseUrl = new Uri(shopAPIConfig.Value.URL)
                };

                var client = new RestClient(options);
                var request = new RestRequest("/api/identity/users");
                request.AddHeader("content-type", "application/json");
                var response = await client.ExecuteGetAsync<List<UserDto>>(request);

                if (response.IsSuccessful)
                {
                    Users = response.Data;
                    logger.LogInformation($"users length: {response.Data.Count}");
                }
                else
                {
                    ErrorMessage = "asda";
                }
            }
            else
            {
                ErrorMessage = "asda";
            }

        }
    }
}
