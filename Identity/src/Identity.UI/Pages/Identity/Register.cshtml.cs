using Identity.UI.Aggregates;
using Identity.UI.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.UI.Pages.Identity
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterDto Register { get; set; }
        public string ReturnUrl { get; set; } = string.Empty;
        private readonly UserManager<MyUserAggregate> _userManager;

        public RegisterModel(UserManager<MyUserAggregate> userManager)
        {
            _userManager = userManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(Register.Password != Register.ConfirmPassword)
            {
                throw new Exception("Passwords do not match");
            }

            var user = new MyUserAggregate
            {
                UserName = Register.Email,
                Email = Register.Email,
                DocumentNumber = Register.DocumentNumber,
                DocumentType = Register.DocumentType
            };

            var result = await _userManager.CreateAsync(user, Register.Password);

            if(ReturnUrl == null)
            {
                ReturnUrl = Url.Content("/");
            }

            return Page();
            //return Page();
        }
    }
}
