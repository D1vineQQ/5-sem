using ASPCMVC06.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TMResearch}/{action=M01}/{id?}"
    );

app.MapControllerRoute(
    name: "V2",
    pattern: "V2/{controller=TMResearch}/{action=M02}"
    );

app.MapControllerRoute(
    name: "V3",
    pattern: "V3/{controller=TMResearch}/{string?}/{action=M03}"
    );

app.MapControllerRoute(
    name: "Any",
    pattern: "{*any}",
    defaults: new {controller = "TMResearch", action = "MXX"}
    );


app.Run();
