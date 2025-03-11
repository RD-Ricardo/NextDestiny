using Flight.Infrastructure;
using Flight.Worker;
using NextDestiny.Core.Amqp;
using NextDestiny.Core.SecretManager;
using NextDestiny.Core.WebApi.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddSecretManager();
builder.AddSerilog(builder.Configuration, "flight-worker");
builder.Services.AddInfra(builder.Configuration);
builder.Services.AddAmqpServices(builder.Configuration, typeof(FlightBookingWorker));

var host = builder.Build();
host.Run();
