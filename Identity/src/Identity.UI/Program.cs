using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Identity.UI.Aggregates;
using Identity.UI.DBContexts;
using Identity.UI.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<AzureKevaultInfo>(builder.Configuration.GetSection("AzureKeyVaultInfo"));

if (builder.Environment.IsProduction())
{
    var azureKeyVaultInfo = builder.Configuration.GetSection("AzureKeyVaultInfo").Get<AzureKevaultInfo>();

    if (azureKeyVaultInfo is not null)
    {
        var vaultUri = new Uri(string.Format(azureKeyVaultInfo.VaultUri, azureKeyVaultInfo.VaultName));
        builder.Configuration.AddAzureKeyVault(
            vaultUri,
            new DefaultAzureCredential(),
            new AzureKeyVaultConfigurationOptions
            {
                ReloadInterval = TimeSpan.FromMinutes(1)
            });
    }
}

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<MyIdentityDBContext>(options =>
{
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connString);
});

builder.Services
    .AddIdentity<MyUserAggregate, MyRolesAgreggate>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 10;
        options.Password.RequiredUniqueChars = 1;

        options.User.RequireUniqueEmail = true;
        options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

        options.SignIn.RequireConfirmedEmail = false;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        options.Lockout.MaxFailedAccessAttempts = 5;
    })
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<MyIdentityDBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
