using Flight.Application.Services;
using Flight.Worker;
using NextDestiny.Core.Amqp;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddAmqpServices(builder.Configuration, typeof(Worker));

var host = builder.Build();
host.Run();
