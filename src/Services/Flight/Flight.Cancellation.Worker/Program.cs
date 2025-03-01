using Flight.Cancellation.Worker;
using Flight.Infrastructure;
using NextDestiny.Core.Amqp;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfra(builder.Configuration);
builder.Services.AddAmqpServices(builder.Configuration, typeof(FlightBookingCancellationWorker));

var host = builder.Build();
host.Run();
