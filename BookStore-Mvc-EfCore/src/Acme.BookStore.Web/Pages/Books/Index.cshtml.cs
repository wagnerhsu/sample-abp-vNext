using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Acme.BookStore.Web.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            _logger.LogDebug("IndexModel Debug Info");
        }
    }
}