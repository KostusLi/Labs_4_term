using ASPA007_1;
using static ASPA007_1.CelebritiesExtensions;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddCelebritiesConfiguration();
        builder.AddCelebritiesServices();

        builder.Services.AddRazorPages();
        builder.Services.AddRazorPages(o=>
            {
                o.Conventions.AddPageRoute("/Celebrities", "/");
                o.Conventions.AddPageRoute("/NewCelebrity", "/0");
                o.Conventions.AddPageRoute("/Celebrity", "/Celebrities/{id:int:min(1)}");
                o.Conventions.AddPageRoute("/Celebrity", "/{id:int:min(1)}");
            }
        );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseANCErrorHandler("ANC27X");

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.MapCelebrities();
        app.MapLifeevents();
        app.MapPhotoCelebrities();

        app.Run();
    }
}