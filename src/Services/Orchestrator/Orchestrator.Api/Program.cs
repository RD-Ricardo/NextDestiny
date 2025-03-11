using MassTransit;
using NextDestiny.Core.SecretManager;
using NextDestiny.Core.WebApi.Extensions;
using Orchestrator.Api.Hubs;
using Orchestrator.Api.Saga;
using Orchestrator.Api.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddSecretManager();
builder.AddObservability("orchestrator-api");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddSagaStateMachine<OrderStateMachine, OrderState>()
        .InMemoryRepository()
        .Endpoint(e =>
        {
            e.Name = "order-saga";
        });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["AmqpSettings:Host"], h =>
        {
            h.Username(builder.Configuration["AmqpSettings:UserName"]!);
            h.Password(builder.Configuration["AmqpSettings:Password"]!);
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<ITrackingService, TrackingService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed(_ => true)
               .AllowCredentials();
    });
});

builder.Services.AddSignalR(x =>
{
    x.EnableDetailedErrors = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapControllers();

app.MapHub<TrackingEventHub>("/tracking");

app.Run();
