using ASPA006_1;
using DAL_Celebrity_MSSQL;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ErrorHandlerMiddleware>();

builder.Configuration.AddJsonFile("Celebrities.config.json", optional: false, reloadOnChange: true);

builder.Services.Configure<CelebritiesConfig>(builder.Configuration.GetSection("Celebrities"));

builder.Services.AddScoped<IRepository, Repository>((IServiceProvider p) =>
{
    CelebritiesConfig config = p.GetRequiredService<IOptions<CelebritiesConfig>>().Value;
    return new Repository(config.ConnectionString);
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

// --------- ЗНАМЕНИТОСТИ (Celebrities) -------------------
var celebrities = app.MapGroup("/api/Celebrities");

// все знаменитости
celebrities.MapGet("/", (IRepository repo) => repo.GetAllCelebrities());

// знаменитость по ID
celebrities.MapGet("/{id:int:min(1)}", (IRepository repo, int id) =>
{
    var celebrity = repo.GetCelebrityById(id);
    return celebrity != null ? Results.Ok(celebrity) : Results.NotFound();
});

// знаменитость по ID события
celebrities.MapGet("/Lifeevents/{id:int:min(1)}", (IRepository repo, int id) =>
{
    var celebrity = repo.GetCelebrityByLifeeventId(id);
    return celebrity != null ? Results.Ok(celebrity) : Results.NotFound();
});

// удалить знаменитость по ID
celebrities.MapDelete("/{id:int:min(1)}", (IRepository repo, int id) =>
{
    bool isDeleted = repo.DelCelebrity(id);
    return isDeleted ? Results.Ok() : Results.NotFound();
});

// добавить новую знаменитость
celebrities.MapPost("/", (IRepository repo, Celebrity celebrity) =>
{
    bool isAdded = repo.AddCelebrity(celebrity);
    return isAdded ? Results.Ok(celebrity) : Results.BadRequest();
});

// изменить знаменитость по ID
celebrities.MapPut("/{id:int:min(1)}", (IRepository repo, int id, Celebrity celebrity) =>
{
    bool isUpdated = repo.UpdCelebrity(id, celebrity);
    return isUpdated ? Results.Ok(celebrity) : Results.NotFound();
});

// получить файл фотографии по имени файла (fname)
celebrities.MapGet("/photo/{fname}", async (IOptions<CelebritiesConfig> iconfig, HttpContext context, string fname) =>
{
    // Получаем путь к папке из конфигурации (из Задания 13)
    string folder = iconfig.Value.PhotosFolder;
    string fullPath = Path.Combine(folder, fname);

    // Проверяем, существует ли файл
    if (!System.IO.File.Exists(fullPath))
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        return;
    }

    // Определяем тип картинки для браузера
    string ext = Path.GetExtension(fname).ToLower();
    context.Response.ContentType = ext == ".png" ? "image/png" : "image/jpeg";

    // Асинхронно отправляем файл клиенту (поэтому в параметрах есть HttpContext)
    await context.Response.SendFileAsync(fullPath);
});


// --------- СОБЫТИЯ (Lifeevents) -------------------
var lifeevents = app.MapGroup("/api/Lifeevents");

// все события
lifeevents.MapGet("/", (IRepository repo) => repo.GetAllLifeevents());

// событие по ID
lifeevents.MapGet("/{id:int:min(1)}", (IRepository repo, int id) =>
{
    var lifeevent = repo.GetLifeevetById(id);
    return lifeevent != null ? Results.Ok(lifeevent) : Results.NotFound();
});

// все события по ID знаменитости
lifeevents.MapGet("/Celebrities/{id:int:min(1)}", (IRepository repo, int id) =>
{
    var events = repo.GetLifeeventsByCelebrityId(id);
    return Results.Ok(events); // Возвращаем список (он может быть пустым, это нормально)
});

// удалить событие по ID
lifeevents.MapDelete("/{id:int:min(1)}", (IRepository repo, int id) =>
{
    bool isDeleted = repo.DelLifeevent(id);
    return isDeleted ? Results.Ok() : Results.NotFound();
});

// добавить новое событие
lifeevents.MapPost("/", (IRepository repo, Lifeevent lifeevent) =>
{
    bool isAdded = repo.AddLifeevent(lifeevent);
    return isAdded ? Results.Ok(lifeevent) : Results.BadRequest();
});

// изменить событие по ID
lifeevents.MapPut("/{id:int:min(1)}", (IRepository repo, int id, Lifeevent lifeevent) =>
{
    bool isUpdated = repo.UpdLifeevent(id, lifeevent);
    return isUpdated ? Results.Ok(lifeevent) : Results.NotFound();
});


app.Run();
