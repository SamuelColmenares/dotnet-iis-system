using Identity.UI.Aggregates;
using Identity.UI.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.UI.Pages.Identity;

public class LogInModel : PageModel
{
    private readonly UserManager<MyUserAggregate> _userManager;
    private readonly SignInManager<MyUserAggregate> _signInManager;

    [BindProperty]
    public LoginDto LogIn { get; set; }

    public LogInModel(
        UserManager<MyUserAggregate> userManager,
        SignInManager<MyUserAggregate> signInManager
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
        var result = await _signInManager.PasswordSignInAsync(LogIn.Email, LogIn.Password, false, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return RedirectToPage("/");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
