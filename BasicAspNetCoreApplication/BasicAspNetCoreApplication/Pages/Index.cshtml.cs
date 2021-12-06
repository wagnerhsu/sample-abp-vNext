using BasicAspNetCoreApplication.Infrastructure;
using BasicAspNetCoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace BasicAspNetCoreApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MyTestOptions _myTestOptions;

        public IndexModel(ILogger<IndexModel> logger, IOptions<MyTestOptions> myTestOptions)
        {
            _logger = logger;
            _myTestOptions = myTestOptions.Value;
        }

        public void OnGet()
        {
            _logger.LogInformation(_myTestOptions.AsJson());
        }
    }
}