using CedarCreek.ApplicationServices.Services;
using CedarCreek.Core.Domain;
using CedarCreek.Core.ServiceInterface;
using CedarCreek.Data;
using CedarCreek.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICharactersServices, CharacterServices>();
builder.Services.AddScoped<IFileServices, FileServices>();
builder.Services.AddScoped<IAccountsServices, AccountsServices>();
builder.Services.AddScoped<IEmailsServices, EmailsServices>();
builder.Services.AddScoped<IPlayerProfilesServices, PlayerProfilesServices>();
builder.Services.AddScoped<IRealmsServices, RealmsServices>();
builder.Services.AddScoped<PlayerProfile>();
builder.Services.AddDbContext<CedarCreekContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequiredLength = 3;
        options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    })

    .AddEntityFrameworkStores<CedarCreekContext>()
    .AddDefaultTokenProviders()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("CustomEmailConfirmation")
    .AddDefaultUI();

//all tokens
builder.Services.Configure<DataProtectionTokenProviderOptions>(
    options => options.TokenLifespan = TimeSpan.FromHours(5)
    );

//email tokens confirmation
builder.Services.Configure<CustomEmailConfirmationTokenProviderOptions>(
    options => options.TokenLifespan = TimeSpan.FromDays(3)
    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
