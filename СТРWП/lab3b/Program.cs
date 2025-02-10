using lab3b;
using lab3b.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString(
    //"DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."
    "Identity") ?? throw new InvalidOperationException("Connection string 'Identity' not found."
    );
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(
    options => {
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.Password.RequiredUniqueChars = 1;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.User.RequireUniqueEmail = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = "/Admin/SignIn";
//    options.LogoutPath = "/Admin/SignOut";
//    options.AccessDeniedPath = "/Admin/Error";
//});

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.Events.OnRedirectToLogin = context =>
//    {
//        context.Response.StatusCode = 401;
//        return Task.CompletedTask;
//    };
//});

//builder.Services.AddAuthentication(options => options.DefaultScheme = "Cookies").AddCookie("Cookies", "Cookies", options => {
//    options.Events.OnRedirectToAccessDenied = context =>
//    {
//        context.Response.StatusCode = 403;
//        return Task.CompletedTask;
//    };
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Admin/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "admin",
    pattern: "{controller=Admin}",
    defaults: new { action = "SignIn" }
);

app.MapControllerRoute(
    name: "signIn",
    pattern: "{controller=Admin}/{action=SignIn}"
    //defaults: new { action = "SignIn" }
);

app.MapRazorPages();


//services.ConfigureApplicationCookie(options =>
//{
//    options.Events.OnRedirectToLogin = context =>
//    {
//        context.Response.StatusCode = 401;
//        return Task.CompletedTask;
//    };
//});

//services.AddAuthentication(options => options.DefaultScheme = "Cookies").AddCookie("Cookies", "Cookies", options => {
//    options.Events.OnRedirectToAccessDenied = context => 
//    {
//        context.Response.StatusCode = 403;
//        return Task.CompletedTask;
//    }
//}

InitApp initApp = new InitApp(app);
await initApp.initIdentity();

app.Run();