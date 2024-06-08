using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RestSharp;
using Shop.PanelAdmin.Config;
using Shop.PanelAdmin.Models;

namespace Shop.PanelAdmin.Pages
{
    public class IndexModel(IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        [BindProperty]
        public LoginUser LoginUser { get; set; } = default!;

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
            var response = await client.ExecutePostAsync<AccessTokenResponse>(request);

            if(!response.IsSuccessStatusCode || response.Data == null)
            {
                ModelState.AddModelError(string.Empty, "Inavlid password or email address");
                return Page();
            }
            
            HttpContext.Session.SetString("AuthToken", response.Data.AccessToken);

            return RedirectToPage("/Products/Index");
        }
    }
}
