using Microsoft.AspNetCore.HttpLogging;

internal class Program
{
    private static void Main(string[] args)
    {
        //создание builder для настройки приложения
        var builder = WebApplication.CreateBuilder(args);

        //добавления сервиса для логгирования
        builder.Services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;

        });


        //создания конвейра с сервисами
        var app = builder.Build();

        //добавление middleware
        app.UseHttpLogging();

        //маппинг
        app.MapGet("/api", () => "Мое первое ASPA");

        //запуск
        app.Run();
    }
}
