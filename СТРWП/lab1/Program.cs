internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy("AllowAll",
        //        builder =>
        //        {
        //            builder.AllowAnyOrigin()
        //                   .AllowAnyMethod()
        //                   .AllowAnyHeader();
        //        });
        //});

        var app = builder.Build();

        //app.UseCors("AllowAll");

        app.MapGet("", () => {});

        app.MapGet("/BDA", async (context) => {
            var ParmA = context.Request.Query["ParmA"];
            var ParmB = context.Request.Query["ParmB"];

            context.Response.ContentType = "text/plain";

            await context.Response.WriteAsync($"GET-HTTP-BDA:ParmA = {ParmA},ParmB = {ParmB}");
        });

        app.MapPost("/BDA", async (context) => {
            var ParmA = context.Request.Query["ParmA"];
            var ParmB = context.Request.Query["ParmB"];

            context.Response.ContentType = "text/plain";

            await context.Response.WriteAsync($"POST-HTTP-BDA:ParmA = {ParmA},ParmB = {ParmB}");
        });

        app.MapPut("/BDA", async (context) => {
            var ParmA = context.Request.Query["ParmA"];
            var ParmB = context.Request.Query["ParmB"];

            context.Response.ContentType = "text/plain";

            await context.Response.WriteAsync($"PUT-HTTP-BDA:ParmA = {ParmA},ParmB = {ParmB}");
        });

        app.MapPost("/BDA/sum", async (context) => {
            var X = context.Request.Query["X"];
            var Y = context.Request.Query["Y"];

            context.Response.ContentType = "text/plain";

            await context.Response.WriteAsync($"POST-HTTP-BDA/sum: = {int.Parse(X) + int.Parse(Y)}");
        });

        app.MapGet("/BDA/multiply", async (context) => {
            var X = context.Request.Query["X"];
            var Y = context.Request.Query["Y"];

            context.Response.ContentType = "text/html";

            await context.Response.SendFileAsync("multiply.html");
        });

        app.MapPost("/BDA/multiply", async (context) => {
            var X = context.Request.Query["X"];
            var Y = context.Request.Query["Y"];

            context.Response.ContentType = "text/plain";

            await context.Response.WriteAsync($"POST-HTTP-BDA/multiply: = {int.Parse(X) * int.Parse(Y)}");
        });

        app.MapGet("/BDA/multiplyForm", async (context) => {
            var X = context.Request.Query["X"];
            var Y = context.Request.Query["Y"];

            context.Response.ContentType = "text/html";

            await context.Response.SendFileAsync("multiplyform.html");
        });

        app.MapPost("/BDA/multiplyForm", async (context) => {
            var X = context.Request.Form["Xinp"];
            var Y = context.Request.Form["Yinp"];

            context.Response.ContentType = "text/plain";

            await context.Response.WriteAsync($"POST-HTTP-BDA/multiplyForm: = {int.Parse(X) * int.Parse(Y)}");
        });

        app.Run();
    }
}