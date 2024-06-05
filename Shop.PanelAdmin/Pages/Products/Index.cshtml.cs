﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RestSharp;
using Shop.Application.Products.Dtos;
using Shop.PanelAdmin.Config;

namespace Shop.PanelAdmin.Pages.Products
{
    public class IndexModel(IOptions<ShopAPIConfig> shopAPIConfig) : PageModel
    {
        public List<ProductDto> Products { get; private set; }

        public async Task OnGetAsync()
        {
            var client = new RestClient(shopAPIConfig.Value.URL);
            var request = new RestRequest("/api/products") { RequestFormat = DataFormat.Json };

            Products = await client.GetAsync<List<ProductDto>>(request);
        }
    }
}
