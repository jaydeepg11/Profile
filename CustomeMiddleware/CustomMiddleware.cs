
public class CustomMiddleware
{
    public readonly RequestDelegate _Rd;
    public readonly ILogger<CustomMiddleware> _log;
    public CustomMiddleware(RequestDelegate Rd,ILogger<CustomMiddleware> log)
    {
        _Rd=Rd;
        _log=log;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _log.LogInformation("Start CustomMiddleware logic");
        await _Rd(context);
        _log.LogInformation("End CustomMiddleware logic");
    }
}