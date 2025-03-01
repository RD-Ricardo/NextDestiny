using MassTransit;
using Orchestrator.Api.Hubs;
using Orchestrator.Api.Saga;
using Orchestrator.Api.Service;

var builder = WebApplication.CreateBuilder(args);

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
        cfg.Host("localhost", h =>
        {
            h.Username("rabbitmq");
            h.Password("rabbitmq");
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddSignalR();

builder.Services.AddScoped<ITrackingService, TrackingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<TrackingEventHub>("/tracking");

app.Run();
