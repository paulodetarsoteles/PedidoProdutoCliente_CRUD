using Microsoft.EntityFrameworkCore;
using PedidoProdutoCliente.API.Extensions;
using PedidoProdutoCliente.Infrastructure.Contexts;
using Serilog;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    .WriteTo.File(
        path: "log-.txt",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7
    )
    .Enrich.FromLogContext()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddDbContext<PedidoProdutoClienteContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDependencyInjectionConfig();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseDataSeeder<PedidoProdutoClienteContext>();

app.Run();
