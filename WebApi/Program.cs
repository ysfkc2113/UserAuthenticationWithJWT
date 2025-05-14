using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NLog;
using Presentation.ActionFilters;
using Repositories.EFCore;
using Services;
using Services.Contracts;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.CacheProfiles.Add("10sec", new CacheProfile() { Duration = 10 });
})
//.AddXmlDataContractSerializerFormatters()
.AddCustomCsvFormatter()
.AddApplicationPart(typeof(Presentation.AssemblyRefence).Assembly)
.AddNewtonsoftJson(opt =>
opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);



builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();//swagger configurasyonu
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureActionFilters();
builder.Services.ConfigureCors();//farklý kullanýcýlardan isteklere izin
builder.Services.ConfigureDataShaper();
builder.Services.AddCustomMediaTypes();// xml cvs json gibi custom veri tipi oluþturma
builder.Services.AddScoped<IEventLinks, EventLinks>();//kitaplara linkler
builder.Services.ConfigureVersioning();//proje version 
builder.Services.ConfigureResponseCaching();//cache
builder.Services.ConfigureHttpCacheHeaders();//cache
builder.Services.AddMemoryCache();//Request Limit
builder.Services.ConfigureRateLimitingOptions();//Request Limit
builder.Services.AddHttpContextAccessor();//Request Limit
builder.Services.ConfigureIdentity();//user and password configuration
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.RegisterServices();//servislerin Yaþam döngüsü
builder.Services.RegisterRepositories();//Repositorilerin yaþam döngüsü


var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Club's Manager v1");
        s.SwaggerEndpoint("/swagger/v2/swagger.json", "Club's Manager v2");
    });
}

if(app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseIpRateLimiting();//Request Limit
app.UseCors("CorsPolicy");

app.UseResponseCaching();
app.UseHttpCacheHeaders();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
