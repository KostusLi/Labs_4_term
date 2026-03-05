using Microsoft.Extensions.FileProviders;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();


        app.UseWelcomePage("/aspnetcore");
        app.UseDefaultFiles(new DefaultFilesOptions
        {
            DefaultFileNames = new List<string> { "Neumann.html" }
        });

        app.UseStaticFiles();
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "Picture")),
            RequestPath = "/Picture"
        });
        app.UseStaticFiles("/static");
        app.MapGet("/aspnetcore", () => "Hello World!");

        app.Run();
    }
}