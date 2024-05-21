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
    //Xml Formant�nda cal��t�rma
    .AddXmlDataContractSerializerFormatters()
    //Cvs formant�nda cal��t�rma
    .AddCustomCvsFormantter()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson(opt=>opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Dogrulamalar i�lemleri filitreleme
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
//IoC ��lem Yap�land�rmas�
builder.Services.ConfigureActionfilters();
//�stemcilerin uygulamaya istek atma i�lemi(izin i�lemi)
builder.Services.ConfigureCors();
//Property'leri �ekillendirme i�lemi
builder.Services.ConfigureDataShaper();
//Xlm ve Json MedyaType i�lemi
builder.Services.AddCustomMediaTypes();
//
builder.Services.AddScoped<IBookLinks, BookLinks>();
// Versionlama
builder.Services.ConfigureVesioning();
// Chaching yap�land�rmas�
builder.Services.ConfigureHttpCacheHeader();
//
builder.Services.AddMemoryCache();
// �stemci istek say�s� belirleme
builder.Services.ConfigureRateLimitingOptions();
//
builder.Services.AddHttpContextAccessor();
//Identity/JWT Yap�lanma
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
