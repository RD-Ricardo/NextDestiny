using Hotel.Cancellation.Worker;
using Hotel.Infrastructure;
using NextDestiny.Core.Amqp;
using NextDestiny.Core.SecretManager;
using NextDestiny.Core.WebApi.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddSecretManager();
builder.AddSerilog(builder.Configuration, "hotel-cancellation-worker");
builder.Services.AddInfra(builder.Configuration);
builder.Services.AddAmqpServices(builder.Configuration, typeof(HotelBookingCancellationWorker));

var host = builder.Build();
host.Run();
