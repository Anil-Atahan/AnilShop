using System.Reflection;
using AnilShop.OrderProcessing;
using AnilShop.Products;
using AnilShop.SharedKernel;
using AnilShop.Users;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) =>
    config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddFastEndpoints()
    .AddJWTBearerAuth(builder.Configuration["Auth:JwtSecret"]!)
    .AddAuthorization()
    .SwaggerDocument();

List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
builder.Services.AddProductModuleService(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddUserModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddOrderProcessingModuleServices(builder.Configuration, logger, mediatRAssemblies);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()));
builder.Services.AddMediatRLoggingBehavior();

builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseSwaggerGen();

app.UseHttpsRedirection();

app.UseFastEndpoints();

app.Run();

public partial class Program { }