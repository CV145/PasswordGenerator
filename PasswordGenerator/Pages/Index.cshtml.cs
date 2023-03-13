using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;

namespace PasswordGenerator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string? GeneratedPassword { get; set; }
        [Range(4, 20,
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

        //Called when the Razor page is simply requested
        public void OnGet()
        {
            Console.WriteLine("Get: " + PasswordLength);
            //GeneratedPassword = HttpContext.Request.Cookies["GeneratedPassword"];
            PasswordLength = HttpContext.Session.GetInt32("PasswordLength") ?? 8;
            Console.WriteLine("Get: " + PasswordLength);
        }

        //Called when an HTTP POST request is made to Index page
        //These are done when users submit forms
        //<form method="post">
        //POST submits data then refreshes and calls a GET
        public void OnPost(int PasswordLength, bool IncludeUppercase, bool IncludeLowercase, bool IncludeNumbers, bool IncludeSymbols)
        {
            //Generate password
            GeneratedPassword = GeneratePassword(PasswordLength, IncludeUppercase, IncludeLowercase, IncludeNumbers, IncludeSymbols);

            //Model binding automatically takes care of this
            //HttpContext.Session.SetString("GeneratedPassword", GeneratedPassword);
            //HttpContext.Session.SetInt32("PasswordLength", PasswordLength);
            Console.WriteLine("Post: " + PasswordLength);
        }

        public string GeneratePassword(int length, bool useUppercase, bool useLowercase, bool useNumbers, bool useSymbols)
        {
            const string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
            const string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string Numbers = "0123456789";
            const string Symbols = "!@#$%^&*()_+-=[]{}|;':\",./<>?";

            var allowedChars = new StringBuilder();
            if (useUppercase)
            {
                allowedChars.Append(UppercaseLetters);
            }

            if (useLowercase)
            {
                allowedChars.Append(LowercaseLetters);
            }

            if (useNumbers)
            {
                allowedChars.Append(Numbers);
            }

            if (useSymbols)
            {
                allowedChars.Append(Symbols);
            }

            var password = new char[length];
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                password[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(password);
        }
    }
}
        
