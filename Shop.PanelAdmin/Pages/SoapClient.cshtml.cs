
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductService;

namespace Shop.PanelAdmin.Pages
{
    public class SoapClientModel(ILogger<SoapClientModel> logger) : PageModel
    {
        public void OnGet()
        {
            IProductService soapServiceChannel = new ProductServiceClient(ProductServiceClient.EndpointConfiguration.BasicHttpBinding_IProductService);
            var result = soapServiceChannel.GetAll();

            logger.LogInformation($"products length: {result.Length}");
        }
    }
}
