using DinkToPdf;

namespace Shop.GrpcService.Configurations
{
    public class HtmlToPdfConfiguration
    {
        public GlobalSettings GlobalSettings { get; } = new()
        {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4,
            Margins = new MarginSettings { Top = 10 },
        };

        public ObjectSettings ObjectSettings { get; } = new()
        {
            PagesCount = true,
            WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
            HeaderSettings = { Right = "Page [page] of [toPage]", Line = true },
            FooterSettings = { Line = true, Center = "Report Footer" }
        };
    }
}
