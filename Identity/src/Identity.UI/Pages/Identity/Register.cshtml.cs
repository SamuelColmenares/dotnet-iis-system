using Identity.UI.Aggregates;
using Identity.UI.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Identity.UI.Pages.Identity
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterDto Register { get; set; }

        [BindProperty]
        public string ValorSecreto { get; set; }
        public string ReturnUrl { get; set; } = string.Empty;
        private readonly UserManager<MyUserAggregate> _userManager;
        private readonly AzureKevaultInfo _vaultInfo;
        private readonly IConfiguration _configuration;

        public RegisterModel(UserManager<MyUserAggregate> userManager, 
            IOptions<AzureKevaultInfo> vaultInfo,
            IConfiguration conf)
        {
            _userManager = userManager;
            _vaultInfo = vaultInfo.Value;
            _configuration = conf;
        }

        public void OnGet()
        {
            var info = _vaultInfo;
            ValorSecreto = _configuration["MiUrlApi"] ?? "No se encontró el valor secreto";
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
            //Parallel.ForEach(result.Errors, error =>
            //{
            //    ModelState.AddModelError(string.Empty, error.Description);
            //});

            //List<Task<string>> tasks = new List<Task<string>> ();

            //result.Errors.ToList().ForEach( async error =>
            //{
            //    tasks.Add( Task.Run( async () =>  await Task.Delay( 2200 ) );
            //    ModelState.AddModelError(string.Empty, error.Description);
            //});

            //var res = Task.WhenAll(tasks);


            return Page();
            //return Page();


/*
 * Mover Aggregates DbContexts migrations a otra projecto (infrastructure)
 * Crear controlador que exponga API para registrar, login, singout usuario
 * angular front que consuma la API
 * Deberia trabajarse en el mismo puerto
 * Identity UI cambiara a Identity.API
 * agregar capa application donde application consuma infrastructure 
 * Identity.API consuma application
 * 
 */
}
}
}
