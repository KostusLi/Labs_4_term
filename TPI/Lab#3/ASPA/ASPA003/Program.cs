using DAL003;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Repository.JSONFileName = "Celebrities.json";

using (IRepository repository = Repository.Create("Celebrities"))
{

    app.UseDirectoryBrowser(new DirectoryBrowserOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, repository.BasePath)),
        RequestPath = "/Photos"
    });
    
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, repository.BasePath)),

        OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers.Append("Content-Disposition", "attachment");
        },
        RequestPath = "/Download"
    });

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Photo")),
        RequestPath = "/Photo"
    });

    app.MapGet("/Celebrities",
        () => repository.getAllCelebrities());

    app.MapGet("/Celebrities/{id:int}",
        (int id) => repository.getCelebrityById(id));

    app.MapGet("/Celebrities/BySurname/{surname}",
        (string surname) => repository.getCelebritiesBySurname(surname));

    app.MapGet("/Celebrities/PhotoPathById/{id:int}",
        (int id) => repository.getPhotoPathById(id));

    app.MapGet("/",
        () => "Hello World!");
}


app.Run();
