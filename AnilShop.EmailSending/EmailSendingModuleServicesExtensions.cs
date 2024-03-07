using AnilShop.EmailSending.Data;
using AnilShop.EmailSending.EmailBackgroundService;
using AnilShop.EmailSending.Integrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Serilog;

namespace AnilShop.EmailSending;

public static class EmailSendingModuleServicesExtensions
{
    public static IServiceCollection AddEmailSendingModuleServices(
        this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger,
        List<System.Reflection.Assembly> mediatRAssemblies)
    {
        services.Configure<MongoDBSettings>(config.GetSection("MongoDB"));
        services.AddMongoDB(config);

        services.AddTransient<ISendEmail, MimeKitEmailSender>();
        services.AddTransient<IQueueEmailsInOutboxService, MongoDbQueueEmailOutboxService>();
        services.AddTransient<IGetEmailsFromOutboxService, MongoDbGetEmailsFromOutboxService>();
        services.AddTransient<ISendEmailsFromOutboxService,
            DefaultSendEmailsFromOutboxService>();

        mediatRAssemblies.Add(typeof(EmailSendingModuleServicesExtensions).Assembly);

        services.AddHostedService<EmailSendingBackgroundService>();

        logger.Information("{Module} module services registered", "Email Sending");
        return services;
    }

    private static IServiceCollection AddMongoDB(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            var settings = configuration.GetSection("MongoDB").Get<MongoDBSettings>();
            return new MongoClient(settings!.ConnectionString);
        });

        services.AddSingleton(serviceProvider =>
        {
            var settings = configuration.GetSection("MongoDB").Get<MongoDBSettings>();
            var client = serviceProvider.GetService<IMongoClient>();
            return client!.GetDatabase(settings!.DatabaseName);
        });

        services.AddTransient(serviceProvider =>
        {
            var database = serviceProvider.GetService<IMongoDatabase>();
            return database!.GetCollection<EmailOutboxEntity>("EmailOutboxEntityCollection");
        });

        return services;
    }
}