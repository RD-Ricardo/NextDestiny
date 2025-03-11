using Hotel.Infrastructure;
using Hotel.Worker;
using NextDestiny.Core.Amqp;
using NextDestiny.Core.SecretManager;
using NextDestiny.Core.WebApi.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddSecretManager();
builder.AddSerilog(builder.Configuration, "hotel-worker");
builder.Services.AddInfra(builder.Configuration);
builder.Services.AddAmqpServices(builder.Configuration, typeof(HotelWorker));

var host = builder.Build();
host.Run();
