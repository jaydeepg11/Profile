using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddAuthentication(options=>{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options=>{
    options.RequireHttpsMetadata=false;
    options.SaveToken= true;
    options.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SecurityToken123")),
        ValidateIssuer=false,
        ValidateAudience =false
    };
});

builder.Services.AddCors(options=>
{
    options.AddPolicy("AllowAll",builder=>{
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddHostedService<TimesHostedService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

//IMemory Cache
builder.Services.AddMemoryCache();

builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = "http://localhost:5215";
    option.InstanceName = "SimpleInstance";
}
);

builder.Services.AddDataProtection();

builder.Services.AddAntiforgery(option=>
{
    option.HeaderName="X-CSRF-Token";
}
);

builder.Services.AddControllersWithViews();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.UseRouting();
//Conventional Based Routing
app.UseEndpoints(endpoints=>
{
//     endpoints.MapControllerRoute(
//     name:"Default",
//     pattern :"api/{controller=Dashboard}/{action=DetailDist}"
// );
 endpoints.MapControllerRoute(
    name:"Default1",
    pattern :"{controller=Dashboard}/{action=DetailDist}"
);
});
app.MapControllers();//Attribute base Routing - routing use [Route] properties in controller and Action Method level 
app.UseCustomMiddleware();//custommiddleware jenkins example
app.Run();
