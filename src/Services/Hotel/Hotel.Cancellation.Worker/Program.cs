using Hotel.Cancellation.Worker;
using Hotel.Infrastructure;
using NextDestiny.Core.Amqp;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfra(builder.Configuration);
builder.Services.AddAmqpServices(builder.Configuration, typeof(Worker));

var host = builder.Build();
host.Run();
