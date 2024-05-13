using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Repositories.EfCore;
using Services.Contracts;
using Web.API.Extansions;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
})
    //Cvs formant�nda cal��t�rma
    .AddCustomCvsFormantter()
    //Xml Formant�nda cal��t�rma
    .AddXmlDataContractSerializerFormatters()

    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson();
// Dogrulamalar i�lemleri filitreleme
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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










var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExtentionHandler(logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
