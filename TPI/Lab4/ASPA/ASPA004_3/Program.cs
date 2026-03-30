using DAL004;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Repository.JSONFileName = "Celebrities.json";

using (IRepository repository = Repository.Create("Celebrities"))
{
    app.UseExceptionHandler("/Celebrities/Error");

    app.MapGet("/Celebrities", () => repository.getAllCelebrities());

    app.MapGet("/Celebrities/{id:int}", (int id) =>
    {
        Celebrity? celebrity = repository.getCelebrityById(id);
        if (celebrity == null) throw new FoundByIdException($"Celebrity Id = {id}");
        return celebrity;
    });

    app.MapDelete("/Celebrities/{id:int}", (int id) =>
    {
        if (!repository.delCelebrityById(id)) throw new FoundByIdException($"{repository.BasePath} error, Id = {id}");
        return "Easy pizzy lemon squizy";
    });

    app.MapPut("/Celebrities/{id:int}", (int id, Celebrity celebrity) =>
    {
        if (repository.updCelebrityById(id, celebrity) == null) throw new FoundByIdException("Not Found Item");
        return celebrity with {Id=id};
    });

    app.MapPost("/Celebrities", (Celebrity celebrity) =>
    {
        int? id = repository.addCelebrity(celebrity);

        if (id == null)
            throw new AddCelebrityException("/Celebrities error, id == null");

        if (repository.SaveChanges() <= 0)
            throw new SaveException("/Celebrities error, SaveChanges() <= 0");

        return new Celebrity((int)id, celebrity.Firstname, celebrity.Surname, celebrity.PhotoPath);
    });

    app.MapFallback((HttpContext ctx) =>
        Results.NotFound(new { error = $"path {ctx.Request.Path} not supported" }));


    app.Map("/Celebrities/Error", (HttpContext ctx) =>
    {
        Exception? ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;

        IResult rc = Results.Problem(
            detail: $"Could not find file {Path.Combine(AppDomain.CurrentDomain.BaseDirectory, repository.BasePath, Repository.JSONFileName)}",
            instance: app.Environment.EnvironmentName,
            title: "ASPA004",
            statusCode: 500);

        if (ex != null)
        {
            if (ex is DeleteException)
                rc = Results.Problem(ex.Message);

            if (ex is FoundByIdException)
                rc = Results.NotFound(ex.Message);

            if (ex is BadHttpRequestException)
                rc = Results.BadRequest(ex.Message);

            if (ex is SaveException)
                rc = Results.Problem(
                    title: "ASPA004/SaveChanges",
                    detail: ex.Message,
                    instance: app.Environment.EnvironmentName,
                    statusCode: 500);

            if (ex is AddCelebrityException)
                rc = Results.Problem(
                    title: "ASPA004/addCelebrity",
                    detail: ex.Message,
                    instance: app.Environment.EnvironmentName,
                    statusCode: 500);
        }

        return rc;
    });

    app.Run();
}


public class FoundByIdException : Exception
{
    public FoundByIdException(string message)
        : base($"Found by Id: {message}") { }
}

public class SaveException : Exception
{
    public SaveException(string message)
        : base($"SaveChanges error: {message}") { }
}

public class AddCelebrityException : Exception
{
    public AddCelebrityException(string message)
        : base($"AddCelebrityException error: {message}") { }
}

public class DeleteException : Exception
{
    public DeleteException(string message) : base($"Delete by Id:DELETE {message}") { }
}
