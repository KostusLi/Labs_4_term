using ASPA005_2.Filters;
using DAL004;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Repository.JSONFileName = "Celebrities.json";

using (IRepository repository = Repository.Create("Celebrities"))
{
    SurnameFilter.repository = repository;
    PhotoExistFilter.repository = repository;
    IdExistsFilter.repository = repository;

    app.UseExceptionHandler("/Celebrities/Error");

    RouteGroupBuilder celebApi = app.MapGroup("/Celebrities");

    celebApi.MapGet("/", () => repository.getAllCelebrities());

    celebApi.MapGet("/{id:int}", (int id) => {
        var c = repository.getCelebrityById(id);
        return c ?? throw new Exception($"Found by Id: Celebrity Id = {id}");
    });

    celebApi.MapPost("/", (Celebrity celebrity) => {
        int? id = repository.addCelebrity(celebrity);
        repository.SaveChanges();
        return celebrity with { Id = id ?? 0 };
    })
    .AddEndpointFilter<SurnameFilter>()
    .AddEndpointFilter<PhotoExistFilter>();

    celebApi.MapPut("/{id:int}", (int id, Celebrity celebrity) => {
        repository.updCelebrityById(id, celebrity);
        repository.SaveChanges();
        return celebrity with { Id = id };
    })
    .AddEndpointFilter<IdExistsFilter>();

    celebApi.MapDelete("/{id:int}", (int id) => {
        repository.delCelebrityById(id);
        repository.SaveChanges();
        return Results.Ok(new { message = $"Celebrity with Id = {id} deleted" });
    })
    .AddEndpointFilter<IdExistsFilter>();



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
