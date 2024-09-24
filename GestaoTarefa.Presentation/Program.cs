using GestaoTarefa.Application.Extensions;
using GestaoTarefa.Infra.Data.Extensions;
using GestaoTarefa.Infra.Storage.Extensions;
using GestaoTarefa.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);


builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddDataContext(builder.Configuration);
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDoc();

var app = builder.Build();

app.UseSwaggerDoc();
app.UseAuthorization();

app.MapControllers();


app.Run();
