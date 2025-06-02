using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NLog;
using Presentation.ActionFilters;
using Repositories.EFCore;
using Services;
using Services.Contracts;
using WebApi.Extensions; // Bu using ifadesinin doðru olduðundan emin olun

var builder = WebApplication.CreateBuilder(args);

// NLog configuration
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add Controllers with options, JSON, XML formatters and custom CSV formatter if needed
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    //config.CacheProfiles.Add("10sec", new CacheProfile { Duration = 10 });
})
// .AddXmlDataContractSerializerFormatters()
// .AddCustomCsvFormatter()
// ***** BURAYI KONTROL EDÝN *****
// Eðer FilesController'ýnýz bu Program.cs dosyasýnýn bulunduðu ana projede (assembly'de) ise,
// bu satýra ihtiyacýnýz yoktur ve hatta sorun yaratabilir.
// Genellikle bu satýr, controller'larýnýz farklý bir sýnýf kütüphanesi projesindeyse kullanýlýr.
// Eðer FilesController ana projenizdeyse, bu satýrý yorumlayýn veya silin.
.AddApplicationPart(typeof(Presentation.AssemblyRefence).Assembly) // Bu satýrý yorumlayýn veya silin
.AddNewtonsoftJson(opt =>
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Suppress automatic model state validation to use custom filter
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// API Documentation and Versioning
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

// Database and Repositories
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.RegisterRepositories();

// Services
builder.Services.ConfigureServiceManager();
builder.Services.RegisterServices();
builder.Services.ConfigureLoggerService();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Filters and CORS
builder.Services.ConfigureActionFilters();
// builder.Services.ConfigureCors(); // CORS yapýlandýrmasý Program.cs içinde de olabilir, tekrar etmemek için burayý yorumlayabiliriz veya sadece bir yerde tutabiliriz.

// Data shaping and custom media types
builder.Services.ConfigureDataShaper();
builder.Services.AddCustomMediaTypes();

// Link service for HATEOAS
builder.Services.AddScoped<IEventLinks, EventLinks>();

// API Versioning
builder.Services.ConfigureVersioning();

// Caching
//builder.Services.ConfigureResponseCaching();
//builder.Services.ConfigureHttpCacheHeaders();
//builder.Services.AddMemoryCache();

// Rate Limiting
//builder.Services.ConfigureRateLimitingOptions();
//builder.Services.AddHttpContextAccessor();

// Identity and JWT Authentication
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

var app = builder.Build();

// Global Exception Handling
var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

// Swagger UI in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Club's Manager v1");
        s.SwaggerEndpoint("/swagger/v2/swagger.json", "Club's Manager v2");
    });
}

// Use HSTS in production
if (app.Environment.IsProduction())
{
    app.UseHsts();
}

// Middleware pipeline
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles(); // Bu kýsým, "Media" klasörü wwwroot içinde deðilse ve doðrudan eriþilmesi planlanýyorsa önemlidir.

//app.UseIpRateLimiting();

// ***** BURASI ÇOK ÖNEMLÝ: UseRouting() burada olmalý! *****
// Endpoint belirleme middleware'i mutlaka Authentication, Authorization ve MapControllers'tan önce gelmelidir.
app.UseRouting();

// CORS middleware'i UseRouting'den sonra gelmeli
app.UseCors("CorsPolicy");

//app.UseResponseCaching();
//app.UseHttpCacheHeaders();

// Kimlik doðrulama
app.UseAuthentication();
// Yetkilendirme
app.UseAuthorization();

// Controller rotalarýný haritalama
app.MapControllers();

app.Run();
