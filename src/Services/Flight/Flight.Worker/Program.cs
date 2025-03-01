using Flight.Infrastructure;
using Flight.Worker;
using NextDestiny.Core.Amqp;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfra(builder.Configuration);
builder.Services.AddAmqpServices(builder.Configuration, typeof(FlightBookingWorker));

var host = builder.Build();
host.Run();
