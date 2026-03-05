internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();


        app.UseWelcomePage("/aspnetcore");
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.MapGet("/aspnetcore", () => "Hello World!");

        app.Run();
    }
}