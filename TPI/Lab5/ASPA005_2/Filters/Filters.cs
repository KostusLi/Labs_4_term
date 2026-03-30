using DAL004;

namespace ASPA005_2.Filters
{
    public class SurnameFilter : IEndpointFilter
    {
        public static IRepository repository;

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var celebrity = context.GetArgument<Celebrity>(0);

            if (celebrity == null) return Results.StatusCode(500);

            if (string.IsNullOrEmpty(celebrity.Surname) || celebrity.Surname.Length < 2)
            {
                return Results.Json(new { value = "Value:POST /Celebrities error, Surname is wrong" }, statusCode: 409);
            }

            var all = repository.getAllCelebrities();
            if (all.Any(c => c.Surname == celebrity.Surname))
            {
                return Results.Json(new { value = "Value:POST /Celebrities error, Surname is doubled" }, statusCode: 409);
            }

            return await next(context);
        }
    }

    public class PhotoExistFilter : IEndpointFilter
    {
        public static IRepository repository;

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var celebrity = context.GetArgument<Celebrity>(0);
            if (celebrity == null) return await next(context);

            string fileName = Path.GetFileName(celebrity.PhotoPath);
            string fullPath = Path.Combine(repository.BasePath, fileName);

            if (!File.Exists(fullPath))
            {
                context.HttpContext.Response.Headers.Add("X-Celebrity", $"NotFound={fileName}");
            }

            return await next(context);
        }
    }

    public class IdExistsFilter : IEndpointFilter
    {
        public static IRepository repository;

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var id = context.GetArgument<int>(0);

            if (repository.getCelebrityById(id) == null)
            {
                throw new Exception($"Found by Id: {repository.BasePath} error, Id = {id}");
            }

            return await next(context);
        }
    }
}