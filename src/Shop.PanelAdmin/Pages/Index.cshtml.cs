using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RestSharp;
using Shop.PanelAdmin.Config;
using Shop.PanelAdmin.Models;
using System.Net;

namespace Shop.PanelAdmin.Pages
{
    public class IndexModel(ILogger<IndexModel> logger, IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        [BindProperty]
        public LoginUser LoginUser { get; set; } = default!;

        public string ErrorMessage { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = new RestClient(shopAPIConfig.Value.URL);
            var request = new RestRequest("/api/auth/login").AddBody(LoginUser);

            try
            {
                var response = await client.PostAsync<AccessTokenResponse>(request);
                logger.LogInformation($"response {response}");
                if (response.AccessToken != string.Empty)
                {
                    
                    logger.LogInformation($"access token: {response.AccessToken}");

                    // Store the token in session
                    HttpContext.Session.SetString("AuthToken", response.AccessToken);

                    return RedirectToPage("/Products/Index");
                }
                else
                {
                    ErrorMessage = "Login failed. Please check your credentials and try again.";
                    return Page();
                }
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                ErrorMessage = "Login failed. Unauthorized access.";
                return Page();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unexpected error occurred during login.");
                ErrorMessage = "An unexpected error occurred. Please try again later.";
                return Page();
            }
        }
    }
}
