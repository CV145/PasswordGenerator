using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace PasswordGenerator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [Range(8, 12,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int PasswordLength { get; set; } = 8;
        public bool IncludeUppercase { get; set; } = true;
        public bool IncludeLowercase { get; set; }
        = true;
        public bool IncludeNumbers { get; set; }
       = true;
        public bool IncludeSymbols { get; set; }
       = true;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}