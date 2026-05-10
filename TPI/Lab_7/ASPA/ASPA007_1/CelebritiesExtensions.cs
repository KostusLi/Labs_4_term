using DAL_Celebrity;
using DAL_Celebrity_MSSQL;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace ASPA007_1
{
    public static class CelebritiesExtensions
    {

        public static void AddCelebritiesConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("Celebrities.config.json", optional: false, reloadOnChange: true);
            builder.Services.Configure<CelebritiesConfig>(builder.Configuration.GetSection("Celebrities"));
        }

        public static void AddCelebritiesServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRepository, Repository>((IServiceProvider p) =>
            {
                CelebritiesConfig config = p.GetRequiredService<IOptions<CelebritiesConfig>>().Value;
                return new Repository(config.ConnectionString);
            });
        }

        public static void MapPhotoCelebrities(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();
            var config = scope.ServiceProvider.GetRequiredService<IOptions<CelebritiesConfig>>().Value;

            app.MapGet($"{config.PhotoRequestPath}/{{fname}}", async (IOptions<CelebritiesConfig> iconfig, HttpContext context, string fname) =>
            {
                string folder = iconfig.Value.PhotosFolder;
                string fullPath = Path.Combine(folder, fname);

                if (!System.IO.File.Exists(fullPath))
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return;
                }

                string ext = Path.GetExtension(fname).ToLower();
                context.Response.ContentType = ext == ".png" ? "image/png" : "image/jpeg";

                await context.Response.SendFileAsync(fullPath);
            });
        }

        public static void MapCelebrities(this WebApplication app)
        {
            app.MapGet("/api/Celebrities", (IRepository repo) => repo.GetAllCelebrities());
        }

        public static void MapLifeevents(this WebApplication app)
        {
            app.MapGet("/api/Lifeevents", (IRepository repo) => repo.GetAllLifeevents());
        }

        public static void UseANCErrorHandler(this WebApplication app, string mes)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next(context);
                }
                catch(Exception ex)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    var errorResponse = new
                    {
                        type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                        title = "Внутренняя ошибка сервера",
                        status = context.Response.StatusCode,
                        detail = ex.Message,
                        instance = mes
                    };

                    await context.Response.WriteAsJsonAsync(errorResponse);
                }
            });
        }

    }
}
