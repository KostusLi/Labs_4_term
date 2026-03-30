using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseExceptionHandler("/Error");

app.MapGet("/A/{x:int:max(100)}", (HttpContext context, [FromRoute] int x) =>
    Results.Ok(new { path = context.Request.Path.Value, x = x }));

app.MapPost("/A/{x:int:range(0, 100)}", (HttpContext context, [FromRoute] int x) =>
    Results.Ok(new { path = context.Request.Path.Value, x = x }));

app.MapPut("/A/{x:int:min(1)}/{y:int:min(1)}", (HttpContext context, [FromRoute] int x, [FromRoute] int y) =>
    Results.Ok(new { path = context.Request.Path.Value, x = x, y = y }));

app.MapDelete("/A/{x:int:min(1)}-{y:int:min(1):max(100)}", (HttpContext context, [FromRoute] int x, [FromRoute] int y) =>
    Results.Ok(new { path = context.Request.Path.Value, x = x, y = y }));


app.MapGet("/B/{x:float}", (HttpContext context, [FromRoute] float x) =>
    Results.Ok(new { path = context.Request.Path.Value, x = x }));

app.MapPost("/B/{x:float}/{y:float}", (HttpContext context, [FromRoute] float x, [FromRoute] float y) =>
    Results.Ok(new { path = context.Request.Path.Value, x = x, y = y }));

app.MapDelete("/B/{x:float}-{y:float}", (HttpContext context, [FromRoute] float x, [FromRoute] float y) =>
    Results.Ok(new { path = context.Request.Path.Value, x = x, y = y }));


app.MapGet("/C/{x:bool}", (HttpContext context, [FromRoute] bool x) =>
    Results.Ok(new { path = context.Request.Path.Value, x = x }));

app.MapPost("/C/{x:bool},{y:bool}", (HttpContext context, [FromRoute] bool x, [FromRoute] bool y) =>
    Results.Ok(new { path = context.Request.Path.Value, x = x, y = y }));


app.MapGet("/D/{x:datetime}", (HttpContext context, [FromRoute] DateTime x) =>
    Results.Ok(new { path = context.Request.Path.Value, x = x }));

app.MapPost("/D/{x:datetime}|{y:datetime}", (HttpContext context, [FromRoute] DateTime x, [FromRoute] DateTime y) =>
    Results.Ok(new { path = context.Request.Path.Value, x = x, y = y }));


app.MapGet("/E/12-{x:required}", (HttpContext context, [FromRoute] string x) =>
    Results.Ok(new { path = context.Request.Path.Value, x = x }));

app.MapPut("/E/{x:length(2,12):alpha}", (HttpContext context, [FromRoute] string x) =>
    Results.Ok(new { path = context.Request.Path.Value, x = x }));


app.MapPut("/F/{x:regex(^[a-zA-\\d]+@.*\\.by$)}", (HttpContext context, [FromRoute] string x) =>
    Results.Ok(new { path = context.Request.Path.Value, x = x }));


app.MapFallback((HttpContext context) =>
    Results.NotFound(new { message = $"path {context.Request.Path.Value} not supported" }));

app.Map("/Error", (HttpContext context) =>
{
    var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
    return Results.Ok(new { message = exception?.Message });
});

app.Run();