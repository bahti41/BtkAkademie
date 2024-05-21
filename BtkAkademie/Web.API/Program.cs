using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Presentation.ActionFilters;
using Repositories.EfCore;
using Services;
using Services.Contracts;
using Web.API.Extansions;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.CacheProfiles.Add("5min", new CacheProfile() { Duration = 300 });
})
    //Xml Formantýnda calýþtýrma
    .AddXmlDataContractSerializerFormatters()
    //Cvs formantýnda calýþtýrma
    .AddCustomCvsFormantter()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson(opt=>opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Dogrulamalar iþlemleri filitreleme
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();


//SqlServer Connection
builder.Services.ConfigureSqlContext(builder.Configuration);
//Repository
builder.Services.ConfigureRepositoryManager();
//Service
builder.Services.ConfigurServiceManager();
//NLog icin
builder.Services.ConfigureLoggerService();
//AutoMapping
builder.Services.AddAutoMapper(typeof(Program));
//IoC Ýþlem Yapýlandýrmasý
builder.Services.ConfigureActionfilters();
//Ýstemcilerin uygulamaya istek atma iþlemi(izin iþlemi)
builder.Services.ConfigureCors();
//Property'leri Þekillendirme iþlemi
builder.Services.ConfigureDataShaper();
//Xlm ve Json MedyaType iþlemi
builder.Services.AddCustomMediaTypes();
//
builder.Services.AddScoped<IBookLinks, BookLinks>();
// Versionlama
builder.Services.ConfigureVesioning();
// Chaching yapýlandýrmasý
builder.Services.ConfigureHttpCacheHeader();
//
builder.Services.AddMemoryCache();
// Ýstemci istek sayýsý belirleme
builder.Services.ConfigureRateLimitingOptions();
//
builder.Services.AddHttpContextAccessor();
//Identity/JWT Yapýlanma
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.RegisterRepositories();
builder.Services.RegisterService();



var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExtentionHandler(logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json","BTK Akademie v1");
        s.SwaggerEndpoint("/swagger/v2/swagger.json","BTK Akademie v2");
    });
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseIpRateLimiting();
app.UseCors("CorsPolicy");
app.UseResponseCaching();
app.UseHttpCacheHeaders();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
